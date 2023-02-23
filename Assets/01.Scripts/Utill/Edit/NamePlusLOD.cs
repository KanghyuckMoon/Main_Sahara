#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NamePlusLOD : MonoBehaviour
{
	[ContextMenu("PlayNamePlusLOD")]
	public void PlayNamePlusLOD()
	{
		var path = AssetDatabase.GetAssetPath(gameObject);
		AssetDatabase.RenameAsset(path, $"{gameObject.name}LOD");
	}
	[ContextMenu("ResetNamePlusLOD")]
	public void ResetNamePlusLOD()
	{
		var path = AssetDatabase.GetAssetPath(gameObject);
		AssetDatabase.RenameAsset(path, $"{gameObject.name}");
	}
}
#endif