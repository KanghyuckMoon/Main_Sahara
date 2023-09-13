using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using ForTheTest;
using Module;

namespace LockOn
{
    public class LockOnCamera : MonoBehaviour
    {
        [SerializeField]
        public GameObject currentCamera;

        [SerializeField]
        private Transform player;
        [SerializeField]
        private ThirdPersonCameraController thirdPersonCameraController;
        [SerializeField]
        private CinemachineFreeLook playerCam;
        [SerializeField]
        private CinemachineVirtualCamera groupCam;
        [SerializeField]
        private CinemachineVirtualCamera zoomCam;
        [SerializeField]
        private Transform playerTarget;
        [SerializeField]
        private CinemachineTargetGroup cinemachineTargetGroup;
        [SerializeField]
        private AbMainModule playerModule;

        private List<Transform> lockOnTargetList = new List<Transform>();
        public Transform target;    // 얘 public으로 바꿈!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        private bool isTarget;

        private StateModule stateModule;

        //private Camera 

        public void AddLockOnObject(Transform _lockOnObj)
		{
            if (lockOnTargetList.Contains(_lockOnObj))
			{
                return;
			}
            lockOnTargetList.Add(_lockOnObj);
        }
        public void RemoveLockOnObject(Transform _lockOnObj)
        {
            lockOnTargetList.Remove(_lockOnObj);
        }

        public void Start()
        {
            currentCamera = playerCam.gameObject;
            stateModule = playerModule.GetModuleComponent<StateModule>(ModuleType.State);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.T) &&
                !stateModule.CheckState(State.CHARGE))
			{
                SetLockOn();
            }
		}

		public void SetLockOn()
		{
            if (isTarget)
			{
                //록온 해제
                cinemachineTargetGroup.RemoveMember(target);
                isTarget = false;
                target = null;
                groupCam.LookAt = null;

                playerCam.gameObject.SetActive(true);
                groupCam.gameObject.SetActive(false);

                //thirdPersonCameraController.cameraY = player.eulerAngles.y;    
                currentCamera = playerCam.gameObject;

                playerModule.LockOnTarget = null;
                playerModule.LockOn = false;
                //Debug.Log("t:록온 해제");
                return;
            }

            if(lockOnTargetList.Count == 0)
			{
                //캐릭터의 전방을 본다
                //Debug.Log("t:전방1");
                thirdPersonCameraController.ChangeCamera(10, -player.eulerAngles.y);
                return;
            }
            else
			{
                int _count = lockOnTargetList.Count;
                float _minDistance = float.MaxValue;
                Transform _target = null;
                for(int i = 0; i < _count; ++i)
                {
                    float _distance = DistanceBetweenTwoVector(transform.position, lockOnTargetList[i].position);
                    if (_minDistance > _distance)
					{
                        _minDistance = _distance;
                        _target = lockOnTargetList[i];
                    }
                }

                //목표하는 대상이 없거나 최단거리 대상이 너무 멀다면 
                if(_target is null || _minDistance > 42f)
				{
                    //캐릭터의 전방을 본다
                    thirdPersonCameraController.ChangeCamera(10, -player.eulerAngles.y);
                    //Debug.Log("t:전방2");
                    return;
                }
                else
				{
                    target = _target;
                    isTarget = true;
                    //Debug.Log("t:타겟팅");
                    cinemachineTargetGroup.AddMember(target, 1f, 1);
                    groupCam.LookAt = target;

                    groupCam.gameObject.SetActive(true);
                    playerCam.gameObject.SetActive(false);

                    currentCamera = groupCam.gameObject;

                    playerModule.LockOnTarget = target;
                    playerModule.LockOn = true;

                    return;
                }
			}
		}


        public static float DistancePointLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
        {
            return Vector3.Magnitude(ProjectPointLine(point, lineStart, lineEnd) - point);

            //return Vector3.Distance();
        }

        public float DistanceBetweenTwoVector(Vector3 pointStart, Vector3 pointEnd)
        {
            return Vector3.Distance(pointStart, pointEnd);
        }


        public static Vector3 ProjectPointLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
        {
            Vector3 rhs = point - lineStart;

            Vector3 vector = lineEnd - lineStart;

            float magnitude = vector.magnitude;

            Vector3 vector2 = vector;

            if (magnitude > 1E-06f)

            {

                vector2 /= magnitude;

            }



            float value = Vector3.Dot(vector2, rhs);

            value = Mathf.Clamp(value, 0f, magnitude);

            return lineStart + vector2 * value;

        }
    }

}
