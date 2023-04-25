using System.Collections;
using System.Collections.Generic;
using HitBox;
using UnityEngine;

namespace Module
{
    public interface IWeaponSkill
    {
        public void Skills(AbMainModule _mainModule);
        public HitBoxAction GetHitBoxAction();
    }
}