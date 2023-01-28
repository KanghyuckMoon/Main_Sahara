using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMove : MonoBehaviour
{
    private int random = 0;
	private float time = 0f;
	public float speed = 1f;
	public float power = 1.2f;

	private void OnEnable()
	{
		//random = Random.Range(0, 4);
	}

	// Update is called once per frame
	void Update()
    {
		time += Time.deltaTime * speed;

		switch (random)
		{
			//哭率
			case 0:
				transform.Translate(new Vector3(Mathf.Cos(time) * power, 0, 0));
				break;
			//菊率
			case 1:
				transform.Translate(new Vector3(0, 0, Mathf.Cos(time) * power));
				break;
			//坷弗率
			case 2:
				transform.Translate(new Vector3(Mathf.Cos(-time) * power, 0, 0));
				break;
			//第率
			case 3:
				transform.Translate(new Vector3(0, 0, Mathf.Cos(-time) * power));
				break;
		}
    }
}
