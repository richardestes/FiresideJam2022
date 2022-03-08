using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Spaceship spaceship;
    private Rigidbody2D rb;
    private Vector2 screenBounds;

    public float speed = 10f;
    public float damage = 10f;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        spaceship = GameObject.FindGameObjectWithTag("Spaceship").GetComponent<Spaceship>();
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
            int damageInt = Mathf.RoundToInt(damage);
            spaceship.TakeDamage(damageInt);
            Destroy(gameObject);
        }
    }

    public void SetDamage(float scale)
    {
        if (scale >= 1 && scale <= 10)
        {
            damage = 10;
        }
        else if (scale >= 10 && scale <= 20)
        {
            damage = 15;
        }
        else if (scale >= 20 && scale <= 25)
        {
            damage = 20;
        }
        else damage = 10;
    }
}
