using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effect;

namespace EnemyComponent
{
    public enum VayuEyeState
    {
        Normal,
        Hit,
        Detect,
        Defenseless,
        Reset,
    }
    
    public class RotateEyes : MonoBehaviour
    {
        public VayuEyeState VayuEyeState
        {
            get
            {
                return vayuEyeState;
            }
            set
            {
                vayuEyeState = value;
            }
        }
        
        [SerializeField] private VayuEyeState vayuEyeState;

        [SerializeField] private float speed = 1f;
        [SerializeField] private float scale = 1f;
        [SerializeField] private Transform centerTrm;
    
        [SerializeField] private float time;
        [SerializeField] private float changeTimer = 1f;
        [SerializeField] private Collider col;
        [SerializeField] private GameObject detect;
        [SerializeField] private Rigidbody rigid;
        [SerializeField] private LookAtPlayer lookAtPlayer;
        [SerializeField] private string detectEffectAddress;
        
        private void Update()
        {
            time += Time.deltaTime;
            switch (vayuEyeState)
            {
                case VayuEyeState.Normal:
                    DefaultAction();
                    break;
                case VayuEyeState.Hit:
                    HitAction();
                    break;
                case VayuEyeState.Detect:
                    DetectAction();
                    break;
                case VayuEyeState.Defenseless:
                    DefenselessAction();
                    break;
                case VayuEyeState.Reset:
                    ResetAction();
                    break;
            }
                
        }

        private void DefaultAction()
        {       
            Vector3 _newPos, _addPos = Vector3.zero;
            _newPos = centerTrm.position;
            _addPos.x = Mathf.Cos(time);
            _addPos.z = Mathf.Sin(time);
            _newPos += _addPos * scale;
            transform.position = Vector3.Lerp(transform.position, _newPos, Time.deltaTime * speed);
        }

        private void HitAction()
        {
            Vector3 _downPos = transform.position;
            _downPos.y = transform.position.y - 10;
            transform.position = Vector3.Lerp(transform.position, _downPos, Time.deltaTime * speed);
            
            changeTimer -= Time.deltaTime;

            if (changeTimer < 0f)
            {
                vayuEyeState = VayuEyeState.Normal;
                rigid.isKinematic = true;
                rigid.useGravity = false;
                col.enabled = true;
                col.isTrigger = true;
                detect.SetActive(false);
                lookAtPlayer.enabled = true;
            }
        }

        private void DetectAction()
        {
            //changeTimer -= Time.deltaTime;
            //
            //if (changeTimer < 0f)
            //{
            //    vayuEyeState = VayuEyeState.Normal;
            //    rigid.isKinematic = true;
            //    col.enabled = true;
            //    detect.SetActive(false);
            //    lookAtPlayer.enabled = true;
            //}
        }

        private void DefenselessAction()
        {
            changeTimer -= Time.deltaTime;

            if (changeTimer < 0f)
            {
                changeTimer = 3f;
                vayuEyeState = VayuEyeState.Reset;
                rigid.isKinematic = true;
                rigid.useGravity = false;
                col.enabled = true;
                col.isTrigger = true;
                detect.SetActive(false);
                lookAtPlayer.enabled = true;
            }
        }

        
        private void ResetAction()
        {
            changeTimer -= Time.deltaTime;

            if (changeTimer < 0f)
            {
                vayuEyeState = VayuEyeState.Normal;
            }
        }
        
        public void EyeHit()
        {
            changeTimer = 5f;
            vayuEyeState = VayuEyeState.Hit;
        }

        public void EyeDetect()
        {
            changeTimer = 7f;
            vayuEyeState = VayuEyeState.Defenseless;
            col.enabled = true;
            col.isTrigger = false;
            lookAtPlayer.enabled = false;
            detect.SetActive(false);
            rigid.isKinematic = false;
            rigid.useGravity = true;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ground"))
            {
                if (vayuEyeState == VayuEyeState.Hit)
                {
                    Vector3 _newPos = transform.position;
                    _newPos.y -= 0.8f;
                    transform.position = _newPos;
                    vayuEyeState = VayuEyeState.Detect;
                    col.enabled = false;
                    detect.SetActive(true);
                    lookAtPlayer.enabled = false;
                    EffectManager.Instance.SetEffectDefault(detectEffectAddress, transform.position, Quaternion.identity);
                }
            }
        }
    }

}