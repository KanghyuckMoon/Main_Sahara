using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Streaming;
using Pool;

namespace Skill
{

    public class Skill_CallEnemy : WeaponSkillFunctions, IWeaponSkill
    {
        [SerializeField]
        private AnimationClip animationClip;

        [SerializeField]
        private string enemyAddress;
        [SerializeField]
        private ObjectDataSO objectDataSO;
        [SerializeField]
        private float radius = 1f;

        [SerializeField] 
        private LayerMask spawnLayerMask;
        
        private Vector3 spawnPos = Vector3.zero;
        //[SerializeField] private buv
        private bool isSpawnOn = false;
        private bool isSpawn = false;
        
        public void Skills(AbMainModule _mainModule)
        {
            PlaySkillAnimation(_mainModule, animationClip);
            //Invoke("SpawnObj", 1f);;
            isSpawnOn = true;
        }

        private void Update()
        {
            if (isSpawnOn && !isSpawn)
            {
                SpawnObj();
                isSpawn = true;
            }
        }
        
        public void Spawn()
        {
            //ObjectPoolManager.Instance.GetObjectAsync(enemyAddress, SpawnObj);
        }

        private void SpawnObj()
        {
            GameObject _obj = ObjectPoolManager.Instance.GetObject(enemyAddress);
            ObjectClassCycle objectClassCycle = _obj.GetComponentInChildren<ObjectClassCycle>();
            objectClassCycle.TargetObject = _obj;
            ObjectSceneChecker _objectSceneChecker = ClassPoolManager.Instance.GetClass<ObjectSceneChecker>("ObjectSceneChecker");
            if (_objectSceneChecker is null)
            {
                _objectSceneChecker = new ObjectSceneChecker();
            }
            _objectSceneChecker.ObjectDataSO = objectDataSO;
            _objectSceneChecker.ObjectClassCycle = objectClassCycle;
            objectClassCycle.AddObjectClass(_objectSceneChecker);
            GetRandomPos();
            _obj.transform.position = spawnPos;
            _obj.SetActive(true);
        }

        private void GetRandomPos()
        {
            Vector3 _result = new Vector3();

            int count = 10;
            while(count-- > 0)
            {
                float randomPosX = Random.Range(-radius, radius);
                float randomPosZ = Random.Range(-radius, radius);

                Vector3 rayPos = new Vector3(transform.position.x + randomPosX, transform.position.y + 50, transform.position.z +randomPosZ);
                RaycastHit raycastHit;

                if (Physics.Raycast(rayPos, Vector3.down, out raycastHit, 150f, spawnLayerMask))
                {
                    _result = rayPos;
                    _result.y = raycastHit.point.y + 3f;
                    break;
                }
            }

            spawnPos = _result;
        }
    }   
}
