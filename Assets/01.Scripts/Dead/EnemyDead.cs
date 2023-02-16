using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utill.Pattern;
using Pool;
using Quest;

namespace Module
{
    public class EnemyDead : MonoBehaviour, Observer
    {
        [SerializeField]
        private string enemyKey;

        [SerializeField]
        private string questClearKey;

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
                if(questClearKey is not null)
				{
                    QuestManager.Instance.ChangeQuestClear(questClearKey);
                }
                StartCoroutine(IDead());
            }
        }

        private IEnumerator IDead()
        {
            yield return new WaitForSeconds(2f);
            ObjectPoolManager.Instance.RegisterObject(enemyKey, gameObject);
            gameObject.SetActive(false);
        }

    }
}