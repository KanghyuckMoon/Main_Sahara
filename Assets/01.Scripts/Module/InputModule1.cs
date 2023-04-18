using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapon;
using InputSystem;

namespace Module
{
	public partial class InputModule : AbBaseModule
	{
		public override void Update()
		{
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
			if (mainModule.CanConsecutiveAttack && mainModule.IsConsecutiveWeapon)
				if (Input.GetMouseButton(0))
					mainModule.Animator.SetBool("ConsecutiveAttack", true);
			
			if (mainModule.IsWeaponExist)
			{
				if (!StateModule.CheckState(State.ATTACK, State.CHARGE) &&
				    !StateModule.CheckState(State.SKILL))
				{
					//Debug.LogError("공격이다 공격이야!!!!!");

					if (Input.GetMouseButtonDown(0))
					{
						mainModule.Attacking = true;
						StateModule.AddState(State.ATTACK);
						StateModule.AddState(State.CHARGE);

						//AttackModule.SpownCurrentArrow();
						AttackModule.SpownAttackEffect();
					}

					if (Input.GetMouseButtonDown(1))
					{
						mainModule.StrongAttacking = true;
						StateModule.AddState(State.ATTACK);
						//StateModule.AddState(State.CHARGE);

						//AttackModule.SpownCurrentArrow();
						AttackModule.SpownAttackEffect();
					}
				}

				mainModule.IsCharging = Input.GetMouseButton(0);
				if (Input.GetMouseButtonUp(0)) // || Input.GetMouseButtonUp(1))
				{
					//bool _inputatk = Input.GetMouseButtonUp(0);
					//AttackModule.ProjectileObject?.GetComponent<IProjectile>().MovingFunc(mainModule.transform.forward, Quaternion.identity);// + new Vector3(0, 1.6f, 0));

					mainModule.Attacking = false;
					StateModule.RemoveState(State.CHARGE);
					//StateModule.RemoveState(State.ATTACK);
				}
				if (Input.GetMouseButtonUp(1)) // || Input.GetMouseButtonUp(1))
				{
					mainModule.StrongAttacking = false;
				}
			}
		}

		private void InputMove()
		{
			if (mainModule.CanMove)
			{
				float _inputX = Input.GetAxis("Horizontal");
				float _inputY = Input.GetAxis("Vertical");

				Vector2 _inputdir = new Vector2(_inputX, _inputY);

				mainModule.ObjDir = _inputdir;

				StateModule.AddState(State.MOVING);
			}
			else if (!StateModule.CheckState(State.SKILL))
			{
				
			}
			else
			{
				mainModule.ObjDir = Vector2.zero;
			}
		}

		private void InputJump()
		{
			if (!StateModule.CheckState(State.ATTACK, State.JUMP, State.CHARGE))
			{
				if (mainModule.StopOrNot >= 1 && !StateModule.CheckState(State.SKILL))
				{
					bool _inputup = InputManager.Instance.CheckKey("Jump");// Input.GetKey(KeyCode.Space);

					mainModule.IsJump = _inputup;
					mainModule.IsJumpBuf = _inputup;

					if (_inputup)
						StateModule.AddState(State.JUMP);
				}
			}
		}

		private void InputSprint()
        {
			bool _inputrun = InputManager.Instance.CheckKey("Sprint");

			mainModule.IsSprint = _inputrun;
		}

        private void InputSkill()
        {
			if (!StateModule.CheckState(State.ATTACK, State.JUMP, State.CHARGE) && !mainModule.IsDead)
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

					if (Input.GetKeyDown(KeyCode.LeftControl))
						mainModule.IsDash = true;
				}
			}
		}

		private void InputTest()
        {
			if(Input.GetMouseButtonDown(2))
            {
				//Debug.LogError("asdfawefaeabraergae");
				mainModule.SettingTime.SetTime(1, 0.1f);
            }
        }

	}
}