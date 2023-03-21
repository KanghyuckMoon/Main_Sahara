using System.Collections;
using System.Collections.Generic;
using Buff;
using DG.Tweening.Plugins;
using UnityEngine;
using Module;

namespace Skill
{
    public class WeaponSkillFunctions : MonoBehaviour
    {
        public string animationName = "WeaponSkill";
        public string buffIconString = "_Icon";
        public string buffEffectString = "_Effect";

        public List<BuffData> buffList = new List<BuffData>();
        //public List<BuffData> debuffList = new List<BuffData>();

        protected void PlaySkillAnimation(AbMainModule _mainModule, AnimationClip _animationClip)
        {
            _mainModule.AnimatorOverrideController[animationName] = _animationClip;
            //_mainModule.Animator.Play(animationName);

            _mainModule.Animator.SetBool(animationName, true);
        }

        protected void GetBuff(AbMainModule _mainModule)
        {
            BuffModule _bufmodule= _mainModule.GetModuleComponent<BuffModule>(ModuleType.Buff);
            
            foreach (var _buffs in buffList)
            {
                GetBuff(_buffs, _bufmodule)
                    .SetDuration(_buffs.duration)
                    .SetPeriod(_buffs.period)
                    .SetValue(_buffs.value)
                    .SetSprite(_buffs.buffs.ToString() + buffIconString)
                    .SetSpownObjectName(_buffs.buffs.ToString() + buffEffectString);
            }
        }

        public AbBuffEffect GetBuff(BuffData _buffs, BuffModule _bufmodule) => _buffs.buffs switch
        {
            Buffs.U_Healing => new Healing_Buf(_bufmodule),
            Buffs.U_ReduceDamage => new ReduceDamage_Buf(_bufmodule),
            Buffs.None => null
        };
    }
}