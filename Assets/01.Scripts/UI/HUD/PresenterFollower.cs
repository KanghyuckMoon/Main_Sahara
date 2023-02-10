using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{

    public class PresenterFollower 
    {
        private GameObject player; // �÷��̾�� �Ÿ�üũ�ؼ� UI ��ġ �����ϱ� ���� �ʿ� 
        private Camera cam; // ī�޶� 
        private Transform targetTrm; // UI ��� ������Ʈ 
        private VisualElement followElement; // UI   
        private Renderer renderer; 
        private int width, height;
        private int maxL = 25; // UI 
        //���
        private EntityPresenter e; 

        // ������Ƽ 
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
        /// hud ui�� ���� �����ǿ� ���� �̵� 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="worldPosition"></param>
        /// <param name="worldSize"></param>
        private void MoveToWorldPosition(VisualElement element, Vector3 worldPosition, Vector2 worldSize)
        {   
            // ������Ʈ ũ��(����) ���ϱ� 
            Bounds bounds = GetTotalMeshFilterBounds(targetTrm);
            // ���� ��ǥ�� ���� ��ũ�� ��ǥ��
            Rect rect = RuntimePanelUtils.CameraTransformWorldToPanelRect(element.panel, worldPosition + new Vector3(0,bounds.extents.y,0), worldSize, cam);

            float l = Mathf.Clamp(maxL - (Player.transform.position - targetTrm.position).magnitude, 0, maxL) / maxL; // 0~ 1
            Debug.Log("Length" + l);
            
            // UI�� ������Ʈ �߾ӿ� ������ width /2 �� �����ش� 
            float width = element.contentRect.width;
            // UI ����, �Ÿ��� �°� �����ش� 
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
        /// ������Ʈ�� bounds ��� 
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
