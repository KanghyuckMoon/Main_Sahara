    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using GoogleSpreadSheet;
using Utill.Measurement;
using Quest;
using Utill.Coroutine;
using UI.ConstructorManager;
using UI;
using UI.Base;
using UI.Manager;
using UI.Shop;
using System;
using UI.EventManage;
using UI.Upgrade;
using Inventory;
    using TimeManager;
    using DG.Tweening; 
    
    namespace UI.Dialogue
{
    public class DialoguePresenter : MonoBehaviour, IScreen
    {
        [SerializeField]
        private UIDocument uiDocument; 
        [SerializeField]
        private DialogueView dialogueView;

        private  bool isSelecting; // 선택화면 
        private  string nameCode, dialogueCode;
        private  int index;
        [SerializeField]
        private bool isDialogue; // 대화중
        
        private Action _endCallback = null; // 대화 끝났을 떄 호출

        private bool isSelect = false; // 대화 선택창이 떴는가 
        // 프로퍼티 
        public IUIController UIController { get; set; }

        public bool IsDialogue
        {
            get => isDialogue;
            set => isDialogue = value; 
        }
            
        private void OnEnable()
        {
            dialogueView.Cashing();
            dialogueView.Init(); 
            EventManager.Instance.StartListening(EventsType.SetCanDialogue,(x) => isDialogue = (bool)x);
        }

        private void OnDisable()
        {
            EventManager.Instance.StopListening(EventsType.SetCanDialogue,(x) => isDialogue = (bool)x);
        }

        private void Awake()
        {
            uiDocument = GetComponent<UIDocument>();
            dialogueView.InitUIDocument(uiDocument); 
        }

        /// <summary>
        /// 처음 대화 시작 코드 넘겨주기 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_dialogue"></param>    
        public void StartDialogue(string _name, string _dialogue, Action _callback = null)
        {
            // 처음 대화시 isDialogue를 true로 설정해준다. 
            if (isDialogue == true) return;

            StartText(_name, _dialogue, _callback);
        }

        /// <summary>
        /// 새로운 코드 받아서 대화 시작 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_dialogue"></param>
        /// <param name="_callback"></param>
        private void StartText(string _name, string _dialogue, Action _callback = null)
        {
            ActiveViewS(true); // 활성화 하고 
    
            StopAllCoroutines();
            index = 0; 
            nameCode = _name;
            dialogueCode = _dialogue;
            fullText = "";
            this.dialogueView.SetDialogueTextA(fullText);
            if (_callback != null)
            {
                _endCallback = _callback;
            }

            SetCodeToText();
            StartCoroutine(CheckNextDialogue()); 
        }

        /// <summary>
        /// 코드를 텍스트로 변환해서 UI에 적용시키기 
        /// </summary>
        private void SetCodeToText()
        {
            string _nameText = TextManager.Instance.GetText($"{nameCode}_{index}").Replace("\r", "");
            fullText = TextManager.Instance.GetText($"{dialogueCode}_{index}");
            
            if (_nameText[0] is '!')
            {
                switch (_nameText)
                {   
                    case "!END":
                        index = 0;
                        fullText = "";
                        ActiveViewS(false); 
                        return;
                    case "!TACTIVE":
                        //index = 0;
                        //ActiveViewS(false);
                        QuestManager.Instance.ChangeQuestActive(fullText);
                        //UIConstructorManager.Instance.EventAlarmPresenter.TestEventAlarm();
                        return;
                    case "!TCLEAR":
                        // 패널 띄우기
                        //UIConstructorManager.Instance.EventAlarmPresenter.TestEventAlarm(); 
                        //index = 0;
                        //ActiveViewS(false);
                        QuestManager.Instance.ChangeQuestClearForce(fullText);
                        return;
                    case "!CHOICE":
                        ActiveSelect(_nameText, fullText);
                        //StopCoroutine(StartCoroutine(SetText())); // 대화 체크 코루틴 종료 
                        return;
                    case "!SHOP":
//                        UIController.GetScreen<ShopPresenter>(ScreenType.Shop).ActivetShop(ShopType.BuyShop); // 구매창 활성화 
                        UIController.ActiveScreen(Keys.BuyUI); // 구매창 활성화 
                        index = 0; 
                        ActiveViewS(false);
                        isDialogue = true; 
                        return;
                    case "!SELL":
                        //UIController.GetScreen<ShopPresenter>(ScreenType.Shop).ActivetShop(ShopType.SellShop); // 판매 창 활성화 
                        UIController.ActiveScreen(Keys.SellUI);
                        index = 0; 
                        ActiveViewS(false);
                        isDialogue = true; 
                        return;
                    case "!SMITH":
                        //UIController.GetScreen<UpgradePresenter>(ScreenType.Upgrade).ActiveView();
                        UIController.ActiveScreen(Keys.SmithUI);
                        index = 0; 
                        ActiveViewS(false);
                        isDialogue = true; 
                        return; 
                    case "!MOVE":
                        string _name = fullText.Replace("\r","");
                        StartText(_name.Replace("T","A"), _name);
                        return; 
                    case "!GIVE":
                        InventoryManager.Instance.AddItem(fullText.Replace("\r",""));
                        return;
                    case "!GIVES":
                        var _text = fullText.Split(',');
                        string _code = _text[0];
                        int _count = int.Parse(_text[1]);
                        InventoryManager.Instance.AddItem(_code, _count);
                        return; 
                }
            }

            // 텍스트 처리 
            this.dialogueView.SetNameTextA(_nameText); // 말하는 사람 이름 설정 
            StartCoroutine(SetText());
      //      StartCoroutine(TypeText()); 
            //    StaticCoroutineManager.Instance.InstanceDoCoroutine(SetText(_dialogueText)); //
        }

        /// <summary>
        /// 대화 중 선택창 띄우기 
        /// </summary>
        /// <param name="_nameText"></param>
        /// <param name="_dialogueText"></param>
        private void ActiveSelect(string _nameText, string _dialogueText)
        {
            int _count = int.Parse(_dialogueText);
            isSelecting = true;
            // 커서 활성화 
            UIManager.Instance.ActiveCursor(true);
            for (int i = 0; i < _count; i++)
            {
                
                ++index;
                string nameText = TextManager.Instance.GetText($"{nameCode}_{index}"); // 대화 넘어가는 코드 
                string dialogueText = TextManager.Instance.GetText($"{dialogueCode}_{index}"); // 선택 버튼 이름 
                this.dialogueView.ActiveSelectButton(dialogueText, () =>
                {
                    ShowSelectedDialogue(nameText);
                    isSelecting = false; 
                });
            }

        }

        /// <summary>
        /// 선택한 대화로 넘어가기 
        /// </summary>
        /// <param name="_nameText"></param>
        private void ShowSelectedDialogue(string _nameText)
        {
            isTexting = true; 
            string _name = _nameText.Replace("\r","");
            StartText("A" + _name.Substring(1, _name.Length-1), _name); // 선택에 맞는 대화로 넘어가기
            this.dialogueView.ResetSelectButtons(); // 버튼 삭제 
            UIManager.Instance.ActiveCursor(false);// 커서 비활성화 

        }
        /// <summary>
        /// 대화 끝 다음 대화 넘어가는 거 체크 
        /// </summary>
        /// <returns></returns>
        private IEnumerator CheckNextDialogue()
        {
            yield return new WaitForSeconds(0.03f);

            while (isDialogue == true)
            {
                if(Input.GetKeyDown(KeyCode.F))
                {
                    if (isTexting == true) // 텍스트가 진행중이었다면 
                    {
                        isTexting = false;  
                        // 트윈 실행 
                        dialogueView.Tween(true);
//                        SetTextInstant(targetText);
                        yield return null;
                    }
                    else
                    {
                        if(isSelecting == false)
                        {
                            index++;
                            SetCodeToText();
                            Debug.Log("대화 다음!!");

                        }
                        
                    }

                    //break; 
                }
                Debug.Log("대화 루프...");
                yield return null;
            }
        }

        [SerializeField]
        private bool isRIchText = false;
        [SerializeField]
        private bool isTexting = false; // 텍스트가 출력되고 있는 중인가 
        private string fullText;
        private const string TransStr = "<alpha=#00>"; // 투명 문자 
        private const string TransEndStr = "</alpha>"; // 투명 문자 
        
        private bool CheckColorTag(string _fullText)
        {
            bool _hasColorTag = false || _fullText.Contains("<color");
            
            //   _fullText.Insert()
            for (int i = 0; i < _fullText.Length; i++)
            {
                if ((_fullText[i] == '<' && _fullText[i+1] == 'c') 
                    || (_fullText[i] == '<' && _fullText[i+1] == '/' && _fullText[i+2] == 'c'))
                {
                    // color 태그 찾아 
                    int  _tagEndIdx = _fullText.IndexOf('>', i);
                    fullText = _fullText.Insert(_tagEndIdx + 1, TransStr);
                    _fullText = fullText;  
                    i = _tagEndIdx + 1; 
                }
            }

            return _hasColorTag; 
        }
        /// <summary>
        /// 대화텍스트 애니메이션 
        /// </summary>
        /// <param name="_str"></param>
        /// <returns></returns>
        private IEnumerator SetText()
        {
            WaitForSeconds w  = new WaitForSeconds(0.03f);
            string _nowText = "";
            string _targetText = ""; 
            isTexting = true;

            // 컬러 태그 있나 
            bool _is = CheckColorTag(fullText);

            for (int i = 0; i < fullText.Length; i++)
            {
                // 태그는 표시X - >  건너뛰기 
                if (fullText[i] == '<')
                {
                    // 태그 길이 구하기 
                    int tagEndIndex = fullText.IndexOf('>', i);
                    if (tagEndIndex != -1)
                    {
                        //_targetText += fullText.Substring(i, tagEndIndex - i + 1);
                        i = tagEndIndex + 1;
                    }
        
                    // 타이핑 애니메이션을 위한 alpha 태그 있으면 삭제하기 
                    //int nextTagEndIndex = fullText.IndexOf('>', i);
                    // i = nextTagEndIndex + 1;
                    if (fullText[i] == '<' && fullText[i + 1] == 'a')
                    {
                        int _endIdx = fullText.IndexOf('>', i);
                        fullText = fullText.Remove(i, _endIdx- i +1);
                        //var replace = fullText.Replace(TransStr, "");
                    }   
                    
                }

                _targetText = fullText.Substring(0, i) + TransStr + fullText.Substring(i);// + TransEndStr;
                
                if (isTexting == false)  
                {
                    // 모든 텍스트 바로 보여주기
                    this.dialogueView.SetDialogueTextA(fullText);
                    yield break;
                }
                this.dialogueView.SetDialogueTextA(_targetText);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     Debug.Log("For 텍스트");
                yield return w;
            }
            this.dialogueView.SetDialogueTextA(fullText);

            isTexting = false;
        }

        IEnumerator SetRichText()
        {
            if (string.IsNullOrEmpty(fullText)) yield break;

            string _targetText = "";
            int _index = 0; 
            isTexting = true;
  
            while (_index < fullText.Length)
            {
                if (isTexting == false)  
                {
                    // 모든 텍스트 바로 보여주기
                    this.dialogueView.SetDialogueTextA(fullText);
                    yield break;
                }
                
                if (fullText[_index] == '<')
                {
                    int tagEndIndex = fullText.IndexOf('>', _index);
                    if (tagEndIndex != -1)
                    {
                        //_targetText += fullText.Substring(_index, tagEndIndex - _index + 1);
                        _index = tagEndIndex + 1;
                    }
                }
                else
                {
                    _targetText += fullText[_index];
                    this.dialogueView.SetDialogueTextA(_targetText);

                    _index++;
                    yield return new WaitForSeconds(0.03f);
                }
            }
            isTexting = false;
        }
        public bool ActiveView()
        {
            throw new NotImplementedException();
        }

        public void ActiveView(bool _isActive)
        {
            dialogueView.ActiveView(_isActive); 
        }
       
        private void ActiveViewS(bool _isActive)
        {
            isDialogue = _isActive;
            dialogueView.ActiveViewS(_isActive);
            StaticTime.UITime = _isActive ? 0f : 1f;
            EventManager.Instance.TriggerEvent(EventsType.SetPlayerCam, _isActive);
            EventManager.Instance.TriggerEvent(EventsType.SetUIInput, ! _isActive);
            //UIManager.Instance.ActiveCursor(_isActive); 

            if (_isActive == false)
            {
                _endCallback?.Invoke();
                _endCallback = null; 
                StopAllCoroutines();
            }
        }

    }
}
