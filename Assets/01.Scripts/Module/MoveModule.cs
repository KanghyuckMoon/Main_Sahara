using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using Utill.Measurement;

namespace Module
{
    public class MoveModule : AbBaseModule
    {
        private StateModule StateModule
        {
            get
            {
                stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);
                return stateModule;
            }
        }

        private StatData StatData
		{
            get
            {
                statData ??= mainModule.GetComponent<StatData>();
                return statData;
            }
		}

        private Animator Animator
		{
            get
            {
                animator ??= mainModule.GetModuleComponent<AnimationModule>(ModuleType.Animation).animator;
                return animator;
            }
            set
			{
                animator = value;

            }
		}
        private Animator animator;
        private float moveSpeed => StatData.Speed;
        private float rotationVelocity;
        private float targetRotation;
        private float rotation;

        private float animationBlend;
        private float currentSpeed;

        private float speedOffset = 0.1f;

        private float addSpeed;

        private StatData statData;
        private Vector3 currentDirection;
        private StateModule stateModule;
        private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");

        public MoveModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }

        public MoveModule()
		{

		}

        /// <summary>
        /// 유기체의 움직임 + 회전. 모든 움직임을 제어한다. 점프제외
        /// </summary>
        public void Move()
        {
            #region 속도 관련 부분
            float _targetSpeed = mainModule.IsSprint ? moveSpeed + 5 : moveSpeed;
            float _lockOnspeed = mainModule.LockOn ? -2 : 0;

            float _speed;

            if (!mainModule.isGround) _targetSpeed = mainModule.IsSprint ? moveSpeed - 2 + 4 : moveSpeed - 2;
            if (mainModule.ObjDir == Vector2.zero || mainModule.Attacking || mainModule.StrongAttacking) _targetSpeed = 0.0f;

            var velocity = mainModule.CharacterController.velocity;
            float currentSpeed = new Vector3(velocity.x, 0, velocity.z).magnitude;

            if (currentSpeed > (_targetSpeed + _lockOnspeed) + speedOffset ||
                currentSpeed < (_targetSpeed + _lockOnspeed) - speedOffset)// && mainModule.objDir != Vector2.up)
            {
                _speed = Mathf.Lerp(currentSpeed, _targetSpeed + _lockOnspeed, 6.8f * mainModule.PersonalFixedDeltaTime);
            }
            else
            {
                _speed = _targetSpeed + _lockOnspeed;
            }

            animationBlend = mainModule.isGround ? animationBlend : 0;
            animationBlend = Mathf.Lerp(animationBlend, _targetSpeed + _lockOnspeed, mainModule.PersonalFixedDeltaTime * 5);
            if (animationBlend < 0.01f) animationBlend = 0f;
            #endregion

            Vector3 _targetDirection = new Vector3(mainModule.ObjDir.x, 0, mainModule.ObjDir.y);

            Vector3 _rotate = mainModule.transform.eulerAngles;
            Vector3 _dir = _targetDirection.normalized;
            float _gravity = mainModule.Gravity;
            Vector3 _moveValue;

                targetRotation = Mathf.Atan2(_dir.x, _dir.z) * Mathf.Rad2Deg +
                                  mainModule.ObjRotation.eulerAngles.y;
                rotation = Mathf.SmoothDampAngle(_rotate.y, targetRotation, ref rotationVelocity, 0.05f);

                if (!mainModule.Attacking || !mainModule.StrongAttacking)
                {
                if (mainModule.LockOnTarget is null && mainModule.ObjDir != Vector2.zero)
                {
                    mainModule.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
                }
                if (mainModule.LockOnTarget is not null)
                {
                    mainModule.transform.rotation = Quaternion.Euler(0.0f, mainModule.ObjRotation.eulerAngles.y, 0.0f);
                }
            }

            Vector3 _direction = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward; //

            _direction = VelocityOnSlope(_direction, _targetDirection);

            _moveValue = _direction.normalized * ((_speed + addSpeed) * mainModule.StopOrNot);
            _moveValue *= mainModule.PersonalFixedDeltaTime;
            
            mainModule.KnockBackVector = Vector3.Lerp(mainModule.KnockBackVector, Vector3.zero, mainModule.PersonalFixedDeltaTime);
            if (mainModule.IsSlope)
            {
                mainModule.CharacterController.Move((_moveValue + mainModule.KnockBackVector + (new Vector3(0, _gravity, 0)) * mainModule.PersonalFixedDeltaTime));
            }
            else
            {
                mainModule.CharacterController.Move(mainModule.SlopeVector * mainModule.PersonalFixedDeltaTime);
            }
            Animator.SetFloat(MoveSpeed, animationBlend);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private Vector3 VelocityOnSlope(Vector3 velocity, Vector3 dir)
        {
            var position = mainModule.transform.position;
            Vector3 _rayPos = new Vector3(position.x, position.y + mainModule.groundOffset,
                position.z);
            var _ray = new Ray(_rayPos, Vector3.down);

            if (Physics.Raycast(_ray, out RaycastHit _hitInfo, 0.2f))
            {
                var _slopRotation = Quaternion.FromToRotation(Vector3.up, _hitInfo.normal);
                var _adjustedVelocity = _slopRotation * velocity;

                if (_adjustedVelocity.y < 0)
                {
                    addSpeed = ((0.5f - _adjustedVelocity.y) * (0.3f - _adjustedVelocity.y) * 1.1f);
                    Logging.Log(addSpeed);
                    return _adjustedVelocity;
                }
            }

            addSpeed = 0;
            return velocity;
        }

        /// <summary>
        /// 중력을 계속 계산해 준다.
        /// </summary>
        private void Gravity()
        {
            if (mainModule.isGround)
            {
                if (mainModule.Gravity < 0.0f)
                {
                    mainModule.Gravity = -2f;
                }
            }
            if (mainModule.Gravity < 100)
            {
                mainModule.Gravity += mainModule.GravityScale * mainModule.PersonalFixedDeltaTime * 2;
            }
        }

        public override void FixedUpdate()
        {
            Move();
            Gravity();
        }

        public override void Start()
        {
            animator = mainModule.GetModuleComponent<AnimationModule>(ModuleType.Animation).animator;
            statData = mainModule.GetComponent<StatData>();
            animationBlend = 0;
        }

		public override void OnDisable()
		{
            stateModule = null;
            animator = null;
            statData = null;
            mainModule = null;

            base.OnDisable();

            Pool.ClassPoolManager.Instance.RegisterObject<MoveModule>("MoveModule", this);
		}
	}
}