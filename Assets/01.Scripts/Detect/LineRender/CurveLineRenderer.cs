using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveLineRenderer : MonoBehaviour
{
	public LineRenderer linerenderer;

	public Transform startPoint;
	public Transform middlePoint_A;


	public Transform middlePoint_B;
	public Transform endPoint;

	public float vertexCount = 12;

	void Update()
	{
		var pointList = new List<Vector3>();
		Vector3 middlePoint = Vector3.Lerp(middlePoint_A.position, middlePoint_B.position, 0.5f);

		for (float ratio = 0; ratio <= 1; ratio += 1 / vertexCount)
		{
			var tangent1 = Vector3.Lerp(startPoint.position, middlePoint_A.position, ratio);
			var tangent2 = Vector3.Lerp(middlePoint_A.position, middlePoint, ratio);
			var curve = Vector3.Lerp(tangent1, tangent2, ratio);

			pointList.Add(curve);
		}

		pointList.RemoveAt(pointList.Count - 1);

		for (float ratio = 0; ratio <= 1; ratio += 1 / vertexCount)
		{
			var tangent1 = Vector3.Lerp(middlePoint, middlePoint_B.position, ratio);
			var tangent2 = Vector3.Lerp(middlePoint_B.position, endPoint.position, ratio);
			var curve = Vector3.Lerp(tangent1, tangent2, ratio);

			pointList.Add(curve);
		}

		linerenderer.positionCount = pointList.Count;
		linerenderer.SetPositions(pointList.ToArray());
	}
}