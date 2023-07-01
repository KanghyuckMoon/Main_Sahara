using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public interface IPopup
{
    public VisualElement Parent { get; }
    public void ActiveTween();
    public void InActiveTween();
    public void Undo();
    public void SetData(object _data);

    public IEnumerator TimerCo(float _time)
    {
        float _curTime = 0f;
        bool isActiveTween = false; 
        while (true)
        {
            _curTime += Time.deltaTime;
            if (isActiveTween == false && _curTime > 0.1f)
            {
                isActiveTween = true; 
                ActiveTween();
            }

            if (_curTime >= _time)
            {
                // 애니메이션 
                InActiveTween();
                yield return new WaitForSecondsRealtime(0.2f);
                Undo();
                yield break;
            }

            yield return null;
        }
    }
}
