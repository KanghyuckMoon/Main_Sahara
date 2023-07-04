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
        private TextMeshProUGUI targetText;

        public void SetText(string _textKey)
        {
            targetText.text = TextManager.Instance.GetText(_textKey);
            targetText.DOFade(1, 5f).OnComplete(() => targetText.DOFade(0, 1f));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameStart();
            }
        }

        public void GameStart()
        {
            //SceneManager.LoadScene("LoadingScene", LoadSceneMode.Single);
            SceneManager.LoadScene("InGame", LoadSceneMode.Single);
        }
    }
}
