using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EquipmentSystem
{
    public class CharacterEquipment : MonoBehaviour
    {
        public readonly Dictionary<int, Transform> baseBoneInfos = new Dictionary<int, Transform>();

        private readonly Transform boneTransform;

        public CharacterEquipment(GameObject boneCharacter)
        {
            boneTransform = boneCharacter.transform;
            SetCharacterBone(boneTransform);
        }

        private void SetCharacterBone(Transform _transform)
        {
            foreach (Transform _childBone in _transform)
            {
                baseBoneInfos.Add(_childBone.name.GetHashCode(), _childBone);

                SetCharacterBone(_childBone);
            }
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
                tempObjs[i] = baseBoneInfos[boneNameLists[i].GetHashCode()];
            }

            newRenderer.bones = tempObjs;
            newRenderer.sharedMesh = skinnedMeshRenderer.sharedMesh;
            newRenderer.materials = skinnedMeshRenderer.sharedMaterials;

            return retBoneObj;
        }

        public Transform[] setMeshItem(GameObject meshObj)
        {
            Transform[] retMeshItems = setMeshObj(meshObj.GetComponentsInChildren<MeshRenderer>());
            return retMeshItems;
        }

        private Transform[] setMeshObj(MeshRenderer[] meshRenderers)
        {
            List<Transform> retMeshObjs = new List<Transform>();
            foreach (MeshRenderer meshRenderer in meshRenderers)
            {
                if (meshRenderer.transform.parent != null)
                {
                    Transform parentBone = baseBoneInfos[meshRenderer.transform.parent.name.GetHashCode()];
                    GameObject itemObj = GameObject.Instantiate(meshRenderer.gameObject, parentBone);
                    retMeshObjs.Add(itemObj.transform);
                }
            }
            return retMeshObjs.ToArray();
        }
    }
}