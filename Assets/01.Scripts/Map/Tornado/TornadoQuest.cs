using System.Collections;
using System.Collections.Generic;
using CutScene;
using UnityEngine;

namespace Map
{
    public class TornadoQuest : MonoBehaviour
    {
        [SerializeField] private TornadoQuestSO tornadoQuestSO;
        [SerializeField] private CutSceneHaver cutSceneHaver;
        
        public void AddTornadoCount()
        {
            tornadoQuestSO.tornadoCount++;
            if (tornadoQuestSO.tornadoCount >= 2)
            {
                CutScenePlay();
            }
        }

        public void CutScenePlay()
        {
            cutSceneHaver.PlayCutScene();
        }
    }
}
