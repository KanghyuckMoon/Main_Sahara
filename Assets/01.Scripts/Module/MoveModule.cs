using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using Utill.Measurement;

namespace Module
{
    public class MoveModule : AbBaseModule
    {
        public float AnimationBlend
        {
            get
            {
                return animationBlend;
            }
        }
        protected StateModule StateModule
        {
            get
            {
                stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);
                return stateModule;
            }
        }

        protected StatData StatData
		{
            get
            {
                statData ??= mainModule.GetComponent<StatData>();
                return statData;
            }
		}

        protected Animator Animator
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

        public float passiveSpeed = 0;
        public bool isCrawling;
        
        protected Animator animator;
        protected float moveSpeed => StatData.WalkSpeed;
        protected float runSpeed => StatData.RunSpeed;
        protected float rotationVelocity;
        protected float targetRotation;
        protected float rotation;

        protected float animationBlend;
        protected float currentSpeed;

        protected float speedOffset = 0.1f;

        protected float addSpeed;

        protected StatData statData;
        protected Vector3 currentDirection;
        protected StateModule stateModule;
        protected static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");

        public MoveModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }

        public MoveModule()
		{

		}

        public override void Init(AbMainModule _mainModule, params string[] _parameters)
        {
            base.Init(_mainModule, _parameters);
            mainModule = _mainModule;
        }

        /// <summary>
        /// 유기체의 움직임 + 회전. 모든 움직임을 제어한다. 점프제외
        /// </summary>
        public virtual void Move()
        {
            #region 속도 관련 부분

            float _targetSpeed = mainModule.IsSprint ? runSpeed : moveSpeed;
            float _lockOnspeed = (mainModule.LockOn ? -1 : 0) + passiveSpeed;

            float _speed;

            if (!mainModule.isGround) _targetSpeed = mainModule.IsSprint ? runSpeed - 2 : moveSpeed - 2;
            if (mainModule.ObjDir == Vector2.zero || mainModule.Attacking || mainModule.StrongAttacking)
                _targetSpeed = 0.0f;

            var velocity = mainModule.CharacterController.velocity;
            float currentSpeed = new Vector3(velocity.x, 0, velocity.z).magnitude;

            _targetSpeed *= mainModule.StopOrNot;
            
            if (currentSpeed > (_targetSpeed + _lockOnspeed) + speedOffset ||
                currentSpeed < (_targetSpeed + _lockOnspeed) - speedOffset) // && mainModule.objDir != Vector2.up)
            {
                _speed = Mathf.Lerp(currentSpeed, _targetSpeed + _lockOnspeed, 6.8f * mainModule.PersonalDeltaTime);
            }
            else
            {
                _speed = _targetSpeed + _lockOnspeed;
            }

            //animationBlend = mainModule.isGround ? animationBlend : 0;
            animationBlend = Mathf.Lerp(animationBlend, _targetSpeed + _lockOnspeed,
                mainModule.PersonalDeltaTime * 5);
            if (animationBlend < 0.01f) animationBlend = 0f;

            #endregion
            Vector3 _targetDirection = new Vector3(mainModule.ObjDir.x, 0, mainModule.ObjDir.y);

            Vector3 _rotate = mainModule.transform.eulerAngles;
            Vector3 _dir = _targetDirection.normalized;
            float _gravity = mainModule.Gravity;
            Vector3 _moveValue;

            targetRotation = Mathf.Atan2(_dir.x, _dir.z) * Mathf.Rad2Deg +
                             mainModule.ObjRotation.eulerAngles.y;
            rotation = Mathf.SmoothDampAngle(_rotate.y, targetRotation, ref rotationVelocity,
                1.6f * mainModule.PersonalDeltaTime);

            if (!StateModule.CheckState(State.HIT, State.ATTACK, State.SKILL))
            {
                if (!mainModule.Attacking || !mainModule.StrongAttacking)
                {
                    if (mainModule.LockOnTarget is null && mainModule.ObjDir != Vector2.zero)
                    {
                        mainModule.transform.rotation = Quaternion.Euler(0, rotation, 0);
                        //Quaternion.RotateTowards(mainModule.transform.rotation,
                        //Quaternion.Euler(0, rotation, 0), 5 * mainModule.PersonalDeltaTime);
                        //Quaternion _qu = Quaternion.LookRotation(Quaternion.Euler(0.0f, rotation, 0.0f).eulerAngles, Vector3.up);
                        //mainModule.transform.rotation =
                        //    Quaternion.RotateTowards(mainModule.transform.rotation, _qu, 10 * mainModule.PersonalDeltaTime);
                    }

                    if (mainModule.LockOnTarget is not null)
                    {
                        mainModule.transform.rotation =
                            Quaternion.Euler(0.0f, mainModule.ObjRotation.eulerAngles.y, 0.0f);
                    }
                }
            }

            Vector3 _direction = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward; //

            _direction = VelocityOnSlope(_direction, _targetDirection);

            _moveValue = _direction.normalized * ((_speed + passiveSpeed) * mainModule.StopOrNot);
            //_moveValue *= mainModule.PersonalDeltaTime;
            Vector3 _moveVector3 = _moveValue;
            mainModule.attackedTime += mainModule.PersonalDeltaTime;
            float _decreaseKnockBackValue = -3 * mainModule.attackedTime * mainModule.attackedTime;
            float _knockBackPower = _decreaseKnockBackValue + mainModule.knockBackPower;
            Vector3 _knockBackVector = _knockBackPower * mainModule.knockBackVector;

            if (_knockBackPower <= 0f)
            {
                mainModule.knockBackPower = 0f;
                mainModule.KnockBackVector = Vector3.zero;
                _knockBackVector = Vector3.zero;
            }

            mainModule.ObjDirection = _moveVector3;

            if (mainModule.IsSlope)
            {
                if (_knockBackPower > 0f)
                {
                    mainModule.CharacterController.Move((_knockBackVector + new Vector3(0, _gravity, 0)) *
                                                        mainModule.PersonalDeltaTime);
                }
                else
                {

                    mainModule.CharacterController.Move((_moveVector3 + new Vector3(0, _gravity, 0)) *
                                                        mainModule.PersonalDeltaTime);
                }
            }
            else
            {
                mainModule.CharacterController.Move(mainModule.SlopeVector * mainModule.PersonalDeltaTime);
            }

            Animator.SetFloat(MoveSpeed, animationBlend);
        }

        public void Crawling()
        {
            Vector3 _targetDirection = new Vector3(mainModule.ObjDir.x, mainModule.ObjDir.y, 0);
            mainModule.CharacterController.Move(_targetDirection* mainModule.PersonalDeltaTime);
        }
        // ReSharper disable Unity.PerformanceAnalysis
        protected Vector3 VelocityOnSlope(Vector3 velocity, Vector3 dir)
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
            Gravity();
        }

        public override void Update()
        {
            Move();
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

        public override void OnDestroy()
        {
            stateModule = null;
            animator = null;
            statData = null;
            mainModule = null;

            base.OnDestroy();

            Pool.ClassPoolManager.Instance.RegisterObject<MoveModule>("MoveModule", this);
        }
	}
}