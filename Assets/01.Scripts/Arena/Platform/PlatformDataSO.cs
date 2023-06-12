using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "SO/Arena/PlatformSO")]
public class PlatformDataSO : ScriptableObject
{
    public float tweenDuration; 
    public float startDelay;
    public float actionDelay; 
}
