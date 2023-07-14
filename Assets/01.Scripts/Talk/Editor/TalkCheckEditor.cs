using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Quest;
using Talk;
using GoogleSpreadSheet;

namespace Module.Talk
{

    public class TalkCheckEditor : EditorWindow
    {
        static TalkCheckEditor window;
        public List<QuestCondition> questConditions;
        private TalkDataSO talkDataSO;
        private TextSO textSO;
        private SerializedObject serializedObject;
        private SerializedProperty questConditionsProperty;

        [MenuItem("MoonTool/TalkCheckEditor")]
        public static void Open ()
        {
            if (window == null)
            {
                window = CreateInstance<TalkCheckEditor> ();
            }

            window.Show();
        }
        
        private void OnEnable()
        {
            // SerializedObject를 생성하고 필요한 SerializedProperty를 가져옵니다.
            serializedObject = new SerializedObject(this);
            questConditionsProperty = serializedObject.FindProperty("questConditions");
        }
        
        void OnGUI ()
        {
            talkDataSO = (TalkDataSO)EditorGUILayout.ObjectField(talkDataSO, typeof(TalkDataSO), false);
            textSO = (TextSO)EditorGUILayout.ObjectField(textSO, typeof(TextSO), false);
            
            // SerializedObject를 업데이트합니다.
            serializedObject.Update();

            // List 필드를 그려줍니다.
            EditorGUILayout.PropertyField(questConditionsProperty, true);

            // SerializedObject를 적용합니다.
            serializedObject.ApplyModifiedProperties();
            
            if (talkDataSO == null || textSO == null)
            {
                return;
            }

            if (GUILayout.Button("Debug TalkSO"))
            {
                LogTalk();
            }
        }

        void LogTalk()
        {
            if (!GetText())
            {
                RandomDefaultText();
            }
        }
        
        private bool GetText()
        {
            for (int i = 0; i < talkDataSO.talkDataList.Count; ++i)
            {
                TalkData _talkData = talkDataSO.talkDataList[i];
                if (ConditionCheck(_talkData))
                {
                    DebugLogTalkCode(_talkData.authorText, _talkData.talkText);
                    return true;
                }
            }
            return false;
        }
        
        private bool ConditionCheck(TalkData _talkData)
        {
            switch (_talkData.talkCondition)
            {
                case TalkCondition.Quest:
                    foreach (var questCondition in _talkData.questConditionList)
                    {
                        try
                        {
                            QuestCondition questData = questConditions.Find(x => x.questKey == questCondition.questKey);
                            if (questCondition.questState == QuestState.NotClear)
                            {
                                if (questData.questState == QuestState.Clear)
                                {
                                    return false;	
                                }
                            }
                            else if(questCondition.questState != questData.questState)
                            {
                                return false;
                            }
                        }
                        catch
                        {
                            //Debug.LogError($"Quest Error {questCondition.questKey}"); 
                            return false;
                        }
                    }
                    break;
                case TalkCondition.Position:
                    break;
                case TalkCondition.HandWork:
                    return false;
                case TalkCondition.CutScene:
                    return false;
            }
            return true;
        }
        
        private void RandomDefaultText()
        {
            DebugLogTalkCode(talkDataSO.defaultAutherCodeList[0], talkDataSO.defaultTalkCodeList[0]);
        }

        private void DebugLogTalkCode(string nameCode, string dialogueCode)
        {
            int index = 0;
            while (true)
            {
                string _nameText = textSO.GetText($"{nameCode}_{index}").Replace("\r", "");
                string fullText = textSO.GetText($"{dialogueCode}_{index}");

                Debug.Log($"{_nameText}\n{fullText}");
                index++;
                
                if (_nameText[0] is '!')
                {
                    switch (_nameText)
                    {
                        case "!END":
                            return;
                    }
                }
            }
        }
    }
    
    
}

