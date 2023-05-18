using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effect
{
    [CreateAssetMenu(fileName = "AnimationEffectSOChanger", menuName = "SOEditor/AnimationEffectSOChanger")]
    public class AnimationEffectSOChanger : ScriptableObject
    {
        [SerializeField] private List<AnimationEffectSO> animationEffectSOList = new List<AnimationEffectSO>();
        [SerializeField] private List<string> effectDataKeyList = new List<string>();
        [SerializeField] private List<EffectData> effectDataList = new List<EffectData>();
        
        [ContextMenu("Upload")]
        public void Upload()
        {
            int i = 0;
            foreach (var so in animationEffectSOList)
            {
                i = 0;
                foreach (var effectData in effectDataList)
                {
                    so.UploadEffectData(effectDataKeyList[i], effectData);
                    ++i;
                }
            }
        }
        
        
        
    }
}
