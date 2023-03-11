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

        // ������ 
        private UIModule uiModule;
        private StatData statData;
        private BuffModule buffModule;

        private List<IUIFollower> _presenterList = new List<IUIFollower>();
        private Dictionary<HudType, List<IUIFollower>> _dataPresenterDic = new Dictionary<HudType, List<IUIFollower>>(); // ������ Ÿ��, �������� 

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

        private void OnEnable()
        {
            uiDocument ??= GetComponent<UIDocument>();
            hudElement = uiDocument.rootVisualElement.ElementAt(0);
            hudElement.style.display = DisplayStyle.None;
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
            if (presenterFollower == null && isPlayerHud == false) // �Ӹ� ���� hud ������� 
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
                //Debug.Log("���󰡴���");
            }

        }

        [ContextMenu("���� �׽�Ʈ")]
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
        /// ���� �ʱ�ȭ 
        /// </summary>
        private void Clear()
        {
            target = null;
            targetRenderer = null;
            presenterFollower = null;
            isPlayerHud = false;
            statData = null; 
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
            _presenterList.Clear();

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

        [ContextMenu("���� ������ ����")]
        public void TestCreateBuffIcon()
        {
       //     buffPresenter.CreateBuffIcon();
        }


        IEnumerator Init()
        {
            if(statData != null && buffModule != null)
            {
                StartPresenters();
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