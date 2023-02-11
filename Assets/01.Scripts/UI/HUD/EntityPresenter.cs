using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Module;
using Utill.Measurement; 

namespace UI
{
    public class EntityPresenter : MonoBehaviour, IUIOwner, Observer
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

        [SerializeField, Header("False�� �Ӹ� ���� Hud �ߵ���")]
        private bool isPlayerHud;

        private VisualElement hudElement;
        private PresenterFollower presenterFollower;
        [SerializeField]
        private UIModule uiModule; 
        [SerializeField]
        private StateModule stateModule;

        private List<IUIFollower> _presenterList = new List<IUIFollower>();

        // ������Ƽ 
        public UIDocument Root { get; set; }
        public UIDocument RootUIDocument => uiDocument;
        public List<IUIFollower> PresenterList => _presenterList;
        private Transform Target
        {
            get
            {
                if (target == null)
                {
                    //target = GetComponentInParent<Transform>();
                    target = transform.parent; 
                    Logging.Log("Ÿ�� ã����..");
                    if (target != null)
                    {
                        Logging.Log("Ÿ�ٷ����� ã����..");
                        targetRenderer = target?.GetComponentInChildren<Renderer>();
                    }
                    else return null;
                }
                Logging.Log("Ÿ�� ��ȯ");
                return target;

            }
        }

        public void DebugTest()
        {
            hpPresenter.Test1(); 
        }
        public void DebugTest2()
        {
            hpPresenter.Test2();
        }

        private void OnEnable()
        {
            uiDocument ??= GetComponent<UIDocument>();
            hudElement = uiDocument.rootVisualElement.ElementAt(0);

            ContructPresenters();
            AwakePresenters();
            StartCoroutine(Init());

        }
        public void Awake()
        {

        }
        public void Start()
        {
        }
        private void Update()
        {
            if (Target == null || targetRenderer == null) return;

            //if(Input.GetKeyDown(KeyCode.Tab))
            //{
            //    DebugTest(); 
            //}

            //if (Input.GetKeyDown(KeyCode.CapsLock))
            //{
            //    DebugTest2(); 
            //}
            //if (Input.GetKeyDown(KeyCode.Backspace))
            //{
            //    hpPresenter.Test3();
            //}
            if (presenterFollower == null && isPlayerHud == false) // �Ӹ� ���� hud ������� 
            {
                presenterFollower = new PresenterFollower(this, hudElement, target, targetRenderer);
            }
        }
        private void LateUpdate()
        {
            if (presenterFollower != null)
            {
                presenterFollower.UpdateUI();
                Debug.Log("���󰡴���");
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
        /// Ȱ��ȭ ���� ��Ƽ�� 
        /// </summary>
        private void UpdateUIActive()
        {
            hudElement.style.display = uiModule.IsRender ? DisplayStyle.Flex : DisplayStyle.None; 
        }

        /// <summary>
        /// hud Ȱ��ȭ ��Ȱ��ȭ 
        /// </summary>
        public void SetActive(bool _isActive)
        {
            hudElement.style.display = _isActive == true ? DisplayStyle.Flex : DisplayStyle.None;
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
                //p.Start(stateData);
                p.Start(stateModule); 
            }
        }

        [ContextMenu("���� ������ ����")]
        public void TestCreateBuffIcon()
        {
            buffPresenter.CreateBuffIcon();
        }


        IEnumerator Init()
        {
            while (stateModule == null)
            {
                this.uiModule = transform.parent.GetComponentInChildren<AbMainModule>().GetModuleComponent<UIModule>(ModuleType.UI);
                this.stateModule = transform.parent.GetComponentInChildren<AbMainModule>().GetModuleComponent<StateModule>(ModuleType.State);
                if (stateModule != null)
                {
                    StartPresenters();
                    this.stateModule.AddObserver(this);
                    this.uiModule.AddObserver(this);
                }
                yield return null;
            }
        }
        //IEnumerator Init()
        //{
        //    while (stateData == null)
        //    {
        //        //stateData = transform.parent.GetComponentInChildren<AbMainModule>().GetModuleComponent<UIModule>(ModuleType.UI).stateData; // �θ� ������Ʈ�� ������ �������� 
        //        if (stateData != null)
        //        {
        //            StartPresenters();
        //        }
        //        yield return null;
        //    }
        //}

        public void Receive()
        {
            UpdateUI();
            UpdateUIActive(); 
        }
    }

}

