using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace StaffRollUp
{
	public class StaffRollUp : MonoBehaviour
	{
		public void Start()
		{
			GetComponent<RectTransform>().DOAnchorPosY(1000f, 7f).OnComplete(() => SceneManager.LoadScene("Title"));
		}
	}
}

