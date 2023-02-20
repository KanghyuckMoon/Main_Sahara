using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; 

namespace UI.Inventory
{
    /// <summary>
    /// �κ��丮�� ���Ե� �����ش� �κ� 
    /// </summary>
    public class InventoryGridSlotsView : AbUI_Base
    {
        public enum InvenPanelElements
        {
            // �гε鸸 �־���� 
            weapon_panel,
            armor_panel,
            consumation_panel,
            skill_panel,
            accessories_panel,
            material_panel,
            valuable_panel
        }

        enum RadioButtons
        {
            weapon_button,
            armor_button,
            consumation_button,
            skill_button,
            accessories_button,
            material_button,
            valuable_button
            //���
            //�Һ�
            //��Ÿ
        }

    }
}

