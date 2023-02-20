using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Module
{
	public partial class InputModule : AbBaseModule
	{
		public override void Update()
		{
			if (!mainModule.IsDead)
			{
				InputMove();
				InputJump();
				InputSprint();
				InputAttack();
				InputSkill();
			}
		}

		private void InputAttack()
		{
			if (!mainModule.Attacking && mainModule.IsWeaponExist && !mainModule.StrongAttacking)
			{
				if (mainModule.StopOrNot != 0 || !mainModule.IsDead)
				{
					bool _inputatk = Input.GetMouseButtonDown(0);
					bool _inputStratk = Input.GetMouseButtonDown(1);

					mainModule.Attacking = _inputatk;
					mainModule.StrongAttacking = _inputStratk;

					AttackModule.SpownAttackEffect();
				}
			}
		}

		private void InputMove()
		{
			if (mainModule.CanMove && !mainModule.Attacking && !mainModule.StrongAttacking)
			{
				if (!mainModule.IsDead)
				{
					float _inputX = Input.GetAxis("Horizontal");
					float _inputY = Input.GetAxis("Vertical");

					Vector2 _inputdir = new Vector2(_inputX, _inputY);

					mainModule.ObjDir = _inputdir;
				}
			}
		}

		private void InputJump()
		{
			if (mainModule.CanMove && !mainModule.Attacking && !mainModule.StrongAttacking)
			{
				if (!mainModule.IsDead && mainModule.StopOrNot == 1)
				{
					bool _inputup = Input.GetKey(KeyCode.Space);

					mainModule.IsJump = _inputup;
					mainModule.IsJumpBuf = _inputup;
				}
			}
		}

		private void InputSprint()
        {
			bool _inputrun = Input.GetKey(KeyCode.LeftShift);

			mainModule.IsSprint = _inputrun;
		}

        private void InputSkill()
        {
			if (Input.GetKeyDown(KeyCode.E) && !mainModule.IsDead)
			{
				mainModule.GetModuleComponent<WeaponModule>(ModuleType.Weapon).UseWeaponSkills();//.BaseWeapon.weaponSkills.Invoke();
			}
        }
        //public Vector2 move;
        //public Vector2 look;

        //public void OnMove(InputValue value)
        //{
        //	MoveInput(value.Get<Vector2>());
        //}

        //public void OnLook(InputValue value)
        //{
        //	Debug.Log("카메라 움직움직");
        //	LookInput(value.Get<Vector2>());
        //}

        //public void OnLock(InputValue value)
        //{

        //}

        //public void OnJump(InputValue value)
        //{
        //	mainModule.isJump = value.isPressed;
        //}

        //public void MoveInput(Vector2 newMoveDirection)
        //{
        //	move = newMoveDirection;
        //	//MainModule -> Object -> InputSystem

        //	mainModule.objDir = move;
        //	//이제 여기서 받은 값을 이동으로 옮겨준다
        //}

        //public void LookInput(Vector2 newLookDirection)
        //{
        //	//mainModule.objRotation = Quaternion.Euler(newLookDirection);
        //}

    }
}