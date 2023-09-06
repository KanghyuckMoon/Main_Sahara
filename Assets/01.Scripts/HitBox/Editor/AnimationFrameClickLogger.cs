#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Reflection;
using UnityEditorInternal;
using HitBox;
using System.Linq;
//using System.Diagnostics;

public class AnimationClickLoggerWindow : EditorWindow
{
	public HitBoxDatasSO hitboxDataSO;
	public CapsuleColEditor boxColEditor;

	private double lastClickTime;
	private MethodInfo animationWindowStateEventMethod;
	private object lastAnimationWindowState;

	private bool isUpdate;

	[MenuItem("MoonTool/Animation Click Logger")]
	public static void ShowWindow()
	{
		GetWindow<AnimationClickLoggerWindow>("Animation Click Logger");
	}

	void OnGUI()
	{
		hitboxDataSO = (HitBoxDatasSO)EditorGUILayout.ObjectField(hitboxDataSO, typeof(HitBoxDatasSO), false);
		boxColEditor = (CapsuleColEditor)EditorGUILayout.ObjectField(boxColEditor, typeof(CapsuleColEditor), true);
		isUpdate = EditorGUILayout.Toggle("Is Update", isUpdate);
		if(hitboxDataSO != null)
		{
			boxColEditor.hitBoxDataSO = hitboxDataSO;
		}

		if(boxColEditor != null)
		{
			if(GUILayout.Button("OffsetPositionZero"))
			{
				boxColEditor.OffsetResetAndPositionZero();
			}

			if(GUILayout.Button("PositionToOffset"))
			{
				boxColEditor.PositionToOffset();
			}

			if(GUILayout.Button("Save Access"))
			{
				UnityEditor.EditorUtility.SetDirty(hitboxDataSO);
			}
		}
		


	}

		private void OnEnable()
	{
		lastClickTime = EditorApplication.timeSinceStartup;
		EditorApplication.update += CheckForAnimationFrameClick;

		Assembly editorAssembly = Assembly.GetAssembly(typeof(EditorWindow));
		System.Type animationWindowStateType = editorAssembly.GetType("UnityEditor.AnimationWindowState");
		animationWindowStateEventMethod = animationWindowStateType.GetMethod("OnEvent", BindingFlags.Instance | BindingFlags.NonPublic);
	}

	private void OnDisable()
	{
		EditorApplication.update -= CheckForAnimationFrameClick;
	}

	private void CheckForAnimationFrameClick()
	{
		if(!isUpdate)
		{
			return;
		}

		if(hitboxDataSO == null || boxColEditor == null) 
		{
			return;
		}
			lastClickTime = EditorApplication.timeSinceStartup;

			// Get the current AnimationWindowState using reflection
			System.Type animationWindowType = typeof(Editor).Assembly.GetType("UnityEditor.AnimationWindow");
			object animationWindow = EditorWindow.GetWindow(animationWindowType);
			PropertyInfo stateProperty = animationWindowType.GetProperty("state", BindingFlags.Instance | BindingFlags.NonPublic);
			object animationWindowState = stateProperty.GetValue(animationWindow);

			// Check if the AnimationWindowState has a currentFrame property (for Unity versions before 2022.2)
			PropertyInfo currentFrameProperty = animationWindowState.GetType().GetProperty("currentFrame", BindingFlags.Instance | BindingFlags.Public);
			if (currentFrameProperty != null)
			{
				int frameNumber = (int)currentFrameProperty.GetValue(animationWindowState, null);
				//Debug.Log("Clicked on frame: " + frameNumber);

				// Get animation events for the clicked frame
				AnimationClip clip = ((AnimationWindow)animationWindow).animationClip;
				if(clip == null)
				{
				return;
				}
				float _time = AnimationHitBoxEditor.GetAnimationWindowTime();
				AnimationEvent _event = clip.events.FirstOrDefault(x => x.time == _time);
				if(_event == null)
				{
					return;		
				}
				if (_event.functionName == "OnHitBox")
				{
					ShowHitBox(_event.stringParameter);
				}
			}
			else
			{
				// Check if the AnimationWindowState has a currentTime property (for Unity versions starting from 2022.2)
				PropertyInfo currentTimeProperty = animationWindowState.GetType().GetProperty("currentTime", BindingFlags.Instance | BindingFlags.Public);
				if (currentTimeProperty != null)
				{
					float currentTime = (float)currentTimeProperty.GetValue(animationWindowState, null);
					int frameNumber = Mathf.FloorToInt(currentTime * 60); // Assuming a frame rate of 60 FPS
					//Debug.Log("Clicked on frame: " + frameNumber);

					// Get animation events for the clicked frame
					AnimationClip clip = ((AnimationWindow)animationWindow).animationClip;
					if (clip == null)
					{
						return;
					}
					float _time = AnimationHitBoxEditor.GetAnimationWindowTime();
					AnimationEvent _event = clip.events.FirstOrDefault(x => x.time == _time);
					if (_event == null)
					{
						return;
					}
					if (_event.functionName == "OnHitBox")
					{
						ShowHitBox(_event.stringParameter);
					}
				}
			}
	}

	private void ShowHitBox(string str)
	{
		if(hitboxDataSO.hitBoxDataDic.ContainsKey(str))
		{
			Debug.Log(str);
			HitBoxDataList hitBoxDataList = hitboxDataSO.hitBoxDataDic[str];
			boxColEditor.SetHitBox(hitBoxDataList.hitBoxDataList[0]);
		}
		else
		{
			Debug.Log($"해당 SO에 키가 포함되어 있지 않음 : {str}");
		}
	}

}
#endif