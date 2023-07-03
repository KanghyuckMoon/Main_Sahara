using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using GoogleSpreadSheet;

namespace UI
{
    public class TitlePresenter : MonoBehaviour
    {
        [SerializeField]
        private UIDocument uiDocument;

        [SerializeField]
        private TitleView titleView;
        
        private void Awake()
        {
            uiDocument ??= GetComponent<UIDocument>();

            titleView.InitUIDocument(uiDocument);
        }
        private void OnEnable()
        {
            titleView.Cashing();
            titleView.AddButtonEventToDic(TitleView.Buttons.start_button,
                () =>
                {
                    SceneManager.LoadScene("StartCutScene", LoadSceneMode.Single);
                });
           
            titleView.AddButtonEventToDic(TitleView.Buttons.end_button, Application.Quit);

        }

        private void OnDisable()
        {
            titleView.RemoveButtonEvents(); 
        }

        IEnumerator Start()
        {
            while(TextManager.Instance.IsInit is false)
            {
                yield return null; 
            }
            titleView.Init();

        }

    }

}
