using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; 

namespace UI.Inventory
{
    /// <summary>
    /// 인벤토리의 슬롯들 보여준는 부분 
    /// </summary>
    public class InventoryGridSlotsView : AbUI_Base
    {
        public enum InvenPanelElements
        {
            // 패널들만 넣어야해 
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
            //장비
            //소비
            //기타
        }

    }
}

