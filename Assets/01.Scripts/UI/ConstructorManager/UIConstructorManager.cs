using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;
//using UI.EventAlarm;
using UI.Base;
using System;
using UI.Popup;
using UI.Production;
using UnityEngine.UIElements; 

namespace UI.ConstructorManager
{
    /// <summary>
    /// 占쏙옙占쏙옙占쏙옙占쏙옙 UI 占쏙옙占쏙옙占쏙옙占쌍댐옙 占쌍듸옙 占쏙옙占쏙옙占쏙옙占쌍댐옙 占쏙옙 
    /// </summary>
    public class UIConstructorManager : MonoSingleton<UIConstructorManager>
    {
        private UIConstructor<EventAlarmView> eventAlarmConstructor;
        private UIConstructor<QuestEntryView> questEntryConstructor;
        private UIConstructor<SlotItemView> slotItemConstructor;
        private UIConstructor<UpgradeSlotView> upgradeSlotConstructor;
        private UIConstructor<ItemDescriptionView> itemDescriptionConstructor;
        private UIConstructor<PopupAcceptView> popupConstructor;
        private UIConstructor<SaveEntryView> saveEntryConstructor;
        private UIConstructor<BuffEntryView> buffEntryConstructor;
        private UIConstructor<PopupGetItemView> popupGetItemConstructor;
        private UIConstructor<InteracftionPopupView> interacftionPopupView; 
    //        private  UIConstructor<popupPr>


        private Dictionary<Type, ICreateUI> uiConstructorDic = new Dictionary<Type, ICreateUI>();

        /*private EventAlarmPresenter eventAlarmPresenter; 

        // 占쏙옙占쏙옙占쏙옙티
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
        }*/


        private void OnEnable()
        {
            uiConstructorDic.Clear(); 

            eventAlarmConstructor = new UIConstructor<EventAlarmView>("EventAlarm");
            questEntryConstructor = new UIConstructor<QuestEntryView>("QuestEntry");
            slotItemConstructor = new UIConstructor<SlotItemView>("SlotItem");
            upgradeSlotConstructor = new UIConstructor<UpgradeSlotView>("UpgradeSlot");
            itemDescriptionConstructor = new UIConstructor<ItemDescriptionView>("ItemDescription");
            popupConstructor = new UIConstructor<PopupAcceptView>("Popup");
            saveEntryConstructor = new UIConstructor<SaveEntryView>("SaveEntry");
            buffEntryConstructor= new UIConstructor<BuffEntryView>("BuffEntry");
            popupGetItemConstructor = new UIConstructor<PopupGetItemView>("PopupGetItem");
            interacftionPopupView = new UIConstructor<InteracftionPopupView>("InteractionEntry");
            
            uiConstructorDic.Add(typeof(EventAlarmView), eventAlarmConstructor);
            uiConstructorDic.Add(typeof(QuestEntryView), questEntryConstructor);
            uiConstructorDic.Add(typeof(SlotItemView), slotItemConstructor);
            uiConstructorDic.Add(typeof(UpgradeSlotView), upgradeSlotConstructor);
            uiConstructorDic.Add(typeof(ItemDescriptionView), itemDescriptionConstructor);
            uiConstructorDic.Add(typeof(PopupAcceptView), popupConstructor);
            uiConstructorDic.Add(typeof(SaveEntryView), saveEntryConstructor);
            uiConstructorDic.Add(typeof(BuffEntryView), buffEntryConstructor);
            uiConstructorDic.Add(typeof(PopupGetItemView), popupGetItemConstructor);
            uiConstructorDic.Add(typeof(InteracftionPopupView), interacftionPopupView);

        }

        private void Awake()
        {
            
        }   

        /// <summary>
        /// UI 占쏙옙占쏙옙 占쏙옙환 
        /// </summary>
        /// <param name="_type"></param>
        /// <returns></returns>
        public (VisualElement, AbUI_Base) GetProductionUI(Type _type)
        {
            if(this.uiConstructorDic.TryGetValue(_type, out ICreateUI _creater) == true)
            {
               return _creater.CreateUI(); 
            }
            Debug.LogWarning($"UIConstructor Dictionary占쏙옙 {_type.Name} 占쏙옙 占쏙옙占쏙옙占싹댐옙");
            return (null,null); 
        }

    }

}
