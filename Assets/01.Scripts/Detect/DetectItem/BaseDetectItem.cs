using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Effect;
using UnityEngine.Serialization;

namespace Detect
{
    public class BaseDetectItem : MonoBehaviour, IDetectItem
    {
        private static Dictionary<int, bool> isSpawnDic = new Dictionary<int, bool>();
        private static int index;

        public DetectItemType DetectItemType
        {
            get
            {
                return detectItemType;
            }

            set
            {
                detectItemType = value;
            }
        }

        [SerializeField]
        protected DetectItemType detectItemType;

        [SerializeField] 
        protected Transform targetHeightTransform;
        
        [SerializeField] 
        protected Transform targetTransform;
        
        [SerializeField] 
        protected Transform targetModel;
        
        [SerializeField] 
        protected Transform targetEffectTrm;
        
        [SerializeField] 
        protected string effectAddress;

        [SerializeField] 
        protected float heightUpTime = 2f;
            
        [SerializeField] 
        protected float shakeStrength = 0.5f;

        protected Vector3 upPos;

        [SerializeField]
        protected UnityEvent getoutEventBefore;
        
        [FormerlySerializedAs("getoutEvent")] [SerializeField]
        protected UnityEvent getoutEventAfter;
        
        [SerializeField]
        protected UnityEvent settingEventAfter;

        [SerializeField]
        protected bool isInitFalse = true;

        [SerializeField]
        private MeshRenderer outLineMeshRenderer;

		[SerializeField]
        private Material outlineMat;

		public List<Observer> Observers
        {
            get
            {
                return observers;
            }
        }

        private List<Observer> observers = new List<Observer>();
        
        public bool IsGetOut
        {
            get
            {
                return isGetOut;
            }
            set
            {
                isGetOut = value;
            }
        }

        protected bool isGetOut = false;
        private EffectObject effectObj;
		[SerializeField]
		private int curIndex = 0;

		[ContextMenu("SetIndex")]
		public void SetIndex()
		{
			curIndex = index++;
		}

		protected virtual void Awake()
		{
			settingEventAfter?.Invoke();

			if (isSpawnDic.TryGetValue(curIndex, out bool _bool))
			{
				if (_bool)
				{
					gameObject.SetActive(false);
				}
			}
			upPos = targetModel.position;
			targetModel.position = new Vector3(targetModel.position.x, targetHeightTransform.position.y, targetModel.position.z);
			if (isInitFalse)
			{
				targetModel.gameObject.SetActive(false);
			}
            else
            {
				outLineMeshRenderer = targetModel.GetComponentInChildren<MeshRenderer>();
				outLineMeshRenderer.materials = CopyMaterialsAndAdd();
			}
		}

        [ContextMenu("GetOut")]
        public virtual void GetOut()
        {
            if (isGetOut)
            {
                return;
			}
			getoutEventBefore?.Invoke();
            targetModel.gameObject.SetActive(true);
            isGetOut = true;
            Vector3 _movePos = upPos;
			effectObj = EffectManager.Instance.SetAndGetEffectDefault( effectAddress, targetEffectTrm.position, Quaternion.identity);

			if (!isInitFalse)
			{
				outLineMeshRenderer.materials = RemoveMateiral();
			}

			targetModel.DOMove(_movePos,  heightUpTime);
            targetTransform.DOShakePosition(heightUpTime, new Vector3(1,0,1) * shakeStrength).OnComplete(() =>
            {
				effectObj.Pool();
                gameObject.SetActive(false);
                isGetOut = true;
                getoutEventAfter?.Invoke();

				if (!isSpawnDic.ContainsKey(curIndex))
				{
					isSpawnDic.Add(curIndex, true);
				}

			});
            Invoke("CheckDetectRemove", heightUpTime);
		}

        private void CheckDetectRemove()
        {
            if(!isGetOut)
			{
				effectObj.Pool();
				gameObject.SetActive(false);
				isGetOut = true;
				getoutEventAfter?.Invoke();
				if (!isSpawnDic.ContainsKey(curIndex))
                {
				    isSpawnDic.Add(curIndex, true);
                }
			}
        }

        private Material[] CopyMaterialsAndAdd()
        {
			Material[] materials = new Material[outLineMeshRenderer.materials.Length + 1];
            int index = outLineMeshRenderer.materials.Length;
            for(int i = 0; i < index; ++i)
            {
                materials[i] = outLineMeshRenderer.materials[i];
			}
            materials[index] = outlineMat;
            return materials;
		}

        private Material[] RemoveMateiral()
		{
			Material[] materials = new Material[outLineMeshRenderer.materials.Length - 1];
			int index = outLineMeshRenderer.materials.Length - 1;
			for (int i = 0; i < index; ++i)
			{
				materials[i] = outLineMeshRenderer.materials[i];
			}
			//materials[index] = null;
			return materials;
		}

#if UNITY_EDITOR
        
        [SerializeField]
        private bool isDebugRender = true;

		[SerializeField]
        private MeshFilter debugRenderer;

        private static int debugDotCount = 0;
        

        private void OnDrawGizmos()
		{
            if(!isDebugRender)
            {
                return;
            }

            if(targetModel == null || debugRenderer == null)
            {
                if(targetModel != null)
                {
                    debugRenderer = targetModel.GetComponentInChildren<MeshFilter>();
				}
                return;
            }

            Vector3 renderPos = Vector3.zero;
            if(DetectItemType == DetectItemType.Slide)
            {
                renderPos = targetHeightTransform.transform.position;
                Gizmos.color = Color.yellow;
                Gizmos.DrawMesh(debugRenderer.sharedMesh, renderPos, debugRenderer.transform.rotation, debugRenderer.transform.lossyScale);
            }
            else
            {
                renderPos = targetModel.transform.position;
                if(debugRenderer.transform != targetModel.transform)
                {
                    renderPos += debugRenderer.transform.localPosition;
                }
			    renderPos.y = targetHeightTransform.position.y;
                Gizmos.DrawMesh(debugRenderer.sharedMesh, renderPos, debugRenderer.transform.rotation, debugRenderer.transform.lossyScale);
            }



		}


        [ContextMenu("DebugGetOut")]
        public void DebugGetOut()
        {
            upPos = targetModel.position;
            targetModel.position = new Vector3(targetModel.position.x, targetHeightTransform.position.y, targetModel.position.z);
            
            debugDotCount++;
            targetModel.gameObject.SetActive(true);
            isGetOut = true;
            Vector3 _movePos = upPos;
            var tween2 = targetModel.DOMove(_movePos,  heightUpTime);
            var tween = targetTransform.DOShakePosition(heightUpTime, new Vector3(1,0,1) * shakeStrength).OnComplete(() =>
            {   
                debugDotCount--;
                if(debugDotCount == 0)
                {
                    DG.DOTweenEditor.DOTweenEditorPreview.Stop();
                }
            });
            DG.DOTweenEditor.DOTweenEditorPreview.PrepareTweenForPreview(tween,false,false,true);
            DG.DOTweenEditor.DOTweenEditorPreview.PrepareTweenForPreview(tween2,false,false,true);
            DG.DOTweenEditor.DOTweenEditorPreview.Start();
        }

        [ContextMenu("DebugMoveHeight")]
        public void DebugMoveHeight()
        {
            Vector3 curPos = targetModel.position;
            targetModel.gameObject.SetActive(true);
            debugDotCount++;
            upPos = new Vector3(targetModel.position.x, targetHeightTransform.position.y, targetModel.position.z);
            var tween2 = targetModel.DOMove(upPos,  1f).OnComplete(() =>
            {   
                debugDotCount--;
                if(debugDotCount == 0)
                {
                    DG.DOTweenEditor.DOTweenEditorPreview.Stop();
                }
                targetModel.position = curPos;
            });
            DG.DOTweenEditor.DOTweenEditorPreview.PrepareTweenForPreview(tween2,false,false,true);
            DG.DOTweenEditor.DOTweenEditorPreview.Start();
        }

        public LayerMask debug_LayerMask;
        
        [ContextMenu("SetHeight")]
        public void SetHeight()
        {
            RaycastHit _hit;
            if (Physics.Raycast(transform.position, Vector3.down, out _hit,50,  debug_LayerMask))
            {
                transform.position = _hit.point;
            }
        }

        [ContextMenu("SetPosIsModelPos")]
        public void SetPosIsModelPos()
        {
            transform.position = targetModel.transform.position;
        }
        
        [ContextMenu("SetEffectPosIsThisPos")]
        public void SetEffectPosIsThisPos()
        {
            targetEffectTrm.position = transform.position;
        }
        
        
        [ContextMenu("SetModelPosIsThisPos")]
        public void SetModelPosIsThisPos()
        {
            targetModel.position = transform.position;
        }
        
#endif
	}
}
