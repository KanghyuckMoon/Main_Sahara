#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Quest
{
	[CustomEditor(typeof(QuestDataSO))]
	public class QuestDataSOEditor : Editor
	{
		static QuestDataSO questDataSO = null;
		public override void OnInspectorGUI()
		{
			questDataSO = (QuestDataSO)target;
			base.OnInspectorGUI();
		}

		private void OnEnable()
		{
			SceneView.onSceneGUIDelegate -= QuestDataSOEditor.OnSceneGUI;
			SceneView.onSceneGUIDelegate += QuestDataSOEditor.OnSceneGUI;
		}

		void OnDestroy()
		{
			SceneView.onSceneGUIDelegate -= QuestDataSOEditor.OnSceneGUI;
		}

		static void OnSceneGUI(SceneView sceneView)
		{
			if (questDataSO is null || questDataSO.questConditionType is not QuestConditionType.Position)
			{
				return;
			}
			Handles.color = Color.red;
			Handles.DrawWireDisc(questDataSO.goalPosition, Vector3.up, questDataSO.distance, 3);
			
			float value = Handles.ScaleSlider(
				questDataSO.distance,
				questDataSO.goalPosition,
				Vector3.up,
				Quaternion.identity,
				questDataSO.distance <= 0 ? 10 : questDataSO.distance,
				0.1f
			);

			if (value < 0 || float.IsNaN(value))
			{
				value = 0;
			}

			questDataSO.distance = value;
		}




		private void OnSceneGUI()
		{
		}
	}
}
#endif