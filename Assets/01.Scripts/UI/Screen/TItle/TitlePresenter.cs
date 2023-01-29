using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using LoadScene;
using UnityEngine.SceneManagement; 
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
                    SceneManager.LoadScene("LoadingScene", LoadSceneMode.Single);
                    //LoadSceneAddressableStatic.LoadSceneAsync("InGameLoadScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
                });
           
            titleView.AddButtonEventToDic(TitleView.Buttons.end_button, () => Debug.Log("Á¾·á"));

        }
        void Start()
        {
            titleView.Init();
        }

    }

}
