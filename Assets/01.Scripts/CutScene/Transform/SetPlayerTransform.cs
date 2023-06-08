using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Module;

namespace CutScene
{
    public class SetPlayerTransform : MonoBehaviour
    {
        private Scene originScene;
        [SerializeField] private Transform targetTrm;
    
        public void PlayerTransformTarget()
        {
            originScene = PlayerObj.Player.scene;
            var _module = PlayerObj.Player.GetComponent<AbMainModule>();
            _module.ObjDir = Vector2.zero;
            _module.ObjDirection = Vector2.zero;
            PlayerObj.Player.transform.SetParent(targetTrm);
        }
        
        
        public void PlayerTransformNull()
        {
            PlayerObj.Player.transform.SetParent(null);
            SceneManager.MoveGameObjectToScene(PlayerObj.Player, originScene);
        }

        public void SetPlayerCollider(bool truefalse)
        {
            var module = PlayerObj.Player.GetComponent<AbMainModule>();
            module.CharacterController.enabled = truefalse;
        }
        
        public void SetPlayerHeight(float height)
        {
            var pos = PlayerObj.Player.transform.position;
            pos.y = transform.position.y + height;
            PlayerObj.Player.transform.position = pos;
        }
    }
   
}