using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DemoEnd : MonoBehaviour
{
	[SerializeField] private Image fadeImage;
	[SerializeField] private Image[] screenShots;
	private int index = 0;

	private void Start()
	{
		fadeImage.DOFade(0, 1f);
		ChangeScreenShot();
	}

	private void ChangeScreenShot()
	{
		screenShots[index % (screenShots.Length)].DOFade(0, 3f);
		screenShots[(index + 1) % (screenShots.Length)].DOFade(1, 3f);
		index++;
		Invoke("ChangeScreenShot", 5f);
	}

	public void GameExit()
	{
		Application.Quit();
	}
}
