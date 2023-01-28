using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TargetMove : MonoBehaviour
{
	[SerializeField]
	private List<Transform> targetPoint = new List<Transform>();
	[SerializeField]
	private float speed = 2f;
	[SerializeField]
	private float rotateSpeed = 2f;

	private Vector3 rotate = Vector3.zero;
	private int index = 0;

	private void Start()
	{
		Move();
	}

	private void Move()
	{
		transform.DOMove(targetPoint[index].position, Vector3.Distance(transform.position, targetPoint[index].position) / speed).OnComplete(() => 
		{
			index++;
			if(index == targetPoint.Count)
			{
				index = 0;
				Invoke("Move", 2f);
			}
			else
			{
				Move();
			}
		});
	}

	private void Update()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		rotate.x += vertical;
		rotate.y += horizontal;

		transform.eulerAngles = rotate;

	}



}
