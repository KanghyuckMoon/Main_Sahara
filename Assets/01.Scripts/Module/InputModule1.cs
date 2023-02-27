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
			if (!StateModule.CheckState(State.DEAD))
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
			if (!StateModule.CheckState(State.ATTACK, State.JUMP))
			{
				mainModule.Attacking = Input.GetMouseButtonDown(0);
				StateModule.AddState(State.ATTACK);

				AttackModule.SpownAttackEffect();
			}

			mainModule.IsCharging = Input.GetMouseButton(0);

			if(StateModule.CheckState(State.ATTACK))
            {
				bool _inputatk = Input.GetMouseButtonUp(0);
			}
		}

		private void InputMove()
		{
			if (mainModule.CanMove && !mainModule.Attacking && !mainModule.StrongAttacking)
			{
					float _inputX = Input.GetAxis("Horizontal");
					float _inputY = Input.GetAxis("Vertical");

					Vector2 _inputdir = new Vector2(_inputX, _inputY);

					mainModule.ObjDir = _inputdir;
			}
		}

		private void InputJump()
		{
			if (mainModule.CanMove && !mainModule.Attacking && !mainModule.StrongAttacking)
			{
				if (mainModule.StopOrNot >= 1)
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
    }
}