using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CutScene
{
    public class SetPlayerTransform : MonoBehaviour
    {
        private Scene originScene;
        [SerializeField] private Transform targetTrm;
    
        public void PlayerTransformTarget()
        {
            originScene = PlayerObj.Player.scene;
            PlayerObj.Player.transform.SetParent(targetTrm);
        }
        
        
        public void PlayerTransformNull()
        {
            PlayerObj.Player.transform.SetParent(null);
            SceneManager.MoveGameObjectToScene(PlayerObj.Player, originScene);
        }
    }
   
}