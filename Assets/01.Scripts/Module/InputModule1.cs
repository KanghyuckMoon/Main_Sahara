using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapon;

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
			if (mainModule.IsWeaponExist)
			{
				if (!StateModule.CheckState(State.ATTACK, State.JUMP, State.CHARGE))
				{
					//Debug.LogError("공격이다 공격이야!!!!!");

					if (Input.GetMouseButtonDown(0))
					{
						mainModule.Attacking = true;
						StateModule.AddState(State.ATTACK);
						StateModule.AddState(State.CHARGE);

						AttackModule.SpownCurrentArrow();
						AttackModule.SpownAttackEffect();
					}
				}
				mainModule.IsCharging = Input.GetMouseButton(0);

				if (Input.GetMouseButtonUp(0))
				{
					//bool _inputatk = Input.GetMouseButtonUp(0);
					AttackModule.ProjectileObject?.GetComponent<IProjectile>().MovingFunc();
				}
			}
		}

		private void InputMove()
		{
			if (!StateModule.CheckState(State.ATTACK))
			{
				float _inputX = Input.GetAxis("Horizontal");
				float _inputY = Input.GetAxis("Vertical");

				Vector2 _inputdir = new Vector2(_inputX, _inputY);

				mainModule.ObjDir = _inputdir;

				StateModule.AddState(State.MOVING);
			}
		}

		private void InputJump()
		{
			if (!StateModule.CheckState(State.ATTACK, State.JUMP, State.CHARGE))
			{
				if (mainModule.StopOrNot >= 1)
				{
					bool _inputup = Input.GetKey(KeyCode.Space);

					mainModule.IsJump = _inputup;
					mainModule.IsJumpBuf = _inputup;

					if (_inputup)
						StateModule.AddState(State.JUMP);
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