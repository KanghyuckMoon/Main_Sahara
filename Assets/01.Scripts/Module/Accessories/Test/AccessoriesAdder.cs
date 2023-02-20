using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

public class AccessoriesAdder : MonoBehaviour
{
	public AccessoriesItemType accessoriesItemType;

	[ContextMenu("AddItem")]
	public void AddItem()
	{
		var mainModule = GetComponent<AbMainModule>();
		var itemModuel = mainModule.GetModuleComponent<ItemModule>(ModuleType.Item);
		itemModuel.SetPassiveItem(accessoriesItemType);
	}
}
