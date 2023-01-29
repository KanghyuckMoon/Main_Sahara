using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Utill.Addressable;
using Utill.Pattern; 

namespace UI.Screens
{
    public class LoadingPresenter : MonoBehaviour
    {
        [SerializeField]
        private UIDocument uiDocument;
        [SerializeField]
        private TipInfoSO tipInfoSO; 
        [SerializeField]
        private LoadingView loadingView;

        private void OnEnable()
        {
            loadingView.InitUIDocument(uiDocument); 
            loadingView.Cashing(); 
        }
        private void OnDisable()
        {
            Debug.Log("DD");
            loadingView.StopTween();
            StopAllCoroutines(); 
        }
        private void Awake()
        {
            uiDocument ??= GetComponent<UIDocument>(); 
            tipInfoSO ??= AddressablesManager.Instance.GetResource<TipInfoSO>("TipInfoSO");
        }
        private void Start()
        {
            loadingView.LoopLoadingImg();
            SelectTip(); 
        }

        private void SelectTip()
        {
            int _maxCount = tipInfoSO.tipList.Count;
            int _pickNum = Random.Range(0, _maxCount);
            StartCoroutine(LoopShowTip(_pickNum, _maxCount));  

        }

        private IEnumerator LoopShowTip(int _index, int _maxCnt)
        {
            int _idx = _index;
            float _delay = 2f;
            WaitForSeconds w = new WaitForSeconds(_delay);
            while(gameObject.activeSelf == true)
            {
                loadingView.SetTipText(tipInfoSO.tipList[_idx].tip, _delay);
                _idx = (++_idx) % _maxCnt; 
                yield return w;
            }
        }

    }

}
