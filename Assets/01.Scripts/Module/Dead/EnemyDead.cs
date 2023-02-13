using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utill.Pattern;
using Pool;

namespace Module
{
    public class EnemyDead : MonoBehaviour, Observer
    {
        public string key;

        private AbMainModule abMainModule;


        private void Start()
        {
            abMainModule = GetComponent<AbMainModule>();
            abMainModule.AddObserver(this);
        }

        public void Receive()
        {
            if (abMainModule.IsDead)
            {
                ObjectPoolManager.Instance.RegisterObject(key, gameObject);
                gameObject.SetActive(false);
            }
        }
    }
}