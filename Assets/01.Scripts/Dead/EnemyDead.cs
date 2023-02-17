using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utill.Pattern;
using Pool;
using Quest;
using Effect;

namespace Module
{
    public class EnemyDead : MonoBehaviour, Observer
    {
        [SerializeField]
        private string enemyKey;

        [SerializeField]
        private string questClearKey = "NULL";

        [SerializeField]
        private string deadSkinEffectKey;

        [SerializeField]
        private string deadExplosionEffectKey;

        [SerializeField]
        private SkinnedMeshRenderer skinnedMeshRenderer;

        [SerializeField]
        private Transform rootTransform;

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
                if(questClearKey is not "NULL")
				{
                    QuestManager.Instance.ChangeQuestClear(questClearKey);
                }
                StartCoroutine(IDead());
            }
        }

        private IEnumerator IDead()
        {
            yield return new WaitForSeconds(0.5f);
            EffectManager.Instance.SetEffectDefault(deadExplosionEffectKey, transform.position, transform.rotation);
            EffectManager.Instance.SetEffectSkin(deadSkinEffectKey, skinnedMeshRenderer, rootTransform);
            ObjectPoolManager.Instance.RegisterObject(enemyKey, gameObject);
            gameObject.SetActive(false);
        }

    }
}