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
        // 디버그용
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

        [SerializeField, Header("False면 머리 위에 Hud 뜨도록")]
        private bool isPlayerHud;

        private VisualElement hudElement;
        private PresenterFollower presenterFollower;
        [SerializeField]
        private UIModule uiModule; 
        [SerializeField]
        private StateModule stateModule;

        private List<IUIFollower> _presenterList = new List<IUIFollower>();

        // 프로퍼티 
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
                    Logging.Log("타겟 찾는중..");
                    if (target != null)
                    {
                        Logging.Log("타겟렌더러 찾는중..");
                        targetRenderer = target?.GetComponentInChildren<Renderer>();
                    }
                    else return null;
                }
                Logging.Log("타겟 반환");
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
            if (presenterFollower == null && isPlayerHud == false) // 머리 위에 hud 띄워야해 
            {
                presenterFollower = new PresenterFollower(this, hudElement, target, targetRenderer);
            }
        }
        private void LateUpdate()
        {
            if (presenterFollower != null)
            {
                presenterFollower.UpdateUI();
                Debug.Log("따라가는중");
            }

        }
        [ContextMenu("테스트")]
        public void UpdateUI()
        {
            foreach (var p in _presenterList)
            {
                p.UpdateUI();
            }
        }

        /// <summary>
        /// 활성화 여부 액티브 
        /// </summary>
        private void UpdateUIActive()
        {
            hudElement.style.display = uiModule.IsRender ? DisplayStyle.Flex : DisplayStyle.None; 
        }

        /// <summary>
        /// hud 활성화 비활성화 
        /// </summary>
        public void SetActive(bool _isActive)
        {
            hudElement.style.display = _isActive == true ? DisplayStyle.Flex : DisplayStyle.None;
        }

        /// <summary>
        /// Presenter 생성
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

        [ContextMenu("버프 아이콘 생성")]
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
        //        //stateData = transform.parent.GetComponentInChildren<AbMainModule>().GetModuleComponent<UIModule>(ModuleType.UI).stateData; // 부모 오브젝트의 데이터 가져오기 
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

