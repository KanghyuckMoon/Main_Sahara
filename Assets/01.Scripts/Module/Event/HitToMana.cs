using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Effect;
using Utill.Addressable;
using Utill;
using HitBox;
using Data;
using Attack;
using Buff;
using DG.Tweening;
using Skill;
using Item;
using Pool;
using Module;

namespace HitBox
{
    public class HitToMana : MonoBehaviour
    {
        [SerializeField] private string hitTagName;
        private ulong praviousHitBoxIndex;
        
        public void AddMana(Collider other)
        {
            Debug.Log(12);
                if (other.CompareTag(hitTagName))
                {
                    Debug.Log(1);
                    InGameHitBox _inGameHitBox = other.GetComponent<InGameHitBox>();
                    if (_inGameHitBox is null) return;
                    if (_inGameHitBox.GetIndex() == praviousHitBoxIndex) return;
                    praviousHitBoxIndex = _inGameHitBox.GetIndex();
                    AttackFeedBack _attackFeedBack = other.GetComponent<AttackFeedBack>();
                    StatData _statData = _inGameHitBox.Owner.GetComponent<StatData>();
                    Vector3 _closerPoint = other.ClosestPoint(transform.position);

                    var _settingTime = _inGameHitBox.Owner.GetComponent<SettingTime>();

                    //Debug.LogError(_inGameHitBox.Owner);
                    /*if(_settingTime is not null)
                    {
                        _settingTime.SetTime(_inGameHitBox.HitBoxData.hitStunDelay, 0.7f);
                    }*/
                    
                    if (_settingTime is not null)
                    {
                        //Debug.LogError("안돼안대ㅗ난돼어노대ㅗ애나애내");
                        _settingTime.SetTime(/*_inGameHitBox.HitBoxData.hitStunDelay - 0.1f*/1f, 0f);
                    }

                    foreach (var _s in _inGameHitBox.HitBoxData.hitEffect)
                    {
                        _attackFeedBack.InvokeEvent(other.ClosestPoint(transform.position), _s);
                    }

                    if (other.CompareTag("Player_Weapon") || other.CompareTag("PlayerSkill"))
                    {
                        int _totalMana = 0;
                        int _manaCount = 0;
                        if (_statData != null)
                        {
                            _inGameHitBox.Owner.GetComponent<BodyRotation>()?.SetChromaticAberration(0.3f);
                            
                            _totalMana = _statData.ManaRegen + _statData.ChangeMana(_statData.ManaRegen);
                        
                            _manaCount = (_totalMana / 10);

                            for (int i = 0; i < _manaCount; ++i)
                            {
                                MPBall mpBall = ObjectPoolManager.Instance.GetObject("MPBall").GetComponent<MPBall>();
                                mpBall.SetMPBall(_closerPoint, _statData.ChargeMana, _totalMana / _manaCount, _inGameHitBox.Owner);
                            }
                        }
                        else
                        {
                            StatData _stat = other.GetComponent<InGameHitBox>().Owner.GetComponent<StatData>();
                            _totalMana = _stat.ManaRegen + _statData.ChangeMana(_stat.ManaRegen);
                        
                            _manaCount = (_totalMana / 10);
                        
                            for (int i = 0; i < _manaCount; ++i)
                            {
                                MPBall mpBall = ObjectPoolManager.Instance.GetObject("MPBall").GetComponent<MPBall>();
                                mpBall.SetMPBall(_closerPoint, _stat.ChargeMana, _totalMana / _manaCount, _inGameHitBox.Owner);
                            }
                        }
                    }

                    else
                    {
                        int _totalMana = 0;
                        if (_statData != null)
                        {
                            _totalMana = _statData.ManaRegen + _statData.ChangeMana(_statData.ManaRegen);
                        }
                        else
                        {
                            StatData _stat = other.GetComponent<InGameHitBox>().Owner.GetComponent<StatData>();
                            _totalMana = _stat.ManaRegen + _statData.ChangeMana(_stat.ManaRegen);
                        }
                    }
                }
        }
    }   
}
