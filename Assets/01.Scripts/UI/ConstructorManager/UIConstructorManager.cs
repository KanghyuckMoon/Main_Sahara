using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;
using UI.EventAlarm;
using UI.Base;
using System;
using UI.Production;
using UnityEngine.UIElements; 

namespace UI.ConstructorManager
{
    /// <summary>
    /// �������� UI �������ִ� �ֵ� �������ִ� �� 
    /// </summary>
    public class UIConstructorManager : MonoSingleton<UIConstructorManager>
    {
        private UIConstructor<EventAlarmView> eventAlarmConstructor;
        private UIConstructor<QuestEntryView> questEntryConstructor;
        private UIConstructor<SlotItemView> slotItemConstructor;
        private UIConstructor<UpgradeSlotView> upgradeSlotConstructor;
        private UIConstructor<ItemDescriptionView> itemDescriptionConstructor;
        private UIConstructor<PopupView> popupConstructor;
        private UIConstructor<SaveEntryView> saveEntryConstructor; 
        private UIConstructor<BuffEntryView> buffEntryConstructor; 

        private EventAlarmPresenter eventAlarmPresenter; 

        private Dictionary<Type, ICreateUI> uiConstructorDic = new Dictionary<Type, ICreateUI>();

        // ������Ƽ
        public Dictionary<Type, ICreateUI> UIConstructorDic => uiConstructorDic; 
        public EventAlarmPresenter EventAlarmPresenter
        {
            get
            {
                if(eventAlarmPresenter is null)
                {
                    GameObject obj = GameObject.FindWithTag("UIParent");
                    if(obj is not null)
                    {
                        eventAlarmPresenter = GetComponentInChildren<EventAlarmPresenter>();
                    }
                    else
                    {
                        return null; 
                    }
                }
                return eventAlarmPresenter;
            }
        }


        private void OnEnable()
        {
            uiConstructorDic.Clear(); 

            eventAlarmConstructor = new UIConstructor<EventAlarmView>("EventAlarm");
            questEntryConstructor = new UIConstructor<QuestEntryView>("QuestEntry");
            slotItemConstructor = new UIConstructor<SlotItemView>("SlotItem");
            upgradeSlotConstructor = new UIConstructor<UpgradeSlotView>("UpgradeSlot");
            itemDescriptionConstructor = new UIConstructor<ItemDescriptionView>("ItemDescription");
            popupConstructor = new UIConstructor<PopupView>("Popup");
            saveEntryConstructor = new UIConstructor<SaveEntryView>("SaveEntry");
            buffEntryConstructor= new UIConstructor<BuffEntryView>("BuffEntry"); 

            uiConstructorDic.Add(typeof(EventAlarmView), eventAlarmConstructor);
            uiConstructorDic.Add(typeof(QuestEntryView), questEntryConstructor);
            uiConstructorDic.Add(typeof(SlotItemView), slotItemConstructor);
            uiConstructorDic.Add(typeof(UpgradeSlotView), upgradeSlotConstructor);
            uiConstructorDic.Add(typeof(ItemDescriptionView), itemDescriptionConstructor);
            uiConstructorDic.Add(typeof(PopupView), popupConstructor);
            uiConstructorDic.Add(typeof(SaveEntryView), saveEntryConstructor);
            uiConstructorDic.Add(typeof(BuffEntryView), buffEntryConstructor);
        }

        private void Awake()
        {
            
        }

        /// <summary>
        /// UI ���� ��ȯ 
        /// </summary>
        /// <param name="_type"></param>
        /// <returns></returns>
        public (VisualElement, AbUI_Base) GetProductionUI(Type _type)
        {
            if(this.uiConstructorDic.TryGetValue(_type, out ICreateUI _creater) == true)
            {
               return _creater.CreateUI(); 
            }
            Debug.LogWarning($"UIConstructor Dictionary�� {_type.Name} �� �����ϴ�");
            return (null,null); 
        }

    }

}
