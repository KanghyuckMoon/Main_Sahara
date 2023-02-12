using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effect
{
    [CreateAssetMenu(menuName = "SO/PlayerLandEffectSO")]
    public class PlayerLandEffectSO : ScriptableObject
    {
        public string landEffectName;
        public string walkRffectName;
        public string walkLffectName;
        public string runREffectName;
        public string runLEffectName;

        [Space]
        public float effectDelayTime;
        public float runEffectDelay;
        //ad
    }
}