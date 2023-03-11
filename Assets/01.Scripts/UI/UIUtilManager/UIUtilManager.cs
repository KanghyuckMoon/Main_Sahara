using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Utill.Pattern;

namespace UI.UtilManager
{
    public class UIUtilManager : MonoSingleton<UIUtilManager>
    {
        private const string TransStr = "<alpha=#00>"; // 투명 문자 

        public void AnimateText(Label _targetLabel, string _fullText)
        {
            StartCoroutine(AnimateTextCo(_targetLabel,_fullText)); 
        }
        
        private IEnumerator AnimateTextCo(Label _targetLabel, string _fullText)
        {
            string _targetText;
            WaitForSeconds _w = new WaitForSeconds(0.03f); 
            for (int i = 0; i <= _fullText.Length; i++)
            {
                _targetText = _fullText.Substring(0, i) + TransStr + _fullText.Substring(i);

                _targetLabel.text = _targetText;
                yield return _w;
            }
        }
    }

}
