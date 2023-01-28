using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using System; 

namespace UI
{
    [Serializable]
    public class InventoryView : AbUI_Base
    {
        enum Elements
        {
            weapon_panel,
            armor_panel,
            consumation_panel
        }

        enum RadioButtons
        {
            weapon_button,
            armor_button,
            consumation_button
            //장비
            //소비
            //기타
        }

        private List<VisualElement> inventoryPanelList = new List<VisualElement>(); 

        public override void Cashing()
        {
            base.Cashing();
            BindVisualElements(typeof(Elements));
            BindRadioButtons(typeof(RadioButtons));
        }

        public override void Init()
        {
            base.Init();

            inventoryPanelList.Clear(); 
            // 인벤토리 패널 리스트에 추가 
            inventoryPanelList.Add(GetVisualElement((int)Elements.weapon_panel));
            inventoryPanelList.Add(GetVisualElement((int)Elements.armor_panel));
            inventoryPanelList.Add(GetVisualElement((int)Elements.consumation_panel));
            //AddButtonEvent<RadioButton>
            
            for(int i=0;i < inventoryPanelList.Count; i++)
            {
                if(i == (int)Elements.weapon_panel)
                {
                    GetVisualElement(i).style.display = DisplayStyle.Flex;
                    continue; 
                }
                GetVisualElement(i).style.display = DisplayStyle.None;
            }
            // 버튼 이벤트 추가 
            AddButtonEvents(); 
        }


        /// <summary>
        /// 버튼 이벤트 추가
        /// </summary>
        private void AddButtonEvents()
        {
            GetRadioButton((int)RadioButtons.weapon_button).RegisterValueChangedCallback((x)=> ActiveInventoryPanel( Elements.weapon_panel));
            GetRadioButton((int)RadioButtons.armor_button).RegisterValueChangedCallback((x) => ActiveInventoryPanel(Elements.armor_panel));
            GetRadioButton((int)RadioButtons.consumation_button).RegisterValueChangedCallback((x) => ActiveInventoryPanel(Elements.consumation_panel));
        }


        /// <summary>
        /// 인벤토리 패널 활성화시키기 
        /// </summary>
        /// <param name="elementType"></param>
        private void ActiveInventoryPanel(Elements elementType)
        {
            Debug.Log("인벤토리 활성화");
            VisualElement v = GetVisualElement((int)elementType);

            Debug.Log(v.style.display);
            if (IsVisible(v) == true) return; 

            ShowVisualElement(v, true); 
            
            for(int i =0; i< inventoryPanelList.Count; i++)
            {
                if(inventoryPanelList[i] != v)
                {
                    inventoryPanelList[i].style.display = DisplayStyle.None; 
                }
            }
            //inventoryPanelList.Where(x => x != v)
            //                            .Select(x => x.style.display = DisplayStyle.None);
        }
    }
}

