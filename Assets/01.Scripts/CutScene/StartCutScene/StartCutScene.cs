using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using GoogleSpreadSheet;
using TMPro;
using UnityEngine.SceneManagement;

namespace CutScene
{
    public class StartCutScene : MonoBehaviour
    {
        [SerializeField]
        private List<StartCutSceneData> movePositionList = new List<StartCutSceneData>();

        [SerializeField] 
        private Transform targetObj;
        [SerializeField] 
        private Transform targetObj2;

        [SerializeField] 
        private TextMeshProUGUI targetText;

        [SerializeField] 
        private Image fadeImage;

        [SerializeField] private float shakePower = 1f;
        
        private void Start()
        {
            Sequence _sequence = DOTween.Sequence();

            foreach (var _data in movePositionList)
            {
                //_sequence.Append(targetObj2.DOShakePosition(4.5f, new Vector3(shakePower, shakePower, 0f)));
                _sequence.Append(targetObj.DOMove(_data.pos, 5f)).Join(targetObj2.DOShakePosition(4.5f, new Vector3(shakePower, 0, 0f))).AppendCallback(() => {targetText.text = TextManager.Instance.GetText(_data.textKey); });
                _sequence.Append(targetText.DOFade(1, 5f));
                _sequence.AppendInterval(_data.delay);
                _sequence.Append(targetText.DOFade(0, 1f));
            }

            _sequence.Append(fadeImage.DOFade(1, 1f));
            _sequence.OnComplete(() => GameStart());
            
            _sequence.Play();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameStart();
            }
        }

        private void GameStart()
        {
            SceneManager.LoadScene("LoadingScene", LoadSceneMode.Single);
        }
    }

    [Serializable]
    public class StartCutSceneData
    {
        public Vector3 pos;
        public float delay;
        public string textKey;
    }
}
