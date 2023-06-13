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
        [SerializeField] private float upHeightPos;
        [SerializeField] private float downHeightPos;
        
        [SerializeField] private Transform upWall;
        [SerializeField] private Transform downWall;
        
        [SerializeField] private float upWallHeightPos_Down;
        [SerializeField] private float downWallHeightPos_Down;
        [SerializeField] private float upWallHeightPos_Up;
        [SerializeField] private float downWallHeightPos_Up;

        [SerializeField]
        private float originUpWallHeight_Up;
        [SerializeField]
        private float originUpWallHeight_Down;
        [SerializeField]
        private float originDownWallHeight_Up;
        [SerializeField]
        private float originDownWallHeight_Down;
        
        private bool isMoving = false;
        private bool isUp;

        void Start()
        {
            Vector3 upPos = upWall.position;
            upPos.y = originUpWallHeight_Down;
            Vector3 downPos = downWall.position;
            downPos.y = originDownWallHeight_Down;

            upWall.position = upPos;
            downWall.position = downPos;
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
                Vector3 upPos = upWall.position;
                upPos.y = originUpWallHeight_Down;
                Vector3 downPos = downWall.position;
                downPos.y = originDownWallHeight_Down;

                upWall.position = upPos;   
                downWall.position = downPos;   
                upWall.DOMoveY(upWallHeightPos_Down, 1f).OnComplete(() => PlayerTeleport());
            }
            else
            {
                Vector3 upPos = upWall.position;
                upPos.y = originUpWallHeight_Up;
                Vector3 downPos = downWall.position;
                downPos.y = originDownWallHeight_Up;
                
                upWall.position = upPos;   
                downWall.position = downPos;
                downWall.DOMoveY(downWallHeightPos_Up, 1f).OnComplete(() => PlayerTeleport());
            }
        }

        private void PlayerTeleport()
        {
            Vector3 newPosition = PlayerObj.Player.transform.position;
            Vector3 newCamPosition = Camera.main.transform.position;
            if (isUp)
            {
                newPosition.y = downHeightPos;
                newCamPosition.y = downHeightPos;
            }
            else
            {
                newPosition.y = upHeightPos;   
                newCamPosition.y = upHeightPos;
            }

            PlayerObj.Player.transform.position = newPosition;
            Camera.main.transform.position = newCamPosition;
            EndPointWallMoving();
        }
        
        private void EndPointWallMoving()
        {
            if (isUp)
            {
                downWall.DOMoveY(downWallHeightPos_Down, 5f).OnComplete(() => ResetIsMoving());   
            }
            else
            {
                upWall.DOMoveY(upWallHeightPos_Up, 5f).OnComplete(() => ResetIsMoving());   
            }
        }
        

        private void ResetIsMoving()
        {
            isMoving = false;
            upWall.gameObject.SetActive(false);
            downWall.gameObject.SetActive(false);
        }
    }
}

