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
        public static void SetTexts(string _name, string _dialogue)
        {
            if (isDialogue == true) return; 
            DialoguePresenter.ActiveViewS(true); // 활성화 하고 

            Logging.Log("이름 코드 : " + _name);
            Logging.Log("내용 코드 : " + _dialogue);
            nameCode = _name;
            dialogueCode = _dialogue;

            SetCodeToText(); 
        }

        /// <summary>
        /// 코드를 텍스트로 변환해서 UI에 적용시키기 
        /// </summary>
        private static void SetCodeToText()
        {
            string _nameText = TextManager.Instance.GetText($"{nameCode}_{index}");
            string _dialogueText = TextManager.Instance.GetText($"{dialogueCode}_{index}");
            Logging.Log($"{nameCode}_{index}");
            Logging.Log($"{dialogueCode}_{index}"); 
            Logging.Log($"{_dialogueText}{_nameText}");

            if (_nameText[0] is '!')
            {
                switch (_nameText)
                {
                    case "!END\r":
                        index = 0;
                        ActiveViewS(false); 
                        return;
                    case "!TACTIVE\r":
                        index = 0;
                        ActiveViewS(false);
                        QuestManager.Instance.ChangeQuestActive(_dialogueText);
                        //UIConstructorManager.Instance.EventAlarmPresenter.TestEventAlarm();
                        return;
                    case "!TCLEAR\r":
                        // 패널 띄우기
                        //UIConstructorManager.Instance.EventAlarmPresenter.TestEventAlarm(); 
                        index = 0;
                        ActiveViewS(false);
                        QuestManager.Instance.ChangeQuestClear(_dialogueText);
                        return;
                    case "!CHOICE\r":
                        ActiveSelect(_nameText, _dialogueText); 
                        return; 
                }
            }
            
            // 텍스트 처리 
            DialogueView.SetNameTextA(_nameText); // 말하는 사람 이름 설정 
            StaticCoroutineManager.Instance.InstanceDoCoroutine(SetText(_dialogueText)); //
        }

        /// <summary>
        /// 대화 중 선택창 띄우기 
        /// </summary>
        /// <param name="_nameText"></param>
        /// <param name="_dialogueText"></param>
        private static void ActiveSelect(string _nameText, string _dialogueText)
        {
            int _count = int.Parse(_dialogueText);
            isSelecting = true;
            for (int i = 0; i < _count; i++)
            {
                
                ++index;
                string nameText = TextManager.Instance.GetText($"{nameCode}_{index}"); // 대화 넘어가는 코드 
                string dialogueText = TextManager.Instance.GetText($"{dialogueCode}_{index}"); // 선택 버튼 이름 
                DialogueView.ActiveSelectButton(dialogueText, () =>
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
        private static void ShowSelectedDialogue(string _nameText)
        {
            index = 0; 
            string _name = _nameText.Replace("\r","");
            Logging.Log(_name + "클릭");
            SetTexts("A" + _name.Substring(1, _name.Length-1), _name); // 선택에 맞는 대화로 넘어가기
            DialogueView.ResetSelectButtons(); // 버튼 삭제 
        }
        /// <summary>
        /// 대화 끝 다음 대화 넘어가는 거 체크 
        /// </summary>
        /// <returns></returns>
        private static IEnumerator CheckNextDialogue()
        {
            yield return new WaitForSeconds(0.1f);

            while (true)
            {
                if(Input.GetKeyDown(KeyCode.F))
                {
                    index++;
                    SetCodeToText();
                    Logging.Log("대화 다음!!");

                    break; 
                }
                Logging.Log("대화 루프...");
                yield return null;
            }
        }

        /// <summary>
        /// 대화텍스트 애니메이션 
        /// </summary>
        /// <param name="_str"></param>
        /// <returns></returns>
        private static IEnumerator SetText(string _str)
        {
            Logging.Log("처음 텍스트");
            WaitForSeconds w  = new WaitForSeconds(0.03f);
            string _fullText = _str;
            string _nowText = "";
            for (int i = 0; i < _fullText.Length; i++)
            {
                _nowText += _fullText[i];
                DialogueView.SetDialogueTextA(_nowText);
                Logging.Log("For 텍스트");
                yield return w;
            }
            StaticCoroutineManager.Instance.InstanceDoCoroutine(CheckNextDialogue());

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

        private static void ActiveViewS(bool _isActive)
        {
            isDialogue = _isActive;
            DialogueView.ActiveViewS(_isActive);
            UIManager.Instance.ActiveCursor(_isActive); 
        }

        public bool TestBool; 
        [ContextMenu("활성화 테스트")]
        public void TestActive()
        {
            DialogueView.ActiveViewS(true);
        }
    }
}
