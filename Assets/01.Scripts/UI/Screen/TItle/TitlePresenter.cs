using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        }
        void Start()
        {
            titleView.Init();
        }

    }

}
