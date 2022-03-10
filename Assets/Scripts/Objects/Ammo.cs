using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private Spaceship spaceship;
    private Crosshair crosshair;
    private Rigidbody2D rb;
    private Vector2 screenBounds;

    [Range(1f, 5f)]
    public float speed;
    public int ammoAmount = 25;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0); // move object to left
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        spaceship = GameObject.FindGameObjectWithTag("Spaceship").GetComponent<Spaceship>();
        crosshair = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Crosshair>();
        speed *= 1000f;
    }

    private void Update()
    {
        if (transform.position.x < screenBounds.x * -1.5f) // off-screen
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Spaceship")
        {
            crosshair.Reload(ammoAmount);
            Destroy(gameObject);
        }
    }
}
