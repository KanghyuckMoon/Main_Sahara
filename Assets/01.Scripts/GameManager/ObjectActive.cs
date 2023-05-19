using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActive : MonoBehaviour
{
    [SerializeField] private GameObject obj;

    private void Start()
    {
        StartCoroutine(ActiveDelay());
    }

    private IEnumerator ActiveDelay()
    {
        while (true)
        {
            if (GameManager.GamePlayerManager.Instance.IsPlaying)
            {
                obj.SetActive(true);
                yield break;
            }
            
            yield return null;
        }
    }
}
