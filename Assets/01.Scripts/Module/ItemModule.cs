using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Pool;
using Utill.ExtraStruct;
using PassiveItem;

namespace Module
{
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

        private Dictionary<string, ItemPassive> passiveItem = new Dictionary<string, ItemPassive>();

        private int meleeAttackDmg;
        private int rangeAttackDmg;
        private int magicAttackDmg;

        //PriorityDelegate<>

        public ItemModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }

        private void ApplyPassive()
        {
            foreach(ItemPassive _itemPassive in passiveItem.Values)
            {
                _itemPassive?.ApplyEffect();
            }
        }

        public void RemovePassiveItem(string _itemKey)
        {
            passiveItem[_itemKey].UndoEffect();

            passiveItem.Remove(_itemKey);
        }

    }
}