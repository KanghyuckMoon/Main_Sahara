using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI.EventManage;
using UnityEngine.UI.Extensions; 

namespace UI
{
    public class MapLiner : MonoBehaviour
    {
        private RectTransform panel;
        private RectTransform center; 
        //private MapInfo mapInfo;

        private UILineRenderer uiLineRenderer; 

//        private 

        [SerializeField]
        private int x;

        [SerializeField]
        private int y;
        private Vector2 originSize; 
        public RectTransform Panel => panel; 
        private void Awake()
        {
            center = GetComponent<RectTransform>(); 
            panel = transform.Find("Panel").GetComponent<RectTransform>();
            uiLineRenderer = GetComponentInChildren<UILineRenderer>(); 
           // mapInfo = new MapInfo(); 
        }
        private void Start()
        {
            //Init();
        }

        private void OnEnable()
        {
            EventManager.Instance.StartListening(EventsType.UpdateMapPos, (x) => UpdatePos((Vector2)x));
            EventManager.Instance.StartListening(EventsType.UpdateMapScale, (x) => UpdateScale((Vector2)x));
            EventManager.Instance.StartListening(EventsType.ClearMapLine, () => ClearMapLine());
            EventManager.Instance.StartListening(EventsType.UpdateMapLine, (x) => UpdateMapLine((List<Vector2>)x));
        
        }

        private void OnDisable()
        {
            EventManager.Instance.StopListening(EventsType.UpdateMapPos, (x) => UpdatePos((Vector2)x));
            EventManager.Instance.StopListening(EventsType.UpdateMapScale, (x) => UpdateScale((Vector2)x));
            EventManager.Instance.StopListening(EventsType.UpdateMapLine, (x) => UpdateMapLine((List<Vector2>)x));
            EventManager.Instance.StartListening(EventsType.ClearMapLine, () => ClearMapLine());
        }

        public void UpdatePos(Vector2 _pos)
        {
           // panel.transform.position = new Vector2(_pos.x * x, _pos.y * y); 
            panel.anchoredPosition =  new Vector2(_pos.x  *x ,_pos.y* y);
        }
        public void UpdateScale(Vector2 _scale)
        {
            //panel.sizeDelta = mapInfo.UIMapSize;
            //panel.sizeDelta = new Vector2(originSize.x + originSize.x * _scale.x, originSize.y + originSize.y * _scale.y);
            center.localScale = _scale;
            //    panel.transform.localScale = _scale;
        }
        public void UpdateMapLine(List<Vector2> _vec)
        {
            uiLineRenderer.Points = null; 

            int _maxCount = _vec.Count;
            uiLineRenderer.Points = new Vector2[_maxCount];
            for (int i =0; i< _maxCount; i++)
            {
                uiLineRenderer.Points[i] = new Vector2(_vec[i].x,- _vec[i].y);
            }
            uiLineRenderer.SetAllDirty(); 
        }

        public void ClearMapLine()
        {
            uiLineRenderer.Points = null;
            uiLineRenderer.SetAllDirty();

        }

        private void Init()
        {
            //panel.sizeDelta = mapInfo.UIMapSize;
            //originSize = mapInfo.UIMapSize;

        }

    }

}
