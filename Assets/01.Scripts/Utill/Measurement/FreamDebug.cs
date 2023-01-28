using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;

namespace Utill.Measurement
{
    /// <summary>
    /// 프레임 체크 클래스. 생성하면 좌측 상단에 프레임 표시함
    /// </summary>
    public class FreamDebug : MonoBehaviour
    {
        [SerializeField, Range(10, 150)]
        private int fontSize = 30;
        [SerializeField]
        private Color color = new Color(.0f, .0f, .0f, 1.0f);
        [SerializeField]
        private float width = 100f;
        [SerializeField]
        private float height = 100f;

        void OnGUI()
        {
            Rect _position = new Rect(width, height, Screen.width, Screen.height);

            float _fps = 1.0f / Time.deltaTime;
            float _ms = Time.deltaTime * 1000.0f;
            string _text = string.Format("{0:N1} FPS ({1:N1}ms)", _fps, _ms);

            GUIStyle _style = new GUIStyle();

            _style.fontSize = fontSize;
            _style.normal.textColor = color;

            GUI.Label(_position, _text, _style);
        }
    }

}