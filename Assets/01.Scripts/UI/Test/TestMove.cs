using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    public float speed;
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime); 

        if(Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up * 1080 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.down * 1080 * Time.deltaTime);
        }
    }
}
