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
			float _inputX = Input.GetAxis("Horizontal");
			float _inputY = Input.GetAxis("Vertical");

			Vector2 _inputdir = new Vector2(_inputX, _inputY);

			mainModule.objDir = _inputdir;
        }

		private void InputJump()
        {
			bool _inputup = Input.GetKeyDown(KeyCode.Space);

			mainModule.isJump = _inputup;
        }

		private void InputSprint()
        {
			bool _inputrun = Input.GetKey(KeyCode.LeftShift);

			mainModule.isSprint = _inputrun;
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