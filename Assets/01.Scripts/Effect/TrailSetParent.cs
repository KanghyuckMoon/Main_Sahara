using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSetParent : MonoBehaviour
{
    [SerializeField] private float delay;
    private Coroutine coroutine;
    
    public void SetParentNull()
    {
        //if (coroutine is not null)
        //{
        //    StopCoroutine(coroutine);
        //    coroutine = null;
        //}
        //coroutine = StartCoroutine(ISetParentNull());
        Invoke("InvokeSetParentNull", delay);
    }

    private IEnumerator ISetParentNull()
    {
        yield return new WaitForSeconds(delay); 
        gameObject.transform.SetParent(null);
    }

    private void InvokeSetParentNull()
    { 
        gameObject.transform.SetParent(null);
    }
    
}
