using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Effect;
using DG.Tweening;

namespace Interaction
{
    public class TeleportInteraction : MonoBehaviour, IInteractionItem
    {
        
        
        public bool Enabled
        {
            get
            {
                return true;
            }
            set
            {

            }
        }

        public string Name
        {
            get
            {
                return nameKey;
            }
        }

        public Vector3 PopUpPos
        {
            get
            {
                return transform.position + new Vector3(0, 1, 0);
            }
        }

        public string ActionName
        {
            get
            {
                return "O00000050";
            }
        }

        [SerializeField] private string nameKey = "M00000010";
        [SerializeField] private float heightPos;
        [SerializeField] private Transform upWall;
        [SerializeField] private Transform downWall;
        [SerializeField] private float upWallHeightPos;
        [SerializeField] private float downWallHeightPos;

        private Vector3 originUpWallPos;
        private Vector3 originDownWallPos;
        private bool isMoving = false;

        void Start()
        {
            originUpWallPos = upWall.position;
            originDownWallPos = downWall.position;
        }

        public void Interaction()
        {
            if (isMoving)
            {
                return;
            }

            isMoving = true;
            
            UpWallMoving();
        }

        private void UpWallMoving()
        {
            Vector3 newPosition = downWall.transform.position;
            newPosition.y = downWallHeightPos;
            downWall.position = newPosition;
            upWall.DOMoveY(upWallHeightPos, 1f).OnComplete(() => PlayerTeleport());
        }

        private void PlayerTeleport()
        {
            Vector3 newPosition = PlayerObj.Player.transform.position;
            newPosition.y = heightPos;
            PlayerObj.Player.transform.position = newPosition;
            DownWallMoving();
        }

        private void DownWallMoving()
        {
            Vector3 newPosition = upWall.transform.position;
            newPosition.y = upWallHeightPos;
            upWall.position = newPosition;
            downWall.DOMoveY(downWallHeightPos, 5f).OnComplete(() => ResetIsMoving());
        }

        private void ResetIsMoving()
        {
            isMoving = false;
        }
    }
}

