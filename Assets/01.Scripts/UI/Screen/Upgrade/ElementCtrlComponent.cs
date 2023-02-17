using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Upgrade
{
    /// <summary>
    ///  이동, 확대 축소 
    /// </summary>
    public class ElementCtrlComponent : MonoBehaviour
    {
        private VisualElement target;
        private bool isCtrl;

        private float xMoveValue, yMoveValue, moveSpeed; 
        // 프로퍼티 
        public bool IsCtrl
        {
            get => isCtrl;
            set => isCtrl = value; 
        }
        public ElementCtrlComponent(VisualElement _v)
        {
            this.target = _v; 
        }

        public void InputKey()
        {

        }
        public void Move()
        {
            // 이동 
            Vector3 mapPos = target.transform.position;
            //Vector2 mapPos = new Vector2(mapView.Map.style.left.value.value, mapView.Map.style.top.value.value);

            float mapX, mapY;
            mapX = Mathf.Clamp(mapPos.x + -xMoveValue * (moveSpeed - target.transform.scale.x) * Time.deltaTime,
                                                // -mapView.MapRect.width - width,width * 0.5f);
                                                -(target.contentRect.width /** mapScale.x*/) * 0.5f, (target.contentRect.width /** mapScale.x*/) * 0.5f);

            mapY = Mathf.Clamp(mapPos.y + yMoveValue * (moveSpeed - target.transform.scale.y) * Time.deltaTime,
                                                //  -mapView.MapRect.height - height,height * 0.5f);
                                                -(target.contentRect.height /** mapScale.y*/) * 0.5f, (target.contentRect.height /** mapScale.y*/) * 0.5f);

            this.target.transform.position = new Vector3(mapX, mapY, 0);
        }

        public void Zoom()
        {

        }
            
    }

}
