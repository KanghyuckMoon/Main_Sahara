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
			if (!mainModule.isDead)
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
			if (!mainModule.attacking && mainModule.isWeaponExist && !mainModule.strongAttacking)
			{
				if (mainModule.StopOrNot != 0 || !mainModule.isDead)
				{
					bool _inputatk = Input.GetMouseButtonDown(0);
					bool _inputStratk = Input.GetMouseButtonDown(1);

					mainModule.attacking = _inputatk;
					mainModule.strongAttacking = _inputStratk;
				}
			}
		}

		private void InputMove()
		{
			if (mainModule.canMove && !mainModule.attacking && !mainModule.strongAttacking)
			{
				if (!mainModule.isDead)
				{
					float _inputX = Input.GetAxis("Horizontal");
					float _inputY = Input.GetAxis("Vertical");

					Vector2 _inputdir = new Vector2(_inputX, _inputY);

					mainModule.objDir = _inputdir;
				}
			}
		}

		private void InputJump()
		{
			if (mainModule.canMove && !mainModule.attacking && !mainModule.strongAttacking)
			{
				if (!mainModule.isDead)
				{
					bool _inputup = Input.GetKey(KeyCode.Space);

					mainModule.isJump = _inputup;
					mainModule.isJumpBuf = _inputup;
				}
			}
		}

		private void InputSprint()
        {
			bool _inputrun = Input.GetKey(KeyCode.LeftShift);

			mainModule.isSprint = _inputrun;
		}

        private void InputSkill()
        {
			if (Input.GetKeyDown(KeyCode.E) && !mainModule.isDead)
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