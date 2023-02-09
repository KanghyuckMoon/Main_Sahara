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
			InputMove();
			InputJump();
			InputSprint();
		}

		private void InputMove()
		{
			if (mainModule.canMove)
			{
				float _inputX = Input.GetAxis("Horizontal");
				float _inputY = Input.GetAxis("Vertical");

				Vector2 _inputdir = new Vector2(_inputX, _inputY);

				mainModule.objDir = _inputdir;
			}
		}

		private void InputJump()
		{
			bool _inputup = Input.GetKey(KeyCode.Space);

			mainModule.isJump = _inputup;
			mainModule.isJumpBuf = _inputup;
		}

		private void InputSprint()
		{
			bool _inputrun = Input.GetKey(KeyCode.LeftShift);

			mainModule.isSprint = _inputrun;
		}
	}
}