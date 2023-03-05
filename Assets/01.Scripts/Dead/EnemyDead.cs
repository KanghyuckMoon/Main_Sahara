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
        private string enemyKey = "NULL";

        [SerializeField]
        private string questClearKey = "NULL";

        [SerializeField]
        private string deadSkinEffectKey = "NULL";

        [SerializeField]
        private string deadExplosionEffectKey = "NULL";

        [SerializeField]
        private SkinnedMeshRenderer skinnedMeshRenderer;

        [SerializeField]
        private Transform rootTransform;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private string dropItemKey;

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
            yield return new WaitForSeconds(0.1f);
            animator.speed = 0;
            EffectManager.Instance.SetEffectDefault(deadExplosionEffectKey, transform.position, transform.rotation);
            EffectManager.Instance.SetEffectSkin(deadSkinEffectKey, skinnedMeshRenderer, transform, rootTransform, gameObject.scene);
            ItemDrop(dropItemKey);
            yield return new WaitForSeconds(0.2f);
            abMainModule.Model.gameObject.SetActive(false);
            yield return new WaitForSeconds(3f);
            animator.speed = 1;
            abMainModule.Model.gameObject.SetActive(true);
            ObjectPoolManager.Instance.RegisterObject(enemyKey, gameObject);
            gameObject.SetActive(false);
        }

        private void ItemDrop(string _key)
        {
            if (_key is null || _key == "")
            {
                return;
            }
            GameObject _dropObj = ObjectPoolManager.Instance.GetObject(_key);
            _dropObj.transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
            _dropObj.SetActive(true);
        }

    }
}