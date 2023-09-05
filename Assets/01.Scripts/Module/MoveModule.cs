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

        public float XRotation => xRotation;
        public float ZRotation => zRotation;

        public float passiveSpeed = 0;
        public bool isCrawling;
        
        protected Animator animator;
        protected float moveSpeed => StatData.WalkSpeed;
        protected float runSpeed => StatData.RunSpeed;
        protected float rotationVelocity;
        protected float targetRotation;
        protected float rotation;
        protected float xRotation;
        protected float zRotation;

        protected float animationBlend;
        protected float currentSpeed;

        protected float speedOffset = 0.1f;

        protected float addSpeed;
        private float previousSpeed;

        private float knockBackPower = 0;

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

            var _targetSpeed = mainModule.IsSprint ? runSpeed : moveSpeed;
            var _lockOnspeed = (mainModule.LockOn ? -1 : 0) + passiveSpeed;

            float _speed;

            if (!mainModule.isGround)
            {
				_targetSpeed = (mainModule.IsSprint ? runSpeed - 1 : moveSpeed - 1);
			}
            if (mainModule.ObjDir == Vector2.zero || mainModule.Attacking || mainModule.StrongAttacking)
            {
                _targetSpeed = 0.0f;
            }

            if (_targetSpeed == 0 && previousSpeed > 1.4f)
            {
                Animator.SetTrigger("IfStop");
            }

            previousSpeed = _targetSpeed;

            var velocity = mainModule.CharacterController.velocity;
            var currentSpeed = new Vector3(velocity.x, 0, velocity.z).magnitude;

            _targetSpeed *= mainModule.StopOrNot;

            if (currentSpeed > (_targetSpeed + _lockOnspeed) + speedOffset ||
                currentSpeed < (_targetSpeed + _lockOnspeed) - speedOffset) // && mainModule.objDir != Vector2.up)
            {
                var _stopValue = mainModule.ObjDir == Vector2.zero ? 10.2f : 5.7f;
                _speed = Mathf.Lerp(currentSpeed, _targetSpeed + _lockOnspeed,
                    _stopValue * mainModule.PersonalDeltaTime);
            }
            else
            {
                _speed = _targetSpeed + _lockOnspeed;
            }

            //animationBlend = mainModule.isGround ? animationBlend : 0;
            animationBlend = Mathf.Lerp(animationBlend, _targetSpeed + _lockOnspeed,
                mainModule.PersonalDeltaTime * 9.5f);
            if (animationBlend < 0.01f) animationBlend = 0f;

            #endregion

            #region 이동 관련 부분

            var _targetDirection = new Vector3(mainModule.ObjDir.x, 0, mainModule.ObjDir.y);

            var _rotate = mainModule.transform.eulerAngles;
            var _dir = _targetDirection.normalized;
            var _gravity = mainModule.Gravity;
            Vector3 _moveValue;

            if (_dir != Vector3.zero)
            {
                targetRotation = Mathf.Atan2(_dir.x, _dir.z) * Mathf.Rad2Deg +
                                 mainModule.ObjRotation.eulerAngles.y;
            }
            rotation = Mathf.SmoothDampAngle(_rotate.y, targetRotation, ref rotationVelocity,
                1.6f * mainModule.PersonalDeltaTime);

            if (!StateModule.CheckState(State.HIT, State.ATTACK, State.SKILL))
            {
                if (!mainModule.Attacking || !mainModule.StrongAttacking || knockBackPower > 0f)
                {

                    if (mainModule.LockOnTarget != null)
					{
						mainModule.transform.rotation =
							Quaternion.Euler(xRotation, mainModule.ObjRotation.eulerAngles.y, zRotation);

						//Quaternion _qu = Quaternion.LookRotation(Quaternion.Euler(0.0f, rotation, 0.0f).eulerAngles, Vector3.up);
						//mainModule.transform.rotation =
						//    Quaternion.RotateTowards(mainModule.transform.rotation, _qu, 10 * mainModule.PersonalDeltaTime);
					}
					else
					if (mainModule.ObjDir != Vector2.zero)
					{
						//Debug.LogWarning($"Obj Rotation {rotation}, Time {mainModule.PersonalDeltaTime}", mainModule.gameObject);
						Quaternion curRotation = Quaternion.Euler(mainModule.transform.eulerAngles.x, rotation, mainModule.transform.eulerAngles.z);
						Quaternion changeRotation = Quaternion.RotateTowards(curRotation, Quaternion.Euler(xRotation, rotation, mainModule.transform.eulerAngles.z), 150 * mainModule.PersonalDeltaTime);
						changeRotation = Quaternion.RotateTowards(changeRotation, Quaternion.Euler(changeRotation.eulerAngles.x, changeRotation.eulerAngles.y, zRotation), 70 * mainModule.PersonalDeltaTime);
						mainModule.transform.rotation = changeRotation;
					}
					else
					{
                        var changeRotation = Quaternion.RotateTowards(Quaternion.Euler(xRotation, mainModule.transform.eulerAngles.y, zRotation), Quaternion.Euler(xRotation, rotation, zRotation), 5 * mainModule.PersonalDeltaTime);
                        //changeRotation = Quaternion.RotateTowards(changeRotation, Quaternion.Euler(xRotation, changeRotation.eulerAngles.y, zRotation), 50 * mainModule.PersonalDeltaTime);
                        mainModule.transform.rotation = changeRotation;
					}
                }
            }

            var _direction = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward; //

            _direction = VelocityOnSlope(_direction, _targetDirection);

            _moveValue = _direction.normalized * (_speed * mainModule.StopOrNot);
            //_moveValue *= mainModule.PersonalDeltaTime;
            var _moveVector3 = _moveValue;
            mainModule.attackedTime += mainModule.PersonalDeltaTime;
            var _decreaseKnockBackValue = -3 * mainModule.attackedTime * mainModule.attackedTime;
            knockBackPower = _decreaseKnockBackValue + mainModule.knockBackPower;
            var _knockBackVector = knockBackPower * mainModule.knockBackVector;

            if (knockBackPower <= 0f)
            {
                mainModule.knockBackPower = 0f;
                mainModule.KnockBackVector = Vector3.zero;
                _knockBackVector = Vector3.zero;
            }

            if (mainModule.ObjDir != Vector2.zero)
            {
                mainModule.ObjDirection = _moveVector3;
            }
            else
            {
                //_moveVector3 = Vector3.zero;
            }

            if (mainModule.IsSlope)
            {
                if (knockBackPower > 0f)
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
                mainModule.CharacterController.Move(
                    (mainModule.SlopeVector + new Vector3(0, _gravity, 0)) *
                    mainModule.PersonalDeltaTime);
            }


            #endregion

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
                if (mainModule.ObjDir == Vector2.zero)
                {
                    _slopRotation = Quaternion.identity;
                    velocity = Vector3.zero;
                    return Vector3.zero;
                }

                var _adjustedVelocity = _slopRotation * velocity;

                if (_adjustedVelocity.y < 0)
                {
                    addSpeed = ((0.5f - _adjustedVelocity.y) * (0.3f - _adjustedVelocity.y) * 1.1f);

                    //Logging.Log(addSpeed);
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
                if (!mainModule.isTouchGround)
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

            Pool.ClassPoolManager.Instance.RegisterObject<MoveModule>(this);
		}

        public override void OnDestroy()
        {
            stateModule = null;
            animator = null;
            statData = null;
            mainModule = null;

            base.OnDestroy();

            Pool.ClassPoolManager.Instance.RegisterObject<MoveModule>(this);
        }

        public void SetXRotation(float angle)
        {
            xRotation = Mathf.Lerp(xRotation, Mathf.Clamp(angle, -15, 15), Time.deltaTime * 2);
            //mainModule.Root.rotation = Quaternion.Euler(xRotation, 0, 0);
        }
		public void SetZRotation(float angle)
		{
			zRotation = Mathf.Lerp(zRotation, Mathf.Clamp(angle, -15, 15), Time.deltaTime * 2);;
			//mainModule.Root.rotation = Quaternion.Euler(xRotation, 0, 0);
		}
	}
}