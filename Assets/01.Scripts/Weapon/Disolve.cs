 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


 namespace Weapon
 {
     public class Disolve : MonoBehaviour
     {
         [SerializeField] private Renderer _renderer; //mesh
         [SerializeField] private Material mtrlOrg; //기본 메테리얼
         [SerializeField] private Material mtrlDissolve; //아래서 위로 가는 메테리얼
         [SerializeField] private Material mtrlPhase; //노이즈로 사라지는 메테리얼

         [SerializeField] private float fadeTime = 2f;

         /*void Start()
         {
             
             DoFade(1, -2, fadeTime);
       
         }*/
         public void DoFade(float start, float dest, float time) //함수
         {
             _renderer.material = mtrlDissolve;

             var mat = _renderer.material;

             DOTween.To(() => start, x => mat.SetFloat("_Split_Val", x), dest, time).OnComplete(
                 () =>
                 {
                     if (dest > 0)
                     {
                         _renderer.material = mtrlOrg;
                     }
                 });
         }
     }
 }