using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Module;

namespace UI
{
    public class EntityPresenter : MonoBehaviour, IUIOwner
    {
        // ����׿�
        public float a, b;
        [SerializeField]
        private Transform target;
        [SerializeField]
        private Renderer targetRenderer;
        [SerializeField]
        private UIDocument uiDocument;

        [SerializeField]
        private HpPresenter hpPresenter;
        [SerializeField]
        private MpPresenter mpPresenter;
        [SerializeField]
        private BuffPresenter buffPresenter;

        [SerializeField,Header("False�� �Ӹ� ���� Hud �ߵ���")]
        private bool isPlayerHud;

        private VisualElement hudElement;
        private PresenterFollower presenterFollower;
        private StateData stateData;

        private List<IUIFollower> _presenterList = new List<IUIFollower>();
        public UIDocument Root { get; set; }
        public UIDocument RootUIDocument => uiDocument;
        public List<IUIFollower> PresenterList => _presenterList;

        private void OnEnable()
        {
            hudElement = uiDocument.rootVisualElement.ElementAt(0);
            if(isPlayerHud == false) // �Ӹ� ���� hud ������� 
            {
                presenterFollower = new PresenterFollower(this, hudElement, target, targetRenderer);
            }

        }
        public void Awake()
        {
            uiDocument ??= GetComponent<UIDocument>();
            target ??= GetComponentInParent<Transform>();
            targetRenderer ??= target.GetComponentInChildren<Animator>().GetComponentInChildren<Renderer>();
            ContructPresenters();
            AwakePresenters();
        }
        public void Start()
        {
            StartCoroutine(Init());
        }

        private void LateUpdate()
        {
            if (presenterFollower != null)
            {
                presenterFollower.UpdateUI();
            }

        }
        [ContextMenu("�׽�Ʈ")]
        public void UpdateUI()
        {
            foreach (var p in _presenterList)
            {
                p.UpdateUI();
            }
        }

        /// <summary>
        /// Presenter ����
        /// </summary>
        private void ContructPresenters()
        {
            _presenterList.Add(hpPresenter);
            _presenterList.Add(mpPresenter);
            _presenterList.Add(buffPresenter);
        }

        private void AwakePresenters()
        {
            foreach (var p in _presenterList)
            {
                p.RootUIDocument = RootUIDocument;
                p.Awake();
            }
        }
        private void StartPresenters()
        {
            foreach (var p in _presenterList)
            {
                p.Start(stateData);
            }
        }

        [ContextMenu("���� ������ ����")]
        public void TestCreateBuffIcon()
        {
            buffPresenter.CreateBuffIcon();
        }

        IEnumerator Init()
        {
            while (stateData == null)
            {
                stateData = transform.parent.GetComponentInChildren<MainModule>().GetModuleComponent<UIModule>(ModuleType.UI).stateData; // �θ� ������Ʈ�� ������ �������� 
                if (stateData != null)
                {
                    StartPresenters();
                }
                yield return null;
            }
        }
    }

}

