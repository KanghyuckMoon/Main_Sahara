using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SortBtn
{
	[SerializeField] private UIDocument m_UIDocument;

	public Button generalBtn;

	void Start()
	{
		var rootElement = m_UIDocument.rootVisualElement;

		// connect visual elements with variables
		generalBtn = rootElement.Q<Button>("SortBtn");
		// connect handler with visual elements
		generalBtn.clickable.clicked += OnClickSortBtn;
	}

	private void OnClickSortBtn()
	{
		Debug.Log("click btn");
	}
}