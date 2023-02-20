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

        // ������Ƽ 

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
        /// Ư�� ���ڿ����� ī��Ʈ��ŭ �� ��Ʈ�� �ִ°� ��SO�� �������� 
        /// </summary>
        private void GetLoadingTip()
        {
            tipInfoSO.ClearList(); 

            // ù��° Ű �� 
            string _findKey = UIManager.Instance.TextKeySO.FindKey(TextKeyType.loadingTip);
            int _count = tipInfoSO.count; // ���� 
            string _cKey = _findKey.Substring(0,1); // ���� ó�� Ű (A,B,C ...) ���� 
            string fmt = _cKey + "00000000.##";
            int _findInt = int.Parse(_findKey.Substring(1,_findKey.Length-1)); 
            // 0�� �ƴ� ���ڸ� ã�ƿͼ� �� ������ ���� 
            // ù ��° �ڸ� ������ 1�� �ø��� 

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
