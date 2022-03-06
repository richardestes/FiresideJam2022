using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(0.1f,1f)]
    public float speed = 0.1f;
    private Rigidbody2D rb;
    private Vector2 screenBounds;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    private void Update()
    {
        if (transform.position.x < screenBounds.x * -1.5f) // off-screen
            Destroy(gameObject);
    }
}
