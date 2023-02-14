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

        private static bool isSelecting; // ����ȭ�� 
        private static string nameCode, dialogueCode;
        private static int index;
        private static bool isDialogue; // ��ȭ��

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

        [ContextMenu("Select�׽�Ʈ")]
        public void TestSelect()
        {
            index = 0; 
            SetTexts("A00000002", "T00000002");
        }
        /// <summary>
        /// ó�� ��ȭ ���� �ڵ� �Ѱ��ֱ� 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_dialogue"></param>    
        public static void SetTexts(string _name, string _dialogue)
        {
            if (isDialogue == true) return; 
            DialoguePresenter.ActiveViewS(true); // Ȱ��ȭ �ϰ� 

            Logging.Log("�̸� �ڵ� : " + _name);
            Logging.Log("���� �ڵ� : " + _dialogue);
            nameCode = _name;
            dialogueCode = _dialogue;

            SetCodeToText(); 
        }

        /// <summary>
        /// �ڵ带 �ؽ�Ʈ�� ��ȯ�ؼ� UI�� �����Ű�� 
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
                        // �г� ����
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
            
            // �ؽ�Ʈ ó�� 
            DialogueView.SetNameTextA(_nameText); // ���ϴ� ��� �̸� ���� 
            StaticCoroutineManager.Instance.InstanceDoCoroutine(SetText(_dialogueText)); //
        }

        /// <summary>
        /// ��ȭ �� ����â ���� 
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
                string nameText = TextManager.Instance.GetText($"{nameCode}_{index}"); // ��ȭ �Ѿ�� �ڵ� 
                string dialogueText = TextManager.Instance.GetText($"{dialogueCode}_{index}"); // ���� ��ư �̸� 
                DialogueView.ActiveSelectButton(dialogueText, () =>
                {
                    ShowSelectedDialogue(nameText); 
                });
            }

            // ������ �� ������ üũ 
            // ���� ��ŭ ��Ʈ ���� ��ư Ȱ��ȭ 

            //_nameText = 
            // _dialogueText = 

        }

        /// <summary>
        /// ������ ��ȭ�� �Ѿ�� 
        /// </summary>
        /// <param name="_nameText"></param>
        private static void ShowSelectedDialogue(string _nameText)
        {
            index = 0; 
            string _name = _nameText.Replace("\r","");
            Logging.Log(_name + "Ŭ��");
            SetTexts("A" + _name.Substring(1, _name.Length-1), _name); // ���ÿ� �´� ��ȭ�� �Ѿ��
            DialogueView.ResetSelectButtons(); // ��ư ���� 
        }
        /// <summary>
        /// ��ȭ �� ���� ��ȭ �Ѿ�� �� üũ 
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
                    Logging.Log("��ȭ ����!!");

                    break; 
                }
                Logging.Log("��ȭ ����...");
                yield return null;
            }
        }

        /// <summary>
        /// ��ȭ�ؽ�Ʈ �ִϸ��̼� 
        /// </summary>
        /// <param name="_str"></param>
        /// <returns></returns>
        private static IEnumerator SetText(string _str)
        {
            Logging.Log("ó�� �ؽ�Ʈ");
            WaitForSeconds w  = new WaitForSeconds(0.03f);
            string _fullText = _str;
            string _nowText = "";
            for (int i = 0; i < _fullText.Length; i++)
            {
                _nowText += _fullText[i];
                DialogueView.SetDialogueTextA(_nowText);
                Logging.Log("For �ؽ�Ʈ");
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
        [ContextMenu("Ȱ��ȭ �׽�Ʈ")]
        public void TestActive()
        {
            DialogueView.ActiveViewS(true);
        }
    }
}
