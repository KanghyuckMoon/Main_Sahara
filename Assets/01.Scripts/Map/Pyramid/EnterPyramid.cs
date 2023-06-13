using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
public class EnterPyramid : MonoBehaviour
{
    [SerializeField] private MeshRenderer pyramidMesh;
    [SerializeField] private Material outMaterial;
    [SerializeField] private Material inMaterial;
    [SerializeField] private List<GameObject> monsterList = new List<GameObject>();

    public void ChangeOutMaterial()
    {
        pyramidMesh.material = outMaterial;
    }
    
    public void ChangeInMaterial()
    {
        pyramidMesh.material = inMaterial;
    }

    public void SpawnMonsters()
    {
        foreach(var monster in monsterList)
        {
            try
            {
                monster.SetActive(true);
            }
            catch (Exception e)
            {
                continue;
            }
        }
    }
}
}

