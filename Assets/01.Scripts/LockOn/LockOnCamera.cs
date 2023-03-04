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
        private CinemachineVirtualCamera playerCam;
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
        public Transform target;    // �� public���� �ٲ�!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        private bool isTarget;

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
        }

        public void Update()
		{
			if (Input.GetKeyDown(KeyCode.T))
			{
                SetLockOn();
            }
		}

		public void SetLockOn()
		{
            if (isTarget)
			{
                //�Ͽ� ����
                cinemachineTargetGroup.RemoveMember(target);
                isTarget = false;
                target = null;
                groupCam.LookAt = null;

                playerCam.gameObject.SetActive(true);
                groupCam.gameObject.SetActive(false);
                currentCamera = playerCam.gameObject;

                playerModule.LockOn = false;
                playerModule.LockOnTarget = null;

                Debug.Log("t:�Ͽ� ����");
                return;
            }

            if(lockOnTargetList.Count == 0)
			{
                //ĳ������ ������ ����
                Debug.Log("t:����1");
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
                    float _distance = DistancePointLine(transform.position, transform.position, lockOnTargetList[i].position);
                    if (_minDistance > _distance)
					{
                        _minDistance = _distance;
                        _target = lockOnTargetList[i];
                    }
                }

                //��ǥ�ϴ� ����� ���ų� �ִܰŸ� ����� �ʹ� �ִٸ� 
                if(_target is null || _minDistance > 1000f)
				{
                    //ĳ������ ������ ����
                    thirdPersonCameraController.ChangeCamera(10, -player.eulerAngles.y);
                    Debug.Log("t:����2");
                    return;
                }
                else
				{
                    target = _target;
                    isTarget = true;
                    Debug.Log("t:Ÿ����");
                    cinemachineTargetGroup.AddMember(target, 1f, 1);
                    groupCam.LookAt = target;

                    groupCam.gameObject.SetActive(true);
                    playerCam.gameObject.SetActive(false);

                    currentCamera = groupCam.gameObject;

                    playerModule.LockOn = true;
                    playerModule.LockOnTarget = target;

                    return;
                }
			}
		}


        public static float DistancePointLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)

        {

            return Vector3.Magnitude(ProjectPointLine(point, lineStart, lineEnd) - point);

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
