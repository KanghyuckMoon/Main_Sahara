using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;
using Pool;
using Utill.Random;
using HitBox;
using Attack;

public class HitDropItem : MonoBehaviour
{
    [SerializeField]
    private DropItemListSO dropItemListSO;
    
    [SerializeField] 
    private int remainCount = 5;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player_Weapon"))
        {
            InGameHitBox _inGameHitBox = other.GetComponent<InGameHitBox>();
            AttackFeedBack _attackFeedBack = other.GetComponent<AttackFeedBack>();
            _attackFeedBack.InvokeEvent(other.ClosestPoint(transform.position), _inGameHitBox.HitBoxData.hitEffect);

            
            int _random = UnityEngine.Random.Range(1, dropItemListSO.dropCount);
            for (int i = 0; i < _random; ++i)
            {
                int _index = StaticRandom.Choose(dropItemListSO.randomPercentArr);
                if (dropItemListSO.dropItemKeyArr[_index] is null || dropItemListSO.dropItemKeyArr[_index] is "")
                {
                    continue;
                }

                //Vector3 _spawnPos = Vector3.Lerp(other.ClosestPoint(transform.position), other.transform.position, 0.9f);
                
                ItemDrop(dropItemListSO.dropItemKeyArr[_index], other.ClosestPoint(transform.position), other.gameObject.transform.position - transform.position); 
            }

            remainCount -= _random;

            if (remainCount <= 0)
            {
                Break();
            }
        }
    }
    
    
    private void ItemDrop(string _key, Vector3 _pos, Vector3 _dir)
    {
        if (_key is null || _key == "")
        {
            return;
        }
        GameObject _dropObj = ObjectPoolManager.Instance.GetObject(_key);
        _dropObj.transform.position = _pos;
        _dropObj.GetComponent<Rigidbody>().AddForce(_dir * 20, ForceMode.Impulse);
        _dropObj.SetActive(true);
    }

    private void Break()
    {
        gameObject.SetActive(false);
    }
    
}
