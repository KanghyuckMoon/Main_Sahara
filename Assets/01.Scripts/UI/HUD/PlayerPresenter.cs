using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Hud
{
    public class PlayerPresenter : EntityPresenter
    {
        [SerializeField]
        private QuickSlotPresenter quickSlotPresenter;

        protected override void ContructPresenters()
        {
            base.ContructPresenters();
            PresenterList.Add(quickSlotPresenter);
            DataPresenterDic[HudType.statData].Add(quickSlotPresenter);
        }

        private void Awake()
        {
            quickSlotPresenter.Start(); 
        }

        private void OnDestroy()
        {
            quickSlotPresenter.OnDestroy(); 
        }

        protected override void OnEnable()
        {
            OnConstructorPresenters = () =>
            {
                PresenterList.Add(quickSlotPresenter);
                DataPresenterDic[HudType.statData].Add(quickSlotPresenter);
            };
            base.OnEnable();
            quickSlotPresenter.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            quickSlotPresenter.OnDisable();
        }

        public override void Execute()
        {
            base.Execute();
            quickSlotPresenter.ActiveScreen(false);
        }

        public override void Undo()
        {
            base.Undo();
            quickSlotPresenter.ActiveScreen(true);
        }
    }
}
