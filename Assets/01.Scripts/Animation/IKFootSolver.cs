using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effect;

public class IKFootSolver : MonoBehaviour
{
    [SerializeField]
    private string effectName = "NULL";
    [SerializeField] LayerMask terrainLayer = default;
    [SerializeField] Transform body = default;
    [SerializeField] IKFootSolver otherFoot = default;
    [SerializeField] float speed = 1;
    [SerializeField] float stepDistance = 4;
    [SerializeField] float stepLength = 4;
    [SerializeField] float stepHeight = 1;
    [SerializeField] Vector3 footOffset = default;
    float footSpacingX;
    float footSpacingZ;
    Vector3 oldPosition, currentPosition, newPosition;
    Vector3 oldNormal, currentNormal, newNormal;
    float lerp;

    private void OnEnable()
    {
        footSpacingX = transform.localPosition.x;
        footSpacingZ = transform.localPosition.z;
        currentPosition = newPosition = oldPosition = transform.position;
        currentNormal = newNormal = oldNormal = transform.up;
        lerp = 1;
    }

    // Update is called once per frame

    void Update()
    {
        transform.position = currentPosition;
        transform.up = currentNormal;

        Ray ray = new Ray(body.position + (-body.right * footSpacingX) + (-body.forward * footSpacingZ), Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit info, 10, terrainLayer.value))
        {
            if(Vector3.Distance(body.position, newPosition) < 3f)
			{
				Ray ray2 = new Ray(body.position + (-body.right * footSpacingX * 1.5f) + (-body.forward * footSpacingZ * 1.5f), Vector3.down);
				if (Physics.Raycast(ray2, out RaycastHit info2, 10, terrainLayer.value))
				{
					lerp = 0;
                    oldPosition = newPosition;
					oldNormal = newNormal;
					newPosition = info2.point;
					newNormal = info2.normal;
				}
			}
			else if ((Vector3.Distance(newPosition, info.point) > stepDistance && !otherFoot.IsMoving() && lerp >= 1))
			{
				if (effectName is not "NULL")
				{
					EffectManager.Instance.SetEffectDefault(effectName, transform.position, Quaternion.identity);
				}
				lerp = 0;
				newPosition = info.point;
				newNormal = info.normal;
			}
		}

        if (lerp < 1)
        {
            Vector3 tempPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
            tempPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

            currentPosition = tempPosition;
            currentNormal = Vector3.Lerp(oldNormal, newNormal, lerp);
            lerp += Time.deltaTime * speed;
        }
        else
        {
            oldPosition = newPosition;
            oldNormal = newNormal;
        }
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(newPosition, 0.5f);
    }



    public bool IsMoving()
    {
        return lerp < 1;
    }



}
