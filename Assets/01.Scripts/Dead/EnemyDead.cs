using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utill.Pattern;
using Pool;
using Quest;
using Effect;
using Inventory;
using Utill.Random;

namespace Module
{
    public class EnemyDead : MonoBehaviour, Observer, IObserble
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
        private DropItemListSO dropItemListSO;

        private AbMainModule abMainModule;

        private bool isDestroy;
        private bool isDead;
        public bool IsDestroy => isDestroy;
        public bool IsDead => isDead;

        public List<Observer> Observers => observers;
        public List<Observer> observers = new List<Observer>();

        private void Start()
        {
            isDead = false;
            isDestroy = false;
            abMainModule = GetComponent<AbMainModule>();
            abMainModule.AddObserver(this);
        }

        private void OnEnable()
        {
            isDead = false;
            isDestroy = false;
        }

        public void Receive()
        {
            if (abMainModule.IsDead)
            {
                if(isDead || isDestroy)
				{
                    return;
				}

                if(questClearKey is not "NULL")
				{
                    QuestManager.Instance.ChangeQuestClear(questClearKey);
                }
                isDead = true;
                StartCoroutine(IDead());
            }
        }

        private IEnumerator IDead()
        {
            yield return new WaitForSeconds(0.1f);
            animator.speed = 0;
            EffectManager.Instance.SetEffectDefault(deadExplosionEffectKey, transform.position, transform.rotation);
            EffectManager.Instance.SetEffectSkin(deadSkinEffectKey, skinnedMeshRenderer, transform, rootTransform, gameObject.scene);
            //Item Drop
            for (int i = 0; i < dropItemListSO.dropCount; ++i)
            {
                int _index = StaticRandom.Choose(dropItemListSO.randomPercentArr);
                if (dropItemListSO.dropItemKeyArr[_index] is null || dropItemListSO.dropItemKeyArr[_index] is "")
                {
                    continue;
                }
                ItemDrop(dropItemListSO.dropItemKeyArr[_index]); 
            }
            yield return new WaitForSeconds(0.2f);
            abMainModule.Model.gameObject.SetActive(false);
            yield return new WaitForSeconds(3f);
            Send();
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

        private void OnDestroy()
        {
            isDead = true;
            isDestroy = true;
            Send();
        }

        public void AddObserver(Observer _observer)
        {
            observers.Add(_observer);
        }

        public void Send()
        {
            foreach (Observer _observer in observers)
            {
                _observer.Receive();
            }
        }
    }
}