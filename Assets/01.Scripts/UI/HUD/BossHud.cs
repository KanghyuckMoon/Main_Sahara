using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Hud
{
//  �� ���Ҹ��� �ƴ�hud �� getObejct
    public class BossHud : EntityPresenter
    {
        private Label nameLabel;

        private const string nameLabelStr = "name_label";

        protected override void OnEnable()
        {
            base.OnEnable();
            nameLabel = hudElement.Q<Label>(nameLabelStr);
        }

        public override void UpdateUI()
        {
            base.UpdateUI();
            nameLabel.text = uiModule.Name; 
        }
    }
}