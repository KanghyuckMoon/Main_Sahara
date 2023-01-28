using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Module
{
    public class MoveModule : AbBaseModule
    {
        private Animator animator;

        private float moveSpeed => mainModule.GetModuleComponent<StateModule>(ModuleType.State).Speed;
        private float rotationVelocity;
        private float targetRotation;
        private float rotation;

        private float animationBlend;
        private float currentSpeed;

        private float speedOffset = 0.1f;

        private Vector3 currentDirection;

        public MoveModule(MainModule _mainModule) : base(_mainModule)
        {

        }

        /// <summary>
        /// 유기체의 움직임 + 회전. 모든 움직임을 제어한다. 점프제외
        /// </summary>
        public void Move()
        {
            #region 속도 관련 부분
            float _targetSpeed = mainModule.isSprint ? moveSpeed + 4 : moveSpeed;
            float _speed;

            if (mainModule.objDir == Vector2.zero) _targetSpeed = 0.0f;

            float currentSpeed = new Vector3(mainModule.characterController.velocity.x, 0, mainModule.characterController.velocity.z).magnitude;

            if (currentSpeed > _targetSpeed + speedOffset ||
                currentSpeed < _targetSpeed - speedOffset)// && mainModule.objDir != Vector2.up)
            {
                _speed = Mathf.Lerp(currentSpeed, _targetSpeed, 7 * Time.deltaTime);
            }
            else
            {
                _speed = _targetSpeed;
            }

            animationBlend = Mathf.Lerp(animationBlend, _targetSpeed, Time.deltaTime * 7);
            if (animationBlend < 0.01f) animationBlend = 0f;
            #endregion

            Vector3 _targetDirection = new Vector3(mainModule.objDir.x, 0, mainModule.objDir.y);

            //Vector3 _velocity = NextStepGroundAngle(_speed, _targetDirection) > mainModule.maxSlope ? _targetDirection : Vector3.zero;

            Vector3 _rotate = mainModule.transform.eulerAngles;
            Vector3 _dir = _targetDirection.normalized;
            float _gravity = mainModule.gravity;//Vector3.down * Mathf.Abs(mainModule.characterController.velocity.y);
            Vector3 _moveValue;

            if (mainModule.objDir != Vector2.zero)
            {
                targetRotation = Mathf.Atan2(_dir.x, _dir.z) * Mathf.Rad2Deg +
                                  mainModule.objRotation.eulerAngles.y;
                rotation = Mathf.SmoothDampAngle(_rotate.y, targetRotation, ref rotationVelocity, 0.12f);

                mainModule.transform.parent.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }
            Vector3 _direction = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

            _moveValue = _direction.normalized * _speed * Time.fixedDeltaTime;
            mainModule.characterController.Move(_moveValue + (new Vector3(0, _gravity, 0) * Time.fixedDeltaTime));

            animator.SetFloat("MoveSpeed", animationBlend);
        }

        private Vector3 SlopVelocity(Vector3 direction)
        {
            Vector3 _dirVelocity = Vector3.ProjectOnPlane(direction, mainModule.slopeHit.normal).normalized;
            return _dirVelocity;
        }

        public float NextStepGroundAngle(float moveSpeed, Vector3 _targetDirection)
        {
            var _nextPosition = mainModule.raycastTarget.position + _targetDirection * moveSpeed * Time.fixedDeltaTime;

            if (Physics.Raycast(_nextPosition, Vector3.down, out RaycastHit _hitInfo, 2f, mainModule.groundLayer))
            {
                return Vector3.Angle(Vector3.up, _hitInfo.normal);
            }
            return 0f;
        }

        /// <summary>
        /// 중력을 계속 계산해 준다.
        /// </summary>
        private void Gravity()
        {
            if (mainModule.isGround)
            {
                if (mainModule.gravity < 0.0f)
                {
                    mainModule.gravity = -2f;
                }
            }
            if (mainModule.gravity < 100)
            {
                mainModule.gravity += mainModule.gravityScale * Time.fixedDeltaTime * 2;
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
            Move();
            Gravity();
            ETC();
        }

        public override void Start()
        {
            animator = mainModule.GetModuleComponent<AnimationModule>(ModuleType.Animation).animator;
            animationBlend = 0;
        }
    }
}