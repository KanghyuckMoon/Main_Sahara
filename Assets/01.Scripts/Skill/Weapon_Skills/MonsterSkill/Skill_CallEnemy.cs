using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Streaming;
using Pool;
using HitBox;
using Utill.Measurement;

namespace Skill
{

    public class Skill_CallEnemy : WeaponSkillFunctions, IWeaponSkill
    {
        [SerializeField]
        private AnimationClip animationClip;

        [SerializeField]
        private float radius = 1f;

        [SerializeField] 
        private LayerMask callLayerMask;
        
        private Vector3 spawnPos = Vector3.zero;
        //[SerializeField] private buv
        private bool isSpawnOn = false;
        private bool isSpawn = false;
        private AbMainModule mainModule;
        
        public void Skills(AbMainModule _mainModule)
        {
	        this.mainModule = _mainModule;
            PlaySkillAnimation(_mainModule, animationClip, Call);
            //Invoke("SpawnObj", 1f);;
			//isSpawnOn = true;
		}

        public void Call()
		{
			Collider[] targets = Physics.OverlapSphere(mainModule.transform.position, radius, callLayerMask);
			foreach (Collider col in targets)
			{
                var _otherMainModule = col.gameObject.GetComponent<AbMainModule>();
                if(_otherMainModule != null)
                {
                    var _aiModule = _otherMainModule.GetModuleComponent<AIModule>(ModuleType.Input);
                    if(_aiModule != null)
                    {
	                    Logging.Log(_aiModule.MainModule.gameObject.name);
						_aiModule.AIModuleHostileState = AIModule.AIHostileState.Discovery;
                    }
				}
			}
		}

		public HitBoxAction GetHitBoxAction()
		{
            return null;
		}
	}   
}
