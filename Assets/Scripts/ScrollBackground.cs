using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    [Range(0.5f,3f)]
    public float scrollSpeed = 1f;

    void Update()
    {
        transform.position += new Vector3(scrollSpeed * -1 * Time.deltaTime, 0);

        if (transform.position.x < -19) //continuous scroll
        {
            transform.position = new Vector3(19f, transform.position.y, transform.position.z);
        }
    }
}
