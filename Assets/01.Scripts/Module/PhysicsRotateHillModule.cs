using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Effect;
using Utill.Addressable;
using Utill;
using HitBox;
using Data;
using Attack;
using Buff;
using DG.Tweening;
using Skill;
using Item;
using Pool;
using System.Runtime.InteropServices.ComTypes;

namespace Module
{
	public class PhysicsRotateHillModule : PhysicsModule
	{
        private MoveModule MoveModule
        {
            get
			{
				moveModule ??= mainModule.GetModuleComponent<MoveModule>(ModuleType.Move);
				return moveModule;
            }
        }
        private MoveModule moveModule;

        public PhysicsRotateHillModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }
        public PhysicsRotateHillModule() : base()
        {

        }
        protected override void Slope()
        {
            var _transform = mainModule.transform;
            var _position = _transform.position;
            var _rayPos = mainModule.RaycastTarget.position;
			var _ray = new Ray(_rayPos, -_transform.up);
			var _ray2 = new Ray(_rayPos, -Vector3.up);
			
			Vector3 castOrigin =
				new Vector3(mainModule.transform.position.x, mainModule.transform.position.y - mainModule.groundOffset,
					mainModule.transform.position.z);
			
			if (Physics.SphereCast(_ray, 0.31f,out var _raycastHit, 1.4f, mainModule.groundLayer))
			{
				//mainModule.GroundAngle = Vector3.Angle(Vector3.up, _raycastHit.normal);
				//Debug.Log(mainModule.name + " 현재 각도: ");
				
                float _distance = _raycastHit.distance;
				if(mainModule.groundDistance > _distance) 
                {
                    GroundIn(_rayPos, _raycastHit.normal);
				}
                else
				{
					GroundOut();
				}
				//ZRotationSlope();

				//
				// Calculate the desired tilt angle based on the ground normal.
				//float tiltAngle = Mathf.Rad2Deg * Mathf.Atan2(_raycastHit.normal.x, _raycastHit.normal.z);
				//MoveModule.SetZRotation(tiltAngle);
			}
			else if(Physics.Raycast(_ray2, out var _raycastHit2, 1.4f, mainModule.groundLayer))
			{
				float _distance = _raycastHit2.distance;
				if (mainModule.groundDistance > _distance)
				{
					GroundIn(_rayPos, _raycastHit2.normal);
				}
				else
				{
					GroundOut();
				}
			}
            else
            {
				GroundOut();
			}
            //Debug.DrawRay(_rayPos, Vector3.down, Color.red);
        }

        private void GroundIn(Vector3 _rayPos, Vector3 _rayNormal)
        {
	        var forward = mainModule.transform.forward;
	        forward.y = 0;
	        forward = forward.normalized;
	        RaycastHit _hit;
	        var _ray1 = new Ray(_rayPos, forward);
	        Vector3 castOrigin =
		        new Vector3(mainModule.transform.position.x, mainModule.transform.position.y - mainModule.groundOffset,
			        mainModule.transform.position.z);

	        var _transform = mainModule.transform;
	        var _ray = new Ray(_rayPos, -_transform.up);
	        Physics.SphereCast(_ray, 0.33f, out _hit, 1.45f, mainModule.groundLayer);
	        //mainModule.Animator.SetFloat("GrounDegree", previousAngle * mainModule.CanCrawlTheWall);


	        var _angle = Vector3.Angle(Vector3.up, _hit.normal);

	        //Debug.LogError(mainModule.name +": "+ _angle);

	        var _slopeLimit = mainModule.CharacterController.slopeLimit;
	        mainModule.IsSlope = _angle <= _slopeLimit + 3f;


	        if (Vector3.Dot(forward, _hit.normal) > 0)
	        {
		        MoveModule.SetXRotation(_angle);
		        //MoveModule.SetZRotation(_angle);
	        }
	        else
	        {
		        MoveModule.SetXRotation(-_angle);
		        //MoveModule.SetZRotation(-_angle);
	        }

	        ZRotationSlope(_hit.normal);

	        mainModule.SlopeVector = new Vector3(_hit.normal.x, -_hit.normal.y, _hit.normal.z) * 5;
        }

        private void GroundOut()
		{
			mainModule.IsSlope = true;
			MoveModule.SetXRotation(0f);
		}

		private void ZRotationSlope(Vector3 curNormal)
        {
			if (Physics.Raycast(mainModule.leftFeet.position + Vector3.up * 5, Vector3.down, out var _leftFootHit, 20f, mainModule.groundLayer)
				&& Physics.Raycast(mainModule.rightFeet.position + Vector3.up * 5, Vector3.down, out var _rightFootHit, 20f, mainModule.groundLayer))
			{
				float distance = _leftFootHit.point.y - _rightFootHit.point.y;
				float _angle = -distance * Mathf.Rad2Deg;
				MoveModule.SetZRotation(_angle);
				//Debug.Log($"{mainModule.gameObject.name} Y Distance {_angle}", mainModule.gameObject);
			}
            else
			{
				MoveModule.SetZRotation(0f);
			}
		}
        public override void OnDisable()
        {
            hitModule = null;
            stateModule = null;
            mainModule = null;
            landAction = null;
            //base.base.OnDisable();
            Pool.ClassPoolManager.Instance.RegisterObject<PhysicsRotateHillModule>(this);
        }
        public override void OnDestroy()
        {
            hitModule = null;
            stateModule = null;
            mainModule = null;
            landAction = null;
            //base.base.OnDestroy();
            Pool.ClassPoolManager.Instance.RegisterObject<PhysicsRotateHillModule>(this);
        }
	}
}