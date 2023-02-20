using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EquipmentSystem;

namespace Module {
    public class EquipmentModule : AbBaseModule
    {
        //public Dictionary<>
        private BoneItem[] equipmentItem = new BoneItem[4]; //아이템 종류들

        private CharacterEquipment characterEquipment = new CharacterEquipment();

        private readonly Transform boneTransform;

        public EquipmentModule(AbMainModule _mainModule) : base(_mainModule)
        {
            boneTransform = mainModule.VisualObject.transform;
            SetCharacterBone(boneTransform);
        }

        public override void Start()
        {
            //mainModule.visualObject
        }

        private void SetCharacterBone(Transform _transform)
        {
            foreach(Transform _childBone in _transform)
            {
                characterEquipment.baseBoneInfos.Add(_childBone.name.GetHashCode(), _childBone);

                SetCharacterBone(_childBone);
            }
        }

        public void SetEquipmentItem()
        {

        }

        public Transform setBoneItem(GameObject boneObj, List<string> boneNameLists)
        {
            Transform retBoneItem = setBoneObj(boneObj.GetComponentInChildren<SkinnedMeshRenderer>(), boneNameLists);
            retBoneItem.SetParent(boneTransform);

            return retBoneItem;
        }

        private Transform setBoneObj(SkinnedMeshRenderer skinnedMeshRenderer, List<string> boneNameLists)
        {
            Transform retBoneObj = new GameObject().transform;

            SkinnedMeshRenderer newRenderer = retBoneObj.gameObject.AddComponent<SkinnedMeshRenderer>();

            Transform[] tempObjs = new Transform[boneNameLists.Count];
            for (int i = 0; i < boneNameLists.Count; ++i)
            {
                tempObjs[i] = characterEquipment.baseBoneInfos[boneNameLists[i].GetHashCode()];
            }

            newRenderer.bones = tempObjs;
            newRenderer.sharedMesh = skinnedMeshRenderer.sharedMesh;
            newRenderer.materials = skinnedMeshRenderer.sharedMaterials;

            return retBoneObj;

        }


        private BoneItem equipItemSkinned(EquipmentItem itemObj)
        {
            if (itemObj == null)
            {
                return null;
            }

            Transform itemInfo = setBoneItem(itemObj.objModelPrefab, itemObj.boneNameLists);

            BoneItem boneItem = itemInfo.gameObject.AddComponent<BoneItem>();
            if (boneItem != null)
            {
                boneItem.itemLists.Add(itemInfo);
            }

            return boneItem;
        }
    }
}