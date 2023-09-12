    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using Inventory;
using UI.EventManage;
using UI.Base;
    using UI.UtilManager;
    using UnityEngine.PlayerLoop;

    namespace UI.Inventory
{
    [Serializable]
    public class InventoryPresenter : AbBaseScreen
    {
        [SerializeField] private UIDocument uiDocument;
        [SerializeField] private Camera inventoryCam;

        [SerializeField] private InventoryView inventoryView;

        private AccentItemCompo accentItemCompo;

        private DraggerRot draggerRot;
        //   private 

        private Action onActiveScreenEvt = null; 
        // 프로퍼티 
        public Action OnActiveScreen
        {
            get => onActiveScreenEvt;
            set => onActiveScreenEvt = value;
        }
        public override IUIController UIController { get; set; }

        [ContextMenu("버튼 초기화 테스트 ")]
        public void Test()
        {
            inventoryView.GridPr.GridView.InitButton();
        }
        private void Awake()
        {
            uiDocument ??= GetComponent<UIDocument>();
            inventoryCam = GameObject.FindWithTag("InventoryCam").GetComponent<Camera>();

            inventoryView.InitUIDocument(uiDocument);
            accentItemCompo = new AccentItemCompo();
            accentItemCompo.Init(inventoryCam.transform);
            inventoryView.AddSlotClickEvent((x) =>
            {
                // 아이템 띄우기
                accentItemCompo.ActiveModel(x.modelkey);

            });
        }

        
        private void OnEnable()
        {   
            inventoryView.Cashing();
            inventoryView.Init();
            draggerRot = new DraggerRot(
                () => Debug.Log("s"),
                () =>
                {
                    accentItemCompo.RotateModelHorizon(-Input.GetAxis("Mouse X") * Vector3.up * 1000 * Time.deltaTime);
                    accentItemCompo.RotateModelVertical(
                        -Input.GetAxis("Mouse Y") * Vector3.right * 500 * Time.deltaTime);
                    accentItemCompo.UpdateRotateModel();
                },
                () => Debug.Log("끝"));
            //inventoryView.AddSlotClickEvent((x) => accentItemCompo.ActiveModel(x.prefebkey));
            inventoryView.SelectImage.AddManipulator(draggerRot);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
            inventoryView.AddButtonEvt(InventoryGridSlotsView.RadioButtons.weapon_button,
                (x) => ChangeCategory(InventoryGridSlotsView.RadioButtons.weapon_button,
                    InventoryView.Elements.quick_slot_panel, x));
            inventoryView.AddButtonEvt(InventoryGridSlotsView.RadioButtons.consumation_button,
                (x) => ChangeCategory(InventoryGridSlotsView.RadioButtons.consumation_button,
                    InventoryView.Elements.quick_slot_panel, x));
            //  inventoryView.AddButtonEvt(InventoryGridSlotsView.RadioButtons.skill_button, 
            //     (x) => ChangeCategory(InventoryGridSlotsView.RadioButtons.skill_button,InventoryView.Elements.skill_equip_panel, x));
            inventoryView.AddButtonEvt(InventoryGridSlotsView.RadioButtons.armor_button,
                (x) => ChangeCategory(InventoryGridSlotsView.RadioButtons.armor_button,
                    InventoryView.Elements.armor_equip_panel, x));
            inventoryView.AddButtonEvt(InventoryGridSlotsView.RadioButtons.accessories_button,
                (x) => ChangeCategory(InventoryGridSlotsView.RadioButtons.accessories_button,
                    InventoryView.Elements.accessoire_equip_panel, x));


            EventManager.Instance.StartListening(EventsType.UpdateInventoryUI, UpdateUI);
        }

        private void OnDisable()
        {
            EventManager.Instance.StopListening(EventsType.UpdateInventoryUI, UpdateUI);
        }

        void Start()
        {
            UpdateUI();
        }

        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (draggerRot == null) return; 
                draggerRot.IsDragging = false;
            }
        }

        public override  bool ActiveView()
        {
            base.ActiveView();
            bool _isActive = inventoryView.ActiveScreen();
            inventoryCam.gameObject.SetActive(_isActive);

            EventManager.Instance.TriggerEvent(EventsType.UpdateQuickSlot);

            if (_isActive == true)
            {
                UpdateUI();
            }
            return _isActive;
        }

        public override  void ActiveView(bool _isActive)
        {
            base.ActiveView(_isActive);     
            inventoryCam.gameObject.SetActive(_isActive); // 인벤토리 활성화시에만 카메라 활성화 
            inventoryView.ActiveScreen(_isActive);  
            if (_isActive == false)
            {
                accentItemCompo.InactiveAllModels();
                inventoryView.SetItemText(null);
            }
            EventManager.Instance.TriggerEvent(EventsType.UpdateQuickSlot);

        }

        public void UpdateUI()
        {
            // 인벤토리 데이터 설정 
            List<ItemData> _itemList = InventoryManager.Instance.GetAllItem();
            foreach (var _itemData in _itemList)
            {
                this.inventoryView.UpdateInventoryUI(_itemData);
            }

            // 퀵슬롯 데이터 설정 
            for (int i = 0; i < 5; i++)
            {
                ItemData _quickSlotData = InventoryManager.Instance.GetQuickSlotItem(i);
                this.inventoryView.UpdateQuickSlotUI(_quickSlotData, i);
            }

            ItemData _arrowData = InventoryManager.Instance.GetArrow();
            this.inventoryView.UpdateArrowSlotUI(_arrowData, 0);
        }

        private InventoryGridSlotsView.RadioButtons curRadioButton = InventoryGridSlotsView.RadioButtons.weapon_button; 

        private void ChangeCategory(InventoryGridSlotsView.RadioButtons _rType, InventoryView.Elements _eType,
            bool _isActive)
        {
            // 무기-소비템은 퀵슬롯을 공유
            if (
                (_rType is InventoryGridSlotsView.RadioButtons.consumation_button &&
                curRadioButton is InventoryGridSlotsView.RadioButtons.weapon_button)
                ||
                (_rType is InventoryGridSlotsView.RadioButtons.weapon_button &&
                 curRadioButton is InventoryGridSlotsView.RadioButtons.consumation_button)
                )
            {
                // 소비, 무기 버튼에서는 
                    
            }

            if (_isActive == true)
            {
                curRadioButton = _rType;
                      inventoryView.SetInventoryTitleName(_rType);
            }

            AnimateSlot(_eType, _isActive);
        }

        private void AnimateSlot(InventoryView.Elements _type, bool _isActive)
        {
            StartCoroutine(AnimateCo(_type, _isActive));
        }

        private IEnumerator AnimateCo(InventoryView.Elements _type, bool _isActive)
        {
            WaitForSeconds _w = new WaitForSeconds(0.1f);
            var _list = inventoryView.GetSlotList(_type, _isActive);
            foreach (var _slot in _list)
            {
                if (_isActive == false)
                {
                    //_slot.style.translate = new StyleTranslate(new Translate(500f,0));
                    _slot.AddToClassList("quick_slot_init");
                }
                else
                {
                    //_slot.style.translate = new StyleTranslate(new Translate(0, 0));
                    _slot.RemoveFromClassList("quick_slot_init");
                }

                yield return _w;
            }
        }
    }
}