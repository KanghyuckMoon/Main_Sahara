using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using Utill.Pattern;
using DG.Tweening;
using Random = UnityEngine.Random;

namespace Item
{
public class MPBall : MonoBehaviour
{
	[SerializeField] 
	private AnimationCurve moveCurve;

	[SerializeField] 
	private string hitEffect;

	private GameObject target;
	
	public void SetMPBall(Vector3 startPos, Action<int> _mpAction, int _addMp, GameObject _target)
	{
		target = _target;
		transform.rotation = Quaternion.identity;
		transform.position = startPos;
		gameObject.SetActive(true);
		StartCoroutine(MoveToTarget(startPos, _mpAction, _addMp));
	}

	private IEnumerator MoveToTarget(Vector3 startPos,  Action<int> _mpAction, int _addMp)
	{

		float startAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
		float startAngle2 = Random.Range(0f, 360f) * Mathf.Deg2Rad;
		transform.rotation = Quaternion.AngleAxis(startAngle, Vector3.forward);
		Vector3 spreadPos = startPos + new Vector3(Mathf.Cos(startAngle), Mathf.Sin(startAngle), Mathf.Cos(startAngle2));
		//spreadPos.z = startPos.z;

		float delay = Random.Range(0.5f, 0.7f);
		float delayCurernt = 0f;
		while (delayCurernt < delay)
		{
			yield return null;
			transform.position = LinearBezierPoint(moveCurve.Evaluate(delayCurernt), startPos, spreadPos);
			delayCurernt += Time.deltaTime;
		}

		transform.DOKill();

		float speed = 2f;
		float time = 0f;

		float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
		Vector3 shotPos = transform.position;
		Vector3 prevPoint = startPos;
		Vector3 dir = Vector3.zero;

		while (time < 1f)
		{
			yield return null;
			
			//Position
			Vector3 targetPos = target.transform.position;
			targetPos.y += 1f;
			transform.position = Vector3.Slerp(shotPos, targetPos, moveCurve.Evaluate(time));// LinearBezierPoint(moveCurve.Evaluate(time), shotPos, targetPos);

			//Rotate
			if (time > 0.01f)
			{
				dir = transform.position - prevPoint;
				angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

				transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

				prevPoint = transform.position;
			}

			time += Time.deltaTime * speed;
		}
		
		_mpAction?.Invoke(_addMp);
		Effect.EffectManager.Instance.SetEffectDefault(hitEffect, transform.position, Quaternion.identity);
		ObjectPoolManager.Instance.RegisterObject("MPBall", gameObject);
		gameObject.SetActive(false);
	}


	private Vector3 LinearBezierPoint(float t, Vector3 start, Vector3 end)
	{
		return start + (t * (end - start));
	}
	}
}

