using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Module
{
    public class RotationFreeMoveModule : MoveModule
    {
        public override void Move()
        {
            #region 속도 관련 부분

            float _targetSpeed = mainModule.IsSprint ? runSpeed : moveSpeed;
            float _lockOnspeed = mainModule.LockOn ? -1 : 0;

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

            //if (!StateModule.CheckState(State.HIT, State.ATTACK, State.SKILL))
            //{
            //    if (!mainModule.Attacking || !mainModule.StrongAttacking)
            //    {
            //        if (mainModule.LockOnTarget is null && mainModule.ObjDir != Vector2.zero)
            //        {
            //            mainModule.transform.rotation = Quaternion.Euler(0, rotation, 0);
            //            //Quaternion.RotateTowards(mainModule.transform.rotation,
            //            //Quaternion.Euler(0, rotation, 0), 5 * mainModule.PersonalDeltaTime);
            //            //Quaternion _qu = Quaternion.LookRotation(Quaternion.Euler(0.0f, rotation, 0.0f).eulerAngles, Vector3.up);
            //            //mainModule.transform.rotation =
            //            //    Quaternion.RotateTowards(mainModule.transform.rotation, _qu, 10 * mainModule.PersonalDeltaTime);
            //        }
            //
            //        if (mainModule.LockOnTarget is not null)
            //        {
            //            mainModule.transform.rotation =
            //                Quaternion.Euler(0.0f, mainModule.ObjRotation.eulerAngles.y, 0.0f);
            //        }
            //    }
            //}
            //mainModule.transform.rotation = Quaternion.Euler(0.0f, mainModule.ObjRotation.eulerAngles.y, 0.0f);

            if(!mainModule.LockOn)
            {
				mainModule.transform.rotation = Quaternion.Euler(0, rotation, 0);
			}

            Vector3 _direction = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward; //

            _direction = VelocityOnSlope(_direction, _targetDirection);

            _moveValue = _direction.normalized * ((_speed + addSpeed) * mainModule.StopOrNot);
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
        public override void OnDisable()
        {
            stateModule = null;
            animator = null;
            statData = null;
            mainModule = null;

            //base.OnDisable();

            Pool.ClassPoolManager.Instance.RegisterObject<RotationFreeMoveModule>(this);
        }

        public override void OnDestroy()
        {
            stateModule = null;
            animator = null;
            statData = null;
            mainModule = null;

            //base.OnDestroy();

            Pool.ClassPoolManager.Instance.RegisterObject<RotationFreeMoveModule>(this);
        }
    }

}