using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapon;
using InputSystem;
using TimeManager;

namespace Module
{
	public partial class InputModule : AbBaseModule
	{
		public override void Update()
		{
			if (StaticTime.EntierTime <= 0f)
			{
				return;
			}
			if (!StateModule.CheckState(State.DEAD, State.UI))
			{
				InputMove();
				InputJump();
				InputSprint();
				InputAttack();
				InputSkill();
				InputTest();
			}
		}

		private void InputAttack()
		{
			if (mainModule.IsWeaponExist)
			{
				if (mainModule.CanConsecutiveAttack && mainModule.IsConsecutiveWeapon)
					if (Input.GetMouseButton(0))
						mainModule.Animator.SetTrigger("ConsecutiveAttack");
				
				if (Input.GetMouseButtonUp(0))
				{
					mainModule.Attacking = false;
					StateModule.RemoveState(State.CHARGE);
				}
				
				mainModule.IsCharging = Input.GetMouseButton(0);
				
				if (!StateModule.CheckState(State.ATTACK, State.CHARGE, State.SKILL) && !StateModule.CheckState(State.EQUIP))
				{
					if (Input.GetMouseButtonDown(0))
					{
						mainModule.SetAnimationLayerOn(0,0);
						
						StateModule.AddState(State.ATTACK);
						StateModule.AddState(State.CHARGE);

						AttackModule.SpownAttackEffect();
						mainModule.Attacking = true;
					}
				}
			}
		}

		private void InputMove()
		{
			if (mainModule.CanMove && !StateModule.CheckState(State.SKILL))
			{
				float _inputX = Input.GetAxis("Horizontal");
				float _inputY = Input.GetAxis("Vertical");

				Vector2 _inputdir = new Vector2(_inputX, _inputY);

				mainModule.ObjDir = _inputdir;

				StateModule.AddState(State.MOVING);
			}
			else if (StateModule.CheckState(State.SKILL))
			{
				
			}
			else
			{
				mainModule.ObjDir = Vector2.zero;
			}
		}

		private void InputJump()
		{
			if (StateModule.CheckState(State.ATTACK, State.CHARGE)) return;
			if (!(mainModule.StopOrNot >= 1) || StateModule.CheckState(State.SKILL)) return;
			var _inputup = InputManager.Instance.CheckKey("Jump");

			mainModule.IsJump = _inputup;
			mainModule.IsJumpBuf = _inputup;
		}

		private void InputSprint()
        {
			bool _inputrun = InputManager.Instance.CheckKey("Sprint");

			mainModule.IsSprint = _inputrun;
		}

        private void InputSkill()
        {
			if (!StateModule.CheckState(State.ATTACK, State.JUMP, State.CHARGE) && !StateModule.CheckState(State.EQUIP))
			{
				if (!StateModule.CheckState(State.SKILL))
				{
					if (InputManager.Instance.CheckKey("Skill1"))
					{
						//Input.key
						//StateModule.AddState(State.SKILL);
						//Debug.LogError(("sdffgafgadfadfafgafgafgasgasgasgd"));
						SkillModule.UseSkill("E");//.BaseWeapon.weaponSkills.Invoke();
					}
					if (InputManager.Instance.CheckKey("Skill2"))
					{
						//StateModule.AddState(State.SKILL);
						SkillModule.UseSkill("R");
					}
					if (InputManager.Instance.CheckKey("WeaponSkill"))
					{
						//StateModule.AddState(State.SKILL);
						SkillModule.UseWeaponSkill();
					}

					if (Input.GetMouseButtonDown(1))
						mainModule.IsDash = true;
				}
			}
		}

		private void InputTest()
        {
			/*if(Input.GetMouseButtonDown(2))
            {
				//Debug.LogError("asdfawefaeabraergae");
				mainModule.SettingTime.SetTime(1, 0.1f);
            }*/
        }

	}
}