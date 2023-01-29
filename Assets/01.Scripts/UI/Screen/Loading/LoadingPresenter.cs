using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Screens
{
    public class LoadingPresenter : MonoBehaviour
    {
        [SerializeField]
        private UIDocument uiDocument;
        [SerializeField]
        private LoadingView loadingView;

        private void OnEnable()
        {
            loadingView.InitUIDocument(uiDocument); 
            loadingView.Cashing(); 
        }
        private void OnDisable()
        {
            loadingView.StopTween(); 
        }
        private void Awake()
        {
            uiDocument ??= GetComponent<UIDocument>(); 
        }
        private void Start()
        {
            loadingView.LoopLoadingImg(); 
        }
    }

}
