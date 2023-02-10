using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{

    public class PresenterFollower 
    {
        private GameObject player; // 플레이어랑 거리체크해서 UI 위치 조정하기 위해 필요 
        private Camera cam; // 카메라 
        private Transform targetTrm; // UI 띄울 오브젝트 
        private VisualElement followElement; // UI   
        private Renderer renderer; 
        private int width, height;
        private int maxL = 25; // UI 
        //디벅
        private EntityPresenter e; 

        // 프로퍼티 
        private GameObject Player
        {
            get
            {
                player ??= GameObject.FindWithTag("Player");
                return player; 
            }
        }
        public PresenterFollower(EntityPresenter e,VisualElement _follow ,Transform _target, Renderer _renderer)
        {
            this.e = e;

            this.followElement = _follow;
            this.targetTrm = _target;
            this.renderer = _renderer; 

            this.cam = Camera.main;
            width = Screen.width;
            height = Screen.height; 
        }

        public void UpdateUI()
        {
            if (Player == null) return; 
            MoveToWorldPosition(followElement, targetTrm.position, new Vector2(e.a, e.b));  
        }

        /// <summary>
        /// hud ui를 월드 포지션에 따라 이동 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="worldPosition"></param>
        /// <param name="worldSize"></param>
        private void MoveToWorldPosition(VisualElement element, Vector3 worldPosition, Vector2 worldSize)
        {   
            // 오브젝트 크기(높이) 구하기 
            Bounds bounds = GetTotalMeshFilterBounds(targetTrm);
            // 월드 좌표를 현재 스크린 좌표로
            Rect rect = RuntimePanelUtils.CameraTransformWorldToPanelRect(element.panel, worldPosition + new Vector3(0,bounds.extents.y,0), worldSize, cam);

            float l = Mathf.Clamp(maxL - (Player.transform.position - targetTrm.position).magnitude, 0, maxL) / maxL; // 0~ 1
            Debug.Log("Length" + l);
            
            // UI가 오브젝트 중앙에 오도록 width /2 를 더해준다 
            float width = element.contentRect.width;
            // UI 높이, 거리에 맞게 더해준다 
            float height = element.contentRect.height + (bounds.extents.y *2 * 100) * l;
            height = element.contentRect.height; 
            Debug.Log("Renderer Exteneds" + bounds.extents);
            // Don't set scale to 0 or a negative number.
            
            //Vector2 layoutSize = element.layout.size;
            //Vector2 scale = layoutSize.x > 0 && layoutSize.y > 0 ? rect.size / layoutSize : Vector2.one * 1e-5f;

            Vector2 pos = rect.position + new Vector2(-width * 0.5f,-height); 
            element.transform.position = pos;
            //element.transform.scale = new Vector3(scale.x, scale.y, 1);
        }

        /// <summary>
        /// 오브젝트의 bounds 얻기 
        /// </summary>
        /// <param name="objectTransform"></param>
        /// <returns></returns>
        private Bounds GetTotalMeshFilterBounds(Transform objectTransform)
        {
            var renderer = objectTransform.GetComponent<Renderer>();
            var result = renderer != null ? renderer.bounds : new Bounds();

            foreach (Transform transform in objectTransform)
            {
                var bounds = GetTotalMeshFilterBounds(transform);
                result.Encapsulate(bounds.min);
                result.Encapsulate(bounds.max);
            }
            var scaledMin = result.min;
            scaledMin.Scale(objectTransform.localScale);
            result.min = scaledMin;
            var scaledMax = result.max;
            scaledMax.Scale(objectTransform.localScale);
            result.max = scaledMax;
            return result;
        }
    }

}
