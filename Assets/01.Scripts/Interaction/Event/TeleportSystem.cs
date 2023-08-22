using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Effect;
using DG.Tweening;

namespace Interaction
{

    public class TeleportSystem : MonoBehaviour
    {
        [SerializeField] private Transform upWall;
        [SerializeField] private Transform downWall;
        
        [SerializeField]
        private Transform upHouse;
		[SerializeField]
		private Transform downHouse;

		private bool isMoving = false;
        private bool isUp;

        private float upWallOrigin;
        private float upWallIsUp;
        private float upWallIsDown;
        private float downWallOrigin;
		private float downWallIsUp;
		private float downWallIsDown;

		void Start()
        {
            upWallOrigin = upHouse.position.y;
			upWallIsUp = upHouse.position.y + 25f;
			upWallIsDown = upHouse.position.y - 25f;
			downWallOrigin = downHouse.position.y;
			downWallIsUp = downHouse.position.y + 25f;
			downWallIsDown = downHouse.position.y - 25f;

            var upVec = upWall.position;
			upVec.y = upWallIsDown;
			var downVec = downWall.position;
            downVec.y = downWallIsUp;
			upWall.position = upVec;
            downWall.position = downVec;
        }

        public void Interaction(bool _isUp)
        {
            if (isMoving)
            {
                return;
            }

            isUp = _isUp;
            isMoving = true;
            
            InteractionPointWallMoving();
        }

        private void InteractionPointWallMoving()
        {
            upWall.gameObject.SetActive(true);
            downWall.gameObject.SetActive(true);
            if (isUp)
			{
				var upVec = upWall.position;
				upVec.y = upWallIsDown;
				var downVec = downWall.position;
				downVec.y = downWallOrigin;

				upWall.position = upVec;
                downWall.position = downVec;   
                upWall.DOMoveY(upWallOrigin, 3f).OnComplete(() => PlayerTeleport());
            }
            else
			{
				var upVec = upWall.position;
				upVec.y = upWallOrigin;
				var downVec = downWall.position;
				downVec.y = downWallIsUp;


				upWall.position = upVec;
                downWall.position = downVec;
                downWall.DOMoveY(downWallOrigin, 3f).OnComplete(() => PlayerTeleport());
            }
        }

        private void PlayerTeleport()
        {
            Vector3 newPosition = isUp ? CalcLocalPos(upHouse, downHouse) : CalcLocalPos(downHouse, upHouse);
            Vector3 newCamPosition = Camera.main.transform.position;
            PlayerObj.Player.transform.position = newPosition;
            Camera.main.transform.position = newCamPosition;
            EndPointWallMoving();
        }
        
        private void EndPointWallMoving()
        {
            if (isUp)
            {
                downWall.DOMoveY(downWallIsUp, 7f).OnComplete(() => ResetIsMoving());   
            }
            else
            {
                upWall.DOMoveY(upWallIsDown, 7f).OnComplete(() => ResetIsMoving());   
            }
        }
        

        private void ResetIsMoving()
        {
            isMoving = false;
            upWall.gameObject.SetActive(false);
            downWall.gameObject.SetActive(false);
        }

        private Vector3 CalcLocalPos(Transform house, Transform moveHouse)
        {
            Vector3 differencePos = house.position - PlayerObj.Player.transform.position;
            Vector3 newPos = moveHouse.position + differencePos;
            return newPos;
		}
    }
}

