using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using EnemyAnimation;
using Pool;

namespace Module
{
    public class NoneDirMoveModule : AbBaseModule
    {
        public SpiderProceduralAnimation SpiderAnimation
		{
            get
			{
                spiderAnimation ??= mainModule.gameObject.GetComponentInChildren<SpiderProceduralAnimation>();
                return spiderAnimation;
			}
		}

        private SpiderProceduralAnimation spiderAnimation;

        private Animator animator;

        private float moveSpeed => statData.WalkSpeed;
        private float rotationVelocity;
        private float targetRotation;
        private float rotation;

        private float animationBlend;
        private float currentSpeed;

        private float speedOffset = 0.1f;

        private float addSpeed;

        private StatData statData;
        private Vector3 currentDirection;

        public NoneDirMoveModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }
        public NoneDirMoveModule() : base()
        {

        }


        /// <summary>
        /// 유기체의 움직임 + 회전. 모든 움직임을 제어한다. 점프제외
        /// </summary>
        public void Move()
        {
            #region 속도 관련 부분
            float _targetSpeed = mainModule.IsSprint ? moveSpeed + 4 : moveSpeed;
            float _lockOnspeed = mainModule.LockOn ? -2 : 0;

            //SpiderAnimation.SetStepSize(_targetSpeed * 0.08f);

            float _speed;

            if (mainModule.ObjDir == Vector2.zero || mainModule.Attacking || mainModule.StrongAttacking) _targetSpeed = 0.0f;

            float currentSpeed = new Vector3(mainModule.CharacterController.velocity.x, 0, mainModule.CharacterController.velocity.z).magnitude;

            if (currentSpeed > (_targetSpeed + _lockOnspeed) + speedOffset ||
                currentSpeed < (_targetSpeed + _lockOnspeed) - speedOffset)// && mainModule.objDir != Vector2.up)
            {
                _speed = Mathf.Lerp(currentSpeed, _targetSpeed + _lockOnspeed, 13.7f * Time.fixedDeltaTime);
            }
            else
            {
                _speed = _targetSpeed + _lockOnspeed;
            }

            animationBlend = Mathf.Lerp(animationBlend, _targetSpeed + _lockOnspeed, Time.fixedDeltaTime * 20);
            if (animationBlend < 0.01f) animationBlend = 0f;
            #endregion

            Vector3 _targetDirection = new Vector3(mainModule.ObjDir.x, 0, mainModule.ObjDir.y);

            //Vector3 _velocity = NextStepGroundAngle(_speed, _targetDirection) > mainModule.maxSlope ? _targetDirection : Vector3.zero;

            Vector3 _rotate = mainModule.transform.eulerAngles;
            Vector3 _dir = _targetDirection.normalized;
            float _gravity = mainModule.Gravity;//Vector3.down * Mathf.Abs(mainModule.characterController.velocity.y);
            Vector3 _moveValue;

            if (mainModule.ObjDir != Vector2.zero)
            {
                targetRotation = Mathf.Atan2(_dir.x, _dir.z) * Mathf.Rad2Deg +
                                  mainModule.ObjRotation.eulerAngles.y;
                rotation = Mathf.SmoothDampAngle(_rotate.y, targetRotation, ref rotationVelocity, 0.05f);
            }

            Vector3 _direction = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward; //

            _direction = VelocityOnSlope(_direction, _targetDirection);

            _moveValue = _direction.normalized * ((_speed + addSpeed) * mainModule.StopOrNot) * Time.fixedDeltaTime;

            mainModule.KnockBackVector = Vector3.Lerp(mainModule.KnockBackVector, Vector3.zero, Time.fixedDeltaTime * 2);
            //SpiderAnimation.SetStop(mainModule.KnockBackVector.magnitude > 0.5f);
            mainModule.CharacterController.Move(_moveValue + mainModule.KnockBackVector + (new Vector3(0, _gravity, 0) * Time.fixedDeltaTime));

            animator.SetFloat("MoveSpeed", animationBlend);
        }

        private Vector3 VelocityOnSlope(Vector3 velocity, Vector3 dir)
        {
            Vector3 _rayPos = new Vector3(mainModule.transform.position.x, mainModule.transform.position.y + mainModule.groundOffset,
                mainModule.transform.position.z);
            var _ray = new Ray(_rayPos, Vector3.down);

            //if (dir == Vector3.zero) { mainModule.StopOrNot = 0; }
            //else mainModule.StopOrNot = 1;

            if (Physics.Raycast(_ray, out RaycastHit _hitInfo, 0.2f))
            {
                var _slopRotation = Quaternion.FromToRotation(Vector3.up, _hitInfo.normal);
                var _adjustedVelocity = _slopRotation * velocity;

                if (_adjustedVelocity.y < 0)// && _adjustedVelocity.y > 60)
                {
                    addSpeed = ((0.5f - _adjustedVelocity.y) * (0.3f - _adjustedVelocity.y) * 1.1f);
                    Debug.Log(addSpeed);
                    //if (dir == Vector3.zero) { addSpeed = 0; mainModule.StopOrNot = 0; }
                    //else mainModule.StopOrNot = 1;
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
                mainModule.Gravity += mainModule.GravityScale * Time.fixedDeltaTime * 2;
            }
        }

        /// <summary>
        /// 기타 설정해줄 것들. 공중에서는 IK꺼주기
        /// </summary>
        private void ETC()
        {
            //mainModule.footRotate.enabled = mainModule.isGround;
        }

        public override void FixedUpdate()
        {
            if (!mainModule.IsSlope)
                Move();
            Gravity();
            ETC();
        }

        public override void Start()
        {
            animator = mainModule.GetModuleComponent<AnimationModule>(ModuleType.Animation).animator;
            statData = mainModule.GetComponent<StatData>();
            animationBlend = 0;
        }
        public override void OnDisable()
        {
            base.OnDisable();
            ClassPoolManager.Instance.RegisterObject<NoneDirMoveModule>("NoneDirMoveModule", this);
        }
    }
}