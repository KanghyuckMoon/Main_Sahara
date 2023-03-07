using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UI.EventManage;
namespace UI
{
    public class MapLiner : MonoBehaviour
    {
        private VisualElement element; 
        private RectTransform panel;
        private MapInfo mapInfo;
        //      private 

        [SerializeField]
        private int x;

        [SerializeField]
        private int y;
        private Vector2 originSize; 
        public RectTransform Panel => panel; 
        private void Awake()
        {
            panel = GetComponent<RectTransform>();
            mapInfo = new MapInfo(); 
        }
        private void Start()
        {
            Init();
        }

        private void OnEnable()
        {
            EventManager.Instance.StartListening(EventsType.UpdateMapPos, (x) => UpdatePos((Vector2)x));
            EventManager.Instance.StartListening(EventsType.UpdateMapScale, (x) => UpdateScale((Vector2)x));
        }

        private void OnDisable()
        {
            EventManager.Instance.StopListening(EventsType.UpdateMapPos, (x) => UpdatePos((Vector2)x));
            EventManager.Instance.StopListening(EventsType.UpdateMapScale, (x) => UpdateScale((Vector2)x));
        }

        public void UpdateUI()
        {
            panel.anchoredPosition = element.transform.position;
            panel.transform.localScale = element.transform.scale;
        }
        public void UpdatePos(Vector2 _pos)
        {
            panel.anchoredPosition =  new Vector2(_pos.x  *x ,_pos.y* y);
        }
        public void UpdateScale( Vector2 _scale)
        {
            panel.sizeDelta = mapInfo.UIMapSize;
            panel.sizeDelta = new Vector2(originSize.x + originSize.x * _scale.x, originSize.y + originSize.y * _scale.y);
        //    panel.transform.localScale = _scale;
        }
        private void Init()
        {
            panel.sizeDelta = mapInfo.UIMapSize;
            originSize = mapInfo.UIMapSize;

        }

    }

}
