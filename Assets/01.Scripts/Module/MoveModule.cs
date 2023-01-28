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
        /// 유기체의 움직임 + 회전. 순수 x,z좌표만 움직인다!!!!
        /// </summary>
        public void Move()
        {
            float _targetSpeed = mainModule.isSprint ? moveSpeed + 5 : moveSpeed;
            float _speed;
            
            if (mainModule.objDir == Vector2.zero) _targetSpeed = 0.0f;

            float currentSpeed = new Vector3(mainModule.rigidBody.velocity.x, 0, mainModule.rigidBody.velocity.z).magnitude;

            if (currentSpeed > _targetSpeed + speedOffset ||
                currentSpeed < _targetSpeed - speedOffset)// && mainModule.objDir != Vector2.up)
            {
                _speed = Mathf.Lerp(currentSpeed, _targetSpeed /* inputMagnitude*/, 7 * Time.deltaTime);
            }
            else
            {
                _speed = _targetSpeed;
            }

            animationBlend = Mathf.Lerp(animationBlend, _targetSpeed, Time.deltaTime * 7);
            if (animationBlend < 0.01f) animationBlend = 0f;

            Vector3 _targetDirection;
            _targetDirection = new Vector3(mainModule.objDir.x, 0, mainModule.objDir.y);
            _targetDirection.y = 0;

            Vector3 _rotate = mainModule.transform.eulerAngles;
            Vector3 _dir = _targetDirection.normalized;
            Vector3 _gravity = Vector3.down * Mathf.Abs(mainModule.rigidBody.velocity.y);

            //_dir += _rotate;
            if (mainModule.objDir != Vector2.zero)
            {
                targetRotation = Mathf.Atan2(_dir.x, _dir.z) * Mathf.Rad2Deg +
                                  mainModule.objRotation.eulerAngles.y;
                rotation = Mathf.SmoothDampAngle(_rotate.y, targetRotation, ref rotationVelocity, 0.12f);

                mainModule.rigidBody.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }
            //Vector3 direction = 
            _targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

            if (mainModule.isGround) _gravity = Vector3.zero;

            Vector3 _moveValue = _targetDirection.normalized * (_speed);// * Time.deltaTime);// + (new Vector3(0, mainModule.gravity, 0) * Time.deltaTime);
            mainModule.rigidBody.velocity = _moveValue + _gravity;//.MovePosition(mainModule.transform.position + _moveValue);
            //mainModule.moveSpeed = Mathf.Abs(mainModule.characterController.velocity.x) + Mathf.Abs(mainModule.characterController.velocity.z);

            animator.SetFloat("MoveSpeed", animationBlend);
        }

        private void OnAir()
        {
            //if (mainModule.isGround)
            //{
            //    currentDirection = mainModule.transform.parent.eulerAngles;
            //    currentSpeed = moveSpeed;
            //}
            //else
            //{
            //    Vector3 _curPos = mainModule.transform.parent.eulerAngles;
            //    Vector3 _savedPos = currentDirection;

            //    bool _isDirectionCurrect = _savedPos.y + 15 >= _curPos.y && _savedPos.y - 15 <= _curPos.y;

            //    float _correctionRate = _isDirectionCurrect ? 1.4F : 0.86f;
            //    currentSpeed = moveSpeed * _correctionRate;
            //}
        }

        private void SlopeYouCanStandOn()
        {
            //Physics. mainModule.transform.position;
        }

        /// <summary>
        /// 기타 설정해줄 것들. 공중에서는 IK꺼주기
        /// </summary>
        private void ETC()
        {
            mainModule.footRotate.enabled = mainModule.isGround;
            //if (mainModule.isGround) mainModule.characterController.center = new Vector3(0, 0.75f, 0);
            //else mainModule.characterController.center = new Vector3(0, 1f, 0);
        }

        public override void FixedUpdate()
        {
            Move();
            OnAir();

            ETC();
        }
        
        public override void Start()
        {
            animator = mainModule.GetModuleComponent<AnimationModule>(ModuleType.Animation).animator;
            animationBlend = 0;
            //moveSpeed = mainModule.GetModuleComponent<StateModule>(ModuleType.State).Speed;
            //Input = _mainModule.dic<InputModule>(ModuleType.INPUT);
        }

        public override void Update()
        {

        }

        public override void Awake()
        {
        }
    }
}