using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTOD : MonoBehaviour
{
    [SerializeField] private TODSO todso;

    public void SetIsUpdateOn(bool _isUpdateOn)
    {
        todso.SetIsUpdateOn(_isUpdateOn);
    }
}
