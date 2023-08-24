using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Editor/MonsterMeshSO")]
public class MonsterMeshSO : ScriptableObject
{
	public List<Mesh> meshList = new List<Mesh>();
}
