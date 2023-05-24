using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;
using Utill.Pattern;

namespace UI.UtilManager
{
    public class UIUtilManager : MonoSingleton<UIUtilManager>
    {
        private const string TransStr = "<alpha=#00>"; // 투명 문자 

        private Dictionary<Label,IEnumerator> changedLabelDic = new Dictionary<Label,IEnumerator>(); 
        // 현재 변경중인 라벨 저장 리스트 진행중인데 또 진행하면 끄고 진행   

        private string styleStr;  
        public void AnimateText(Label _targetLabel, string _fullText, float _time = 0.03f)
        {
            //StopAllCoroutines();
            if (changedLabelDic.ContainsKey(_targetLabel))
            {
                StopCoroutine(changedLabelDic[_targetLabel]);
                changedLabelDic.Remove(_targetLabel); 
            }
            IEnumerator _co = AnimateTextCo(_targetLabel,_fullText,_time);
            changedLabelDic.Add(_targetLabel,_co);
            
            StartCoroutine(_co); 
            
        }

        private bool isRIchText = false;

        private IEnumerator AnimateTextCo(Label _targetLabel, string _fullText,float _time = 0.03f)
        {
            if (_fullText == null) yield break;
            
            _targetLabel.text = String.Empty;
            
            string _targetText;
            WaitForSeconds _w = new WaitForSeconds(_time); 
            for (int i = 0; i < _fullText.Length; i++)
            {
                Debug.Log("@@@@@@" +_fullText[i]);

                if (isRIchText == true)
                {
                    if (_fullText[i] == '>')
                    {
                        isRIchText = false;
                    }
                    continue; 
                }
                if (_fullText[i] == '<')
                {
                    isRIchText = true; 
                    continue;
                }
                _targetText = _fullText.Substring(0, i) + TransStr + _fullText.Substring(i);

                _targetLabel.text = _targetText;
                yield return _w;
            }
            changedLabelDic.Remove(_targetLabel);
        }
    }

}
