 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Disolve : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;//mesh
    [SerializeField] private Material mtrlOrg;//�⺻ ���׸���
    [SerializeField] private Material mtrlDissolve;//�Ʒ��� ���� ���� ���׸���
    [SerializeField] private Material mtrlPhase;//������� ������� ���׸���
    [SerializeField] private float fadeTime = 2f;
    void Start()
    {
        _renderer.material = mtrlDissolve;
        DoFade(1, -2, fadeTime);
  
    }
    private void DoFade(float start, float dest, float time)//�Լ�
    {
        var mat = _renderer.material;
        DOTween.To(() => start, x => mat.SetFloat("_Split_Val", x), dest, time);
    }
}
