#if Unity_Editor
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField] 
    private EveryAnimationSO everyAnimationSO;

    [SerializeField] private List<Animator> playerList;

    private AnimatorOverrideController animatorOverrideController;

    private int num = 0;


    [ContextMenu("积己")]
    private void Animation()
    {
        var count = Mathf.Sqrt(everyAnimationSO.playerAnimation.Length);
        //Debug.LogError(everyAnimationSO.playerAnimation.Length);

        for (var i = 1; i < count + 1; i++)
        {
            for (var j = 0; j <= count; j++)
            {
                if (everyAnimationSO.playerAnimation.Length <= num) continue;
                var _a = Instantiate(player, new Vector3((i - 1) * 2, j * 2, 0), Quaternion.identity);
                
                playerList.Add(_a.GetComponent<Animator>());

                //Animator aas = new Animator();
                animatorOverrideController = new AnimatorOverrideController(_a.GetComponent<Animator>().runtimeAnimatorController);
                _a.GetComponent<Animator>().runtimeAnimatorController = animatorOverrideController;
                animatorOverrideController["Test"] = everyAnimationSO.playerAnimation[num];
//                _a.GetComponent<Animator>().Play("Test");
                num++;
                //_a.GetComponent<Animation>().L
                //else
                //    break;
            }
        }
    }

    [ContextMenu("局聪皋捞记 角青")]
    private void PlayAnimation()
    {
        foreach (var _t in playerList)
        {
            _t.Play("Test");
        }
    }
}
#endif
