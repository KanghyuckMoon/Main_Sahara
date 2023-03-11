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

namespace UI.Dialogue
{
    public class DialoguePresenter : MonoBehaviour, IScreen
    {
        [SerializeField]
        private UIDocument uiDocument; 
        [SerializeField]
        private DialogueView dialogueView;

        private static bool isSelecting; // 선택화면 
        private static string nameCode, dialogueCode;
        private static int index;
        private static bool isDialogue; // 대화중

        private Action _endCallback = null; // 대화 끝났을 떄 호출

        // 프로퍼티 
        public IUIController UIController { get; set; }

        private void OnEnable()
        {
            dialogueView.Cashing();
            dialogueView.Init(); 
        }
        private void Awake()
        {
            uiDocument = GetComponent<UIDocument>();
            dialogueView.InitUIDocument(uiDocument); 
        }
        [ContextMenu("테스트")]
        public void Test()
        {
            ActiveViewS(false);
        }
        [ContextMenu("Select테스트")]
        public void TestSelect()
        {
            index = 0; 
            SetTexts("A00000002", "T00000002");
        }
        /// <summary>
        /// 처음 대화 시작 코드 넘겨주기 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_dialogue"></param>    
        public void SetTexts(string _name, string _dialogue,Action _callback = null)
        {
            if (isDialogue == true) return; 
            ActiveViewS(true); // 활성화 하고 

            Logging.Log("이름 코드 : " + _name);
            Logging.Log("내용 코드 : " + _dialogue);
            nameCode = _name;
            dialogueCode = _dialogue;
            _endCallback = _callback; 

            SetCodeToText();
            StartCoroutine(CheckNextDialogue()); 
        }

        /// <summary>
        /// 코드를 텍스트로 변환해서 UI에 적용시키기 
        /// </summary>
        private void SetCodeToText()
        {
            string _nameText = TextManager.Instance.GetText($"{nameCode}_{index}");
            fullText = TextManager.Instance.GetText($"{dialogueCode}_{index}");
            Logging.Log($"{nameCode}_{index}");
            Logging.Log($"{dialogueCode}_{index}"); 
            Logging.Log($"{fullText}{_nameText}");

            if (_nameText[0] is '!')
            {
                switch (_nameText)
                {
                    case "!END":
                        index = 0;
                        ActiveViewS(false); 
                        return;
                    case "!TACTIVE":
                        index = 0;
                        ActiveViewS(false);
                        QuestManager.Instance.ChangeQuestActive(fullText);
                        //UIConstructorManager.Instance.EventAlarmPresenter.TestEventAlarm();
                        return;
                    case "!TCLEAR":
                        // 패널 띄우기
                        //UIConstructorManager.Instance.EventAlarmPresenter.TestEventAlarm(); 
                        index = 0;
                        ActiveViewS(false);
                        QuestManager.Instance.ChangeQuestClear(fullText);
                        return;
                    case "!CHOICE":
                        ActiveSelect(_nameText, fullText); 
                        return;
                    case "!SHOP":
                        UIController.GetScreen<ShopPresenter>(ScreenType.Shop).ActivetShop(ShopType.BuyShop); // 구매창 활성화 
                        ActiveViewS(false);
                        return;
                    case "!SELL":
                        UIController.GetScreen<ShopPresenter>(ScreenType.Shop).ActivetShop(ShopType.SellShop); // 판매 창 활성화 
                        ActiveViewS(false);
                        return;
                    case "!GIVE":
                        return;
                    case "!GIVES":
                        return; 
                }
            }

            // 텍스트 처리 
            this.dialogueView.SetNameTextA(_nameText); // 말하는 사람 이름 설정 
            StartCoroutine(SetText());
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
            for (int i = 0; i < _count; i++)
            {
                
                ++index;
                string nameText = TextManager.Instance.GetText($"{nameCode}_{index}"); // 대화 넘어가는 코드 
                string dialogueText = TextManager.Instance.GetText($"{dialogueCode}_{index}"); // 선택 버튼 이름 
                this.dialogueView.ActiveSelectButton(dialogueText, () =>
                {
                    ShowSelectedDialogue(nameText); 
                });
            }

            // 선택지 몇 개인지 체크 
            // 개수 만큼 시트 돌고 버튼 활성화 

            //_nameText = 
            // _dialogueText = 

        }

        /// <summary>
        /// 선택한 대화로 넘어가기 
        /// </summary>
        /// <param name="_nameText"></param>
        private void ShowSelectedDialogue(string _nameText)
        {
            index = 0; 
            string _name = _nameText.Replace("\r","");
            Logging.Log(_name + "클릭");
            SetTexts("A" + _name.Substring(1, _name.Length-1), _name); // 선택에 맞는 대화로 넘어가기
            this.dialogueView.ResetSelectButtons(); // 버튼 삭제 
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
//                        SetTextInstant(targetText);
                        yield return null;
                    }
                    else
                    {
                        index++;
                        SetCodeToText();
                        Debug.Log("대화 다음!!");

                    }

                    //break; 
                }
                Debug.Log("대화 루프...");
                yield return null;
            }
        }

        private bool isTexting = false; // 텍스트가 출력되고 있는 중인가 
        private string fullText;
        private const string TransStr = "<alpha=#00>"; // 투명 문자 
        /// <summary>
        /// 대화텍스트 애니메이션 
        /// </summary>
        /// <param name="_str"></param>
        /// <returns></returns>
        private IEnumerator SetText()
        {
            Debug.Log("처음 텍스트");
            WaitForSeconds w  = new WaitForSeconds(0.03f);
            //targetText = _str;
            string _nowText = "";
            string _targetText = ""; 
            isTexting = true; 
            for (int i = 0; i <= fullText.Length; i++)
            {
                _targetText = fullText.Substring(0,i)+ TransStr + fullText.Substring(i);
                if (isTexting == false)  
                {
                    // 모든 텍스트 바로 보여주기
                    //this.dialogueView.SetDialogueTextA(fullText);
                    this.dialogueView.SetDialogueTextA(fullText);
                    yield break;
                }
                //_nowText += fullText[i];
                //this.dialogueView.SetDialogueTextA(_nowText);
                this.dialogueView.SetDialogueTextA(_targetText);
                Debug.Log("For 텍스트");
                yield return w;
            }
            isTexting = false;
          //  StartCoroutine(CheckNextDialogue()); 
        //    StaticCoroutineManager.Instance.InstanceDoCoroutine(CheckNextDialogue());
        }

        private void SetTextInstant(string _str)
        {
            isTexting = false; 
            StopCoroutine(SetText());
            this.dialogueView.SetDialogueTextA(_str);
        }

        public void SetText(string _name, string _dialogue)
        {
            dialogueView.SetNameText(_name);
            dialogueView.SetDialogueText(_dialogue);
        }
        public bool ActiveView()
        {
            return dialogueView.ActiveScreen(); 
        }

        public void ActiveView(bool _isActive)
        {
            dialogueView.ActiveView(_isActive); 
        }
       
        private void ActiveViewS(bool _isActive)
        {
            isDialogue = _isActive;
            dialogueView.ActiveViewS(_isActive);
            UIManager.Instance.ActiveCursor(_isActive); 

            if (_isActive == false)
            {
                _endCallback?.Invoke();
                _endCallback = null; 
            }
        }

        public bool TestBool;


        [ContextMenu("활성화 테스트")]
        public void TestActive()
        {
            this.dialogueView.ActiveViewS(true);
        }
        #region regacy 상점 
        /*
        /// <summary>
        /// 상점 선택창 띄우기 
        /// </summary>
        private void ActiveShopSelect()
        {
            List<string> _nameList = new List<string>();
            string _buyName = TextManager.Instance.GetText(UIManager.Instance.TextKeySO.FindKey(TextKeyType.shopBuy));
            string _sellName = TextManager.Instance.GetText(UIManager.Instance.TextKeySO.FindKey(TextKeyType.shopSell));
            _nameList.Add(_buyName);
            _nameList.Add(_sellName);

            this.dialogueView.ActiveSelectButton(_buyName, () =>
            {
                // 구매 
                UIController.GetScreen<ShopPresenter>(ScreenType.Shop).ActivetShop(ShopType.BuyShop);
                ActiveViewS(false);

            });

            this.dialogueView.ActiveSelectButton(_sellName, () =>
            {
                // 판매 
                UIController.GetScreen<ShopPresenter>(ScreenType.Shop).ActivetShop(ShopType.SellShop);
                ActiveViewS(false);

            });
        }
          */
        #endregion

    }
}
