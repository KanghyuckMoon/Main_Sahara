 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


 namespace Weapon
 {
     public class Disolve : MonoBehaviour
     {
         [SerializeField] private Renderer _renderer; //mesh
         [SerializeField] private Material mtrlOrg; //�⺻ ���׸���
         [SerializeField] private Material mtrlDissolve; //�Ʒ��� ���� ���� ���׸���
         [SerializeField] private Material mtrlPhase; //������� ������� ���׸���

         [SerializeField] private float fadeTime = 2f;

         /*void Start()
         {
             
             DoFade(1, -2, fadeTime);
       
         }*/
         public void DoFade(float start, float dest, float time) //�Լ�
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