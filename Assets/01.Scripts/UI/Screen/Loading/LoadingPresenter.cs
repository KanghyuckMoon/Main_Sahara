using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Utill.Addressable;
using Utill.Pattern;
using GoogleSpreadSheet;
using Utill.Measurement;
using UI.Manager;
using DG.Tweening;
using UnityEngine.SceneManagement;

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

        private bool _isCoroutine = true;
        private static bool _isUnload = false;
        // 프로퍼티 
        public static bool IsUnload { get => _isUnload; set => _isUnload = value; }
        private void OnEnable()
        {
            loadingView.InitUIDocument(uiDocument);
            loadingView.Cashing();
            loadingView.Init();
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

        private void Start()
        {
            loadingView.LoopLoadingImg();
            SelectTip();
            StartCoroutine(AnimatePanel());
        }

        private void Update()
        {
            if (_isUnload == true && _isCoroutine == false)
                StartCoroutine(UnLoadLoading());
        }

        public IEnumerator UnLoadLoading()
        {
            _isUnload = false;
            loadingView.Panels.style.opacity = 0f;
            loadingView.ParentElement.style.backgroundColor = new StyleColor(Color.clear);
            float _dur = 0.5f;
            var _list = loadingView.GetDecos();
            bool _isUp = true;
            foreach (var _e in _list)
            {
                if (_isUp == true)
                {
                    DOTween.To(() => _e.transform.position, (x) => _e.transform.position = x, new Vector3(-3000, 0, 0), _dur);
                    _isUp = false;
                    continue;
                }
                DOTween.To(() => _e.transform.position, (x) => _e.transform.position = x, new Vector3(3000, 0, 0), _dur);
                _isUp = true;
            }
            yield return new WaitForSeconds(1f);
              SceneManager.UnloadSceneAsync("TipScene", UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);

        }
        private IEnumerator AnimatePanel()
        {
            _isCoroutine = true;
            float _duration = 0.5f;
            var _list = loadingView.GetDecos();
            foreach (var _e in _list)
            {
                // _e.style.translate = new StyleTranslate(new Translate(0, 0));
                DOTween.To(() => _e.transform.position, (x) => _e.transform.position = x, Vector3.zero, _duration);

                //if(_e.ClassListContains("panel_deco_top")== true)
                //{
                //    Debug.Log("TOP");
                //    _e.RemoveFromClassList("panel_deco_top");
                //    _e.AddToClassList("panel_deco_init");
                //}
                //else if (_e.ClassListContains("panel_deco_bot") == true)
                //{
                //    Debug.Log("BOT");
                //    _e.RemoveFromClassList("panel_deco_bot");
                //    _e.AddToClassList("panel_deco_init");
                //}
            }
            yield return new WaitForSeconds(_duration);
            loadingView.Panels.style.opacity = 1f;
            _isCoroutine = false;
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
            string _cKey = _findKey.Substring(0, 1); // 문자 처음 키 (A,B,C ...) 구분 
            string fmt = _cKey + "00000000.##";
            int _findInt = int.Parse(_findKey.Substring(1, _findKey.Length - 1));
            // 0이 아닌 숫자를 찾아와서 그 수부터 시작 
            // 첫 번째 자리 수부터 1씩 늘린다 

            for (int i = _findInt; i < _findInt + _count; i++)
            {
                _findKey = i.ToString(fmt);
                tipInfoSO.AddTip(TextManager.Instance.GetText(_findKey));
            }

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
