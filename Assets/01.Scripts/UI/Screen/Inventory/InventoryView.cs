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
            //���
            //�Һ�
            //��Ÿ
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
            // �κ��丮 �г� ����Ʈ�� �߰� 
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
            // ��ư �̺�Ʈ �߰� 
            AddButtonEvents(); 
        }


        /// <summary>
        /// ��ư �̺�Ʈ �߰�
        /// </summary>
        private void AddButtonEvents()
        {
            GetRadioButton((int)RadioButtons.weapon_button).RegisterValueChangedCallback((x)=> ActiveInventoryPanel( Elements.weapon_panel));
            GetRadioButton((int)RadioButtons.armor_button).RegisterValueChangedCallback((x) => ActiveInventoryPanel(Elements.armor_panel));
            GetRadioButton((int)RadioButtons.consumation_button).RegisterValueChangedCallback((x) => ActiveInventoryPanel(Elements.consumation_panel));
        }


        /// <summary>
        /// �κ��丮 �г� Ȱ��ȭ��Ű�� 
        /// </summary>
        /// <param name="elementType"></param>
        private void ActiveInventoryPanel(Elements elementType)
        {
            Debug.Log("�κ��丮 Ȱ��ȭ");
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

