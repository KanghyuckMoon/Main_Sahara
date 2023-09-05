using System;
using System.Collections;
using System.Collections.Generic;
using Buff;
using DG.Tweening.Plugins;
using UnityEngine;
using Module;
using Data;
using UI.EventManage;

namespace Skill
{
    public class WeaponSkillFunctions : MonoBehaviour
    {
        [SerializeField]
        public int usingMana;

        public string skillIconString = ""; 
        public string animationName = "WeaponSkill";
        public string buffIconString = "_Icon";
        public string buffEffectString = "_Effect";

        public List<BuffData> buffList = new List<BuffData>();
        //public List<BuffData> debuffList = new List<BuffData>();

        protected void OnEnable()
        {
            StartCoroutine(UpdateUI());
        }

        private IEnumerator UpdateUI()
        {
            yield return new WaitForSeconds(0.1f);  
            Debug.Log("@@업데이트");
            EventManager.Instance.TriggerEvent(EventsType.SetQuickslotMana,usingMana);
            EventManager.Instance.TriggerEvent(EventsType.SetHudSkillImage,skillIconString);
        }
        protected void PlaySkillAnimation(AbMainModule _mainModule, AnimationClip _animationClip, System.Action _action = null)
        {
            
            _mainModule.AnimatorOverrideController[animationName] = _animationClip;
            //_mainModule.Animator.Play(animationName);

            _mainModule.SkillAnimAction = _action;
            _mainModule.Animator.SetBool(animationName, true);
        }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
        
        protected bool UseMana(AbMainModule _mainModule, int _mana)
        {
            StatData _statData = _mainModule.GetComponent<StatData>();

            return _statData.ChargeMana(_mana);
            //_mainModule.GetComponent<StatData>().ChangeMana(_mana);
        }

        protected void GetBuff(AbMainModule _mainModule)
        {
            BuffModule _bufmodule= _mainModule.GetModuleComponent<BuffModule>(ModuleType.Buff);
            
            if (buffList.Count == 0) return;
            
            foreach (var _buffs in buffList)
            {
                _bufmodule.AddBuff(GetBuff(_buffs, _bufmodule)
                    .SetDuration(_buffs.duration)
                    .SetPeriod(_buffs.period)
                    .SetValue(_buffs.value)
                    .SetSprite(_buffs.buffs.ToString() + buffIconString)
                    .SetSpownObjectName(_buffs.buffs.ToString() + buffEffectString), _buffs.bufftype);
            }
        }

        public AbBuffEffect GetBuff(BuffData _buffs, BuffModule _bufmodule) => _buffs.buffs switch
        {
            Buffs.U_Healing => new Healing_Buf(_bufmodule),
            Buffs.U_ChangeMagicDef => new ChangeMagicResistance_Buf(_bufmodule),
            Buffs.U_ChangePhysicDef => new ChangePhysicResistance_Buf(_bufmodule),
            Buffs.U_AnimationSpeed => new AnimationSpeed_Buf(_bufmodule),
            Buffs.U_ChangeMana => new ChangeMana_Buf(_bufmodule),
            Buffs.U_MoveSpeed => new MoveSpeed_Buf(_bufmodule),
            Buffs.None => null
        };
    }
}