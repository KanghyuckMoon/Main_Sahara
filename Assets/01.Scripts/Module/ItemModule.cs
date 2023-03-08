using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Pool;
using Utill.ExtraStruct;
using PassiveItem;

namespace Module
{
    public enum AccessoriesItemType
    {
        HpUp,
        Fire,
        DoubleJump,
        Dash,
        NONE
    }
    public partial class ItemModule : AbBaseModule
    {
        public int MeleeAttackDmg
        {
            get
            {
                return meleeAttackDmg;
            }
        }
        public int MagicAttackDmg
        {
            get
            {
                return magicAttackDmg;
            }
        }
        public int RangeAttackDmg
        {
            get
            {
                return rangeAttackDmg;
            }
        }

        private Dictionary<AccessoriesItemType, ItemPassive> passiveItem = new Dictionary<AccessoriesItemType, ItemPassive>();

        private int meleeAttackDmg;
        private int rangeAttackDmg;
        private int magicAttackDmg;

        //PriorityDelegate<>

        public ItemModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }

        public ItemModule() : base()
        {

        }

        public override void Start()
        {
            SetPassiveItem(AccessoriesItemType.DoubleJump);
            SetPassiveItem(AccessoriesItemType.Dash);
        }
        private void ApplyPassive()
        {
            foreach(ItemPassive _itemPassive in passiveItem.Values)
            {
                _itemPassive?.ApplyEffect();
            }
        }

        public override void Update()
        {
            foreach (ItemPassive _itemPassive in passiveItem.Values)
            {
                _itemPassive?.UpdateEffect();
            }
        }

        public void RemovePassiveItem(AccessoriesItemType _itemKey)
        {
            passiveItem[_itemKey].UndoEffect();

            passiveItem.Remove(_itemKey);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            ClassPoolManager.Instance.RegisterObject<ItemModule>("ItemModule", this);
        }
    }
}