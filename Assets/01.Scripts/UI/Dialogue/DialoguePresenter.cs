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

        private static bool isSelecting; // ����ȭ�� 
        private static string nameCode, dialogueCode;
        private static int index;
        private static bool isDialogue; // ��ȭ��

        private Action _endCallback = null; // ��ȭ ������ �� ȣ��

        // ������Ƽ 
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
        [ContextMenu("�׽�Ʈ")]
        public void Test()
        {
            ActiveViewS(false);
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
        public void SetTexts(string _name, string _dialogue,Action _callback = null)
        {
            if (isDialogue == true) return; 
            ActiveViewS(true); // Ȱ��ȭ �ϰ� 

            Logging.Log("�̸� �ڵ� : " + _name);
            Logging.Log("���� �ڵ� : " + _dialogue);
            nameCode = _name;
            dialogueCode = _dialogue;
            _endCallback = _callback; 

            SetCodeToText();
            StartCoroutine(CheckNextDialogue()); 
        }

        /// <summary>
        /// �ڵ带 �ؽ�Ʈ�� ��ȯ�ؼ� UI�� �����Ű�� 
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
                        // �г� ����
                        //UIConstructorManager.Instance.EventAlarmPresenter.TestEventAlarm(); 
                        index = 0;
                        ActiveViewS(false);
                        QuestManager.Instance.ChangeQuestClear(fullText);
                        return;
                    case "!CHOICE":
                        ActiveSelect(_nameText, fullText); 
                        return;
                    case "!SHOP":
                        UIController.GetScreen<ShopPresenter>(ScreenType.Shop).ActivetShop(ShopType.BuyShop); // ����â Ȱ��ȭ 
                        ActiveViewS(false);
                        return;
                    case "!SELL":
                        UIController.GetScreen<ShopPresenter>(ScreenType.Shop).ActivetShop(ShopType.SellShop); // �Ǹ� â Ȱ��ȭ 
                        ActiveViewS(false);
                        return;
                    case "!GIVE":
                        return;
                    case "!GIVES":
                        return; 
                }
            }

            // �ؽ�Ʈ ó�� 
            this.dialogueView.SetNameTextA(_nameText); // ���ϴ� ��� �̸� ���� 
            StartCoroutine(SetText());
        //    StaticCoroutineManager.Instance.InstanceDoCoroutine(SetText(_dialogueText)); //
        }

        /// <summary>
        /// ��ȭ �� ����â ���� 
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
                string nameText = TextManager.Instance.GetText($"{nameCode}_{index}"); // ��ȭ �Ѿ�� �ڵ� 
                string dialogueText = TextManager.Instance.GetText($"{dialogueCode}_{index}"); // ���� ��ư �̸� 
                this.dialogueView.ActiveSelectButton(dialogueText, () =>
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
        private void ShowSelectedDialogue(string _nameText)
        {
            index = 0; 
            string _name = _nameText.Replace("\r","");
            Logging.Log(_name + "Ŭ��");
            SetTexts("A" + _name.Substring(1, _name.Length-1), _name); // ���ÿ� �´� ��ȭ�� �Ѿ��
            this.dialogueView.ResetSelectButtons(); // ��ư ���� 
        }
        /// <summary>
        /// ��ȭ �� ���� ��ȭ �Ѿ�� �� üũ 
        /// </summary>
        /// <returns></returns>
        private IEnumerator CheckNextDialogue()
        {
            yield return new WaitForSeconds(0.03f);

            while (isDialogue == true)
            {
                if(Input.GetKeyDown(KeyCode.F))
                {
                    if (isTexting == true) // �ؽ�Ʈ�� �������̾��ٸ� 
                    {
                        isTexting = false;
//                        SetTextInstant(targetText);
                        yield return null;
                    }
                    else
                    {
                        index++;
                        SetCodeToText();
                        Debug.Log("��ȭ ����!!");

                    }

                    //break; 
                }
                Debug.Log("��ȭ ����...");
                yield return null;
            }
        }

        private bool isTexting = false; // �ؽ�Ʈ�� ��µǰ� �ִ� ���ΰ� 
        private string fullText;
        private const string TransStr = "<alpha=#00>"; // ���� ���� 
        /// <summary>
        /// ��ȭ�ؽ�Ʈ �ִϸ��̼� 
        /// </summary>
        /// <param name="_str"></param>
        /// <returns></returns>
        private IEnumerator SetText()
        {
            Debug.Log("ó�� �ؽ�Ʈ");
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
                    // ��� �ؽ�Ʈ �ٷ� �����ֱ�
                    //this.dialogueView.SetDialogueTextA(fullText);
                    this.dialogueView.SetDialogueTextA(fullText);
                    yield break;
                }
                //_nowText += fullText[i];
                //this.dialogueView.SetDialogueTextA(_nowText);
                this.dialogueView.SetDialogueTextA(_targetText);
                Debug.Log("For �ؽ�Ʈ");
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


        [ContextMenu("Ȱ��ȭ �׽�Ʈ")]
        public void TestActive()
        {
            this.dialogueView.ActiveViewS(true);
        }
        #region regacy ���� 
        /*
        /// <summary>
        /// ���� ����â ���� 
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
                // ���� 
                UIController.GetScreen<ShopPresenter>(ScreenType.Shop).ActivetShop(ShopType.BuyShop);
                ActiveViewS(false);

            });

            this.dialogueView.ActiveSelectButton(_sellName, () =>
            {
                // �Ǹ� 
                UIController.GetScreen<ShopPresenter>(ScreenType.Shop).ActivetShop(ShopType.SellShop);
                ActiveViewS(false);

            });
        }
          */
        #endregion

    }
}
