using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Attack;

namespace HitBox
{
    public class HitToAddForce : MonoBehaviour
    {
        [SerializeField] private string hitTagName;
        private ulong praviousHitBoxIndex;
        [SerializeField] private Rigidbody rigid;
        
        public void AddForce(Collider other)
        {
                if (other.CompareTag(hitTagName))
                {
                    InGameHitBox _inGameHitBox = other.GetComponent<InGameHitBox>();
                    if (_inGameHitBox is null) return;
                    if (_inGameHitBox.GetIndex() == praviousHitBoxIndex) return;
                    praviousHitBoxIndex = _inGameHitBox.GetIndex();
                    AttackFeedBack _attackFeedBack = other.GetComponent<AttackFeedBack>();
                    Vector3 _closerPoint = other.ClosestPoint(transform.position);

                    Vector3 _dir;
                    if (_inGameHitBox.IsContactDir)
                    {
                        _dir = (_closerPoint - _inGameHitBox.transform.position).normalized;
                    }
                    else
                    {
                        _dir = (_inGameHitBox.KnockbackDir() * Vector3.forward);
                    }

                    float _power = _inGameHitBox.KnockbackPower() + 5;// / 10f;

                    rigid.AddForce(_dir * _power, ForceMode.Impulse);
                    
                    _attackFeedBack.InvokeEvent(_closerPoint, _inGameHitBox.HitBoxData.hitEffect);

                }
        }
    }   
}
