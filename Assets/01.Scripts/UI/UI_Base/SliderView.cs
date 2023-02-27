using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening; 
public class SliderView
{
    private ProgressBar bar;

    public ProgressBar Bar => bar; 
    public float BarV
    {
        get => Bar.value; 
        set
        {
            if(1f >value)
            {
                float _a = value - (int)value; 
                Bar.value = _a; 
            }
        }
    }
    public SliderView(VisualElement parent,string name)
    {
        bar = parent.Q<ProgressBar>(name);
    }

    public SliderView(ProgressBar progressBar)
    {
        this.bar = progressBar; 
    }

    public void SetSlider(float targetValue)
    {
        TweeningValue(targetValue); 
        //_bar.value = targetValue; 
        // 애니메이션 
        //StartCoroutine(UpdateBar(targetValue)); 
    }

    public void SetMaxV(float highValue)
    {
        bar.highValue = highValue; 
    }

    public void TweeningValue(float _targetV)
    {
        float startV = bar.value;
        
        DOTween.To(() => startV, (x) => bar.value = x, _targetV, 0.5f);
    }
    public IEnumerator IEUpdateBar(float targetV)
    {
        float prevV = bar.value; 
        while (Mathf.Abs(targetV - bar.value) > 0.01f)
        {
            bar.value = Mathf.Lerp(bar.value, targetV, Time.deltaTime); 
            yield return null; 
        }
        bar.value = targetV; 
    }
}
