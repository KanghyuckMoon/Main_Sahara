using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Effect;

namespace EnemyComponent
{
    public class JumpAndLandEffect : MonoBehaviour
    {
        [SerializeField] private string jumpEff;
        [SerializeField] private string landEff;
        [SerializeField] private AbMainModule abMainModule;
        [SerializeField] private AnimationOnEffect animationOnEffect;
        
        private void OnEnable()
        {
            abMainModule.GetModuleComponent<PhysicsModule>(ModuleType.Physics).AddLandAction(SetEffectLand);
            var _jumpModule = abMainModule.GetModuleComponent<JumpModule>(ModuleType.Jump);
            if (_jumpModule is null)
            {
                var _noneAnimationJumpModule = abMainModule.GetModuleComponent<NoneAnimationJumpModule>(ModuleType.Jump);
                _noneAnimationJumpModule?.AddJumpAction(SetEffectJump);
            }
        }

        public void SetEffectLand()
        {
            animationOnEffect.OnEffect(landEff);
        }
        public void SetEffectJump()
        {
            animationOnEffect.OnEffect(jumpEff);
        }
    }

}