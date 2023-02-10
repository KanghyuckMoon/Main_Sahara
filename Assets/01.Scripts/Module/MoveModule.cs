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

        private float addSpeed;

        private Vector3 currentDirection;

        public MoveModule(AbMainModule _mainModule) : base(_mainModule)
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

            if (mainModule.objDir == Vector2.zero || mainModule.attacking || mainModule.strongAttacking) _targetSpeed = 0.0f;

            float currentSpeed = new Vector3(mainModule.characterController.velocity.x, 0, mainModule.characterController.velocity.z).magnitude;

            if (currentSpeed > _targetSpeed + speedOffset ||
                currentSpeed < _targetSpeed - speedOffset)// && mainModule.objDir != Vector2.up)
            {
                _speed = Mathf.Lerp(currentSpeed, _targetSpeed, 13.7f * Time.deltaTime);
            }
            else
            {
                _speed = _targetSpeed;
            }

            animationBlend = Mathf.Lerp(animationBlend, _targetSpeed, Time.deltaTime * 20);
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
                rotation = Mathf.SmoothDampAngle(_rotate.y, targetRotation, ref rotationVelocity, 0.05f);

                mainModule.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }
            Vector3 _direction = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

            _direction = VelocityOnSlope(_direction, _targetDirection);

            _moveValue = _direction.normalized * ((_speed + addSpeed) * mainModule.StopOrNot) * Time.fixedDeltaTime;
            mainModule.characterController.Move(_moveValue + (new Vector3(0, _gravity, 0) * Time.fixedDeltaTime));

            animator.SetFloat("MoveSpeed", animationBlend);
        }

        private Vector3 VelocityOnSlope(Vector3 velocity, Vector3 dir)
        {
            Vector3 _rayPos = new Vector3(mainModule.transform.position.x, mainModule.transform.position.y + mainModule.groundOffset,
                mainModule.transform.position.z);
            var _ray = new Ray(_rayPos, Vector3.down);

            if (dir == Vector3.zero) { mainModule.StopOrNot = 0; }
            else mainModule.StopOrNot = 1;

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