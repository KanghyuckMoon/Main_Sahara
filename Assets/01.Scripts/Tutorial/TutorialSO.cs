using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.SeralizableDictionary;

namespace Tutorial
{
    [CreateAssetMenu(fileName = "TutorialSO", menuName = "SO/TutorialSO")]
    public class TutorialSO : ScriptableObject
    {
        public StringString tutorialKeyDic = new StringString();
    }
}
