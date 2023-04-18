using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HitBox
{
    [CreateAssetMenu(fileName = "HitboxAdder", menuName = "SOEditor/HitboxAdder")]
    public class HitboxAdder : ScriptableObject
    {
        public List<HitBoxDatasSO> hitBoxDatasSOList = new List<HitBoxDatasSO>();
        public List<HitBoxData> hitboxDataList = new List<HitBoxData>();
        public string hitboxStr;

        [ContextMenu("AddData")]
        public void AddData()
		{
            foreach(var obj in hitBoxDatasSOList)
			{
                foreach (var hitboxData in hitboxDataList)
                {
                    obj.UploadHitBox(hitboxData);
                }
            }
		}


        [ContextMenu("DeleteData")]
        public void DeleteData()
        {
            foreach (var obj in hitBoxDatasSOList)
            {
                obj.RemoveDic(hitboxStr);
            }
        }

    }

}
