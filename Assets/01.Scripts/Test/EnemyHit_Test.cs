using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace ForTheTest
{
    public class EnemyHit_Test : MonoBehaviour
    {
        [SerializeField] private string hitLayerName;

        private bool hitDelay;

        private void Start()
        {
            hitDelay = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player_Weapon") && hitDelay)
            {
                Debug.Log("맞았엉! 히히 히히 히ㅣㅎ");
                transform.DOShakePosition(0.15f, 0.5f, 180, 160);
                StartCoroutine(DelayHit());
            }
        }

        IEnumerator DelayHit()
        {
            hitDelay = false;
            yield return new WaitForSeconds(2);
            hitDelay = true;
        }
    }
}