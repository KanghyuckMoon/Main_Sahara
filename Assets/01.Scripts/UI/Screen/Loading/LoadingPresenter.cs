using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Utill.Addressable;
using Utill.Pattern;
using GoogleSpreadSheet;
using Utill.Measurement;
using UI.Manager;

namespace UI.Loading
{
    public class LoadingPresenter : MonoBehaviour
    {
        [SerializeField]
        private UIDocument uiDocument;
        [SerializeField]
        private TipInfoSO tipInfoSO;
        [SerializeField]
        private LoadingView loadingView;

        // 프로퍼티 

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

            GetLoadingTip();
        }

        /// <summary>
        /// 특정 문자열부터 카운트만큼 쭉 시트에 있는거 팁SO로 가져오기 
        /// </summary>
        private void GetLoadingTip()
        {
            tipInfoSO.ClearList(); 

            // 첫번째 키 값 
            string _findKey = UIManager.Instance.TextKeySO.FindKey(TextKeyType.loadingTip);
            int _count = tipInfoSO.count; // 개수 
            string _cKey = _findKey.Substring(0,1); // 문자 처음 키 (A,B,C ...) 구분 
            string fmt = _cKey + "00000000.##";
            int _findInt = int.Parse(_findKey.Substring(1,_findKey.Length-1)); 
            // 0이 아닌 숫자를 찾아와서 그 수부터 시작 
            // 첫 번째 자리 수부터 1씩 늘린다 

            for (int i = _findInt; i < _findInt +_count; i++)
            {
                _findKey = i.ToString(fmt);
                tipInfoSO.AddTip(TextManager.Instance.GetText(_findKey));
            }

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
            while (gameObject.activeSelf == true)
            {
                loadingView.SetTipText(tipInfoSO.tipList[_idx], _delay);
                _idx = (++_idx) % _maxCnt;
                yield return w;
            }
        }

    }

}
