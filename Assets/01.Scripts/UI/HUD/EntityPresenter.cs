using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Module;
using Utill.Measurement;
using Data;
using System;

namespace UI
{
    public enum HudType
    {
        statData, 
        buffData,
    }
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

        // 데이터 
        private UIModule uiModule;
        private StatData statData;
        private BuffModule buffModule;

        private List<IUIFollower> _presenterList = new List<IUIFollower>();
        private Dictionary<HudType, List<IUIFollower>> _dataPresenterDic = new Dictionary<HudType, List<IUIFollower>>(); // 데이터 타입, 프레젠터 

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

        private void OnEnable()
        {
            uiDocument ??= GetComponent<UIDocument>();
            hudElement = uiDocument.rootVisualElement.ElementAt(0);
          //  hudElement.style.display = DisplayStyle.None;
            ContructPresenters();
            AwakePresenters();
            StartCoroutine(Init());

        }

        private void OnDisable()
        {
            Clear(); 
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
            if(buffModule != null)
            {
                buffPresenter.Update(); 
            }
        }
        
        private void LateUpdate()
        {
            if (presenterFollower != null)
            {
                presenterFollower.UpdateUI();
                if (hudElement.style.display == DisplayStyle.None)
                {
                    StartCoroutine(ActivePn());
                }
                //Debug.Log("따라가는중");
            }

        }

        [ContextMenu("버프 테스트")]
        public void Test()
        {
            buffModule.TestBuff(); 
        }
        private IEnumerator ActivePn()
        {
            yield return null; 
            hudElement.style.display = DisplayStyle.Flex;
        }
        /// <summary>
        /// 변수 초기화 
        /// </summary>
        private void Clear()
        {
            target = null;
            targetRenderer = null;
            presenterFollower = null;
            isPlayerHud = false;
            statData = null;
            uiModule = null;
            buffModule = null; 
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
            _presenterList.Clear();
            _dataPresenterDic.Clear();

            _presenterList.Add(hpPresenter);
            _presenterList.Add(mpPresenter);
            _presenterList.Add(buffPresenter);

            _dataPresenterDic.Add(HudType.statData, new List<IUIFollower>());
            _dataPresenterDic.Add(HudType.buffData, new List<IUIFollower>());
        
            _dataPresenterDic[HudType.statData].Add(hpPresenter);
            _dataPresenterDic[HudType.statData].Add(mpPresenter);
            _dataPresenterDic[HudType.buffData].Add(buffPresenter);
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
            foreach(var _p in _dataPresenterDic.Keys)
            {
                switch (_p)
                {
                    case HudType.statData:
                        _dataPresenterDic[_p].ForEach((x) => x.Start(statData));
                        break; 
                    case HudType.buffData:
                        _dataPresenterDic[_p].ForEach((x) => x.Start(buffModule));
                        break;
                }
            }
            //foreach (var p in _presenterDic.Keys)
            //{
            //    switch (p)
            //    {
            //        case HudType.hp: case HudType.mp:
            //            _presenterDic[p].Start(statData);
            //            break;
            //        case HudType.buff:
            //            _presenterDic[p].Start(buffModule);
            //            break;
            //    }
            //}

            //foreach (var p in _presenterList)
            //{
            //    //p.Start(stateData);
            //    p.Start(statData);
            //}
        }

        [ContextMenu("버프 아이콘 생성")]
        public void TestCreateBuffIcon()
        {
       //     buffPresenter.CreateBuffIcon();
        }


        IEnumerator Init()
        {
            if(statData != null && uiModule != null)
            {
                StartPresenters();
            }

            while (transform.parent == null)
            {
                yield return null;
            }
            while (transform.parent != null && statData == null)
            {
                AbMainModule _mainModule = transform.parent.GetComponentInChildren<AbMainModule>(); 
                this.uiModule = _mainModule.GetModuleComponent<UIModule>(ModuleType.UI);
                this.buffModule = _mainModule.GetModuleComponent<BuffModule>(ModuleType.Buff);
                this.statData = transform.parent.GetComponent<StatData>();
                if (statData != null)
                {
                    StartPresenters();
                    this.statData.AddObserver(this);
                    this.uiModule.AddObserver(this);
                }
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