using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Sound;
using UnityEngine;
using UnityEngine.UIElements;
using Utill.Addressable;
using Utill.Pattern;
using Utill.Measurement;

namespace UI.UtilManager
{
    public class UIUtilManager : MonoSingleton<UIUtilManager>
    {
        private UISoundSO uiSoundSO;
        private const string TransStr = "<alpha=#00>"; // 투명 문자 

        private Dictionary<Label,IEnumerator> changedLabelDic = new Dictionary<Label,IEnumerator>(); 
        // 현재 변경중인 라벨 저장 리스트 진행중인데 또 진행하면 끄고 진행   

        private string styleStr;
        public override void Awake()
        {
            base.Awake();
            uiSoundSO = AddressablesManager.Instance.GetResource<UISoundSO>("UISoundSO");
        }

        /// <summary>
        /// UI사운드 재생 
        /// </summary>
        /// <param name="_type"></param>
        public void PlayUISound(UISoundType _type)
        {
            string _address = uiSoundSO.GetSoundAddress(_type);
            if (string.IsNullOrEmpty(_address) == false)
            {
                SoundManager.Instance.PlayEFF(_address);
                Logging.Log("@사운드 재생 : " + Enum.GetName(typeof(UISoundType),_type));

            }
            else
            {
                Debug.LogWarning("사운드 주소 없음 : " + Enum.GetName(typeof(UISoundType),_type));
            }
        }
        public void AnimateText(Label _targetLabel, string _fullText, float _time = 0.03f)
        {
            //StopAllCoroutines();
            if (changedLabelDic.ContainsKey(_targetLabel))
            {
                StopCoroutine(changedLabelDic[_targetLabel]);
                changedLabelDic.Remove(_targetLabel); 
            }
            //IEnumerator _co = AnimateTextCo(_targetLabel,_fullText,_time);
            //changedLabelDic.Add(_targetLabel,_co);
            IEnumerator _co = TypeText(_targetLabel,_fullText,_time);
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
                Logging.Log("@@@@@@" +_fullText[i]);

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

        
        
        
        //private int index = 0;
        IEnumerator TypeText(Label _targetLabel, string _fullText,float _time = 0.03f)
        {
            if (string.IsNullOrEmpty(_fullText)) yield break;
            int index = 0;
            _targetLabel.text = String.Empty; 
            
            while (index < _fullText.Length)
            {
                if (_fullText[index] == '<')
                {
                    int tagEndIndex = _fullText.IndexOf('>', index);
                    if (tagEndIndex != -1)
                    {
                        _targetLabel.text += _fullText.Substring(index, tagEndIndex - index + 1);
                        index = tagEndIndex + 1;
                    }
                }
                else
                {
                    _targetLabel.text += _fullText[index];
                    index++;
                    yield return new WaitForSeconds(_time);
                }
            }
            changedLabelDic.Remove(_targetLabel);

        }
        
    }

}
