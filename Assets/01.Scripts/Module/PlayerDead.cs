using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Module
{
    public class PlayerDead : MonoBehaviour
    {
        public float sceneMoveTime;
        public string sceneMoveName;

        private AbMainModule abMainModule;


        private void Start()
        {
            abMainModule = GetComponent<AbMainModule>();
        }

        private void Update()
        {
            if(abMainModule.isDead)
            {
                sceneMoveTime -= Time.deltaTime;
                if(sceneMoveTime <= 0)
                {
                    ChangeScene();
                }
            }
        }

        private void ChangeScene()
        {
            SceneManager.LoadScene(sceneMoveName);
        }
    }
}