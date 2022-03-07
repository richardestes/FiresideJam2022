using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Shake shake;

    public Sprite poweredDown;
    public bool dead = false;
    public int health = 100;
    [Range(1,10)]
    public int speed = 1;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (!shake) shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
    }

    private void Update()
    {
        if (dead) return;
        float verticalMovement = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.position += new Vector3(0, verticalMovement, 0);        
    }

    private void LateUpdate()
    {
        if (health <= 0)
        {
            dead = true;
            HandleDeath();
        }
        // Clamped movement
        if (!dead) transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 4), 0);
    }

    public void TakeDamage(int damage)
    {
        shake.CamShake();
        health = Mathf.Clamp(health - damage, 0, 100);
    }

    public void Heal(int healthAmount)
    {
        health = Mathf.Clamp(health + healthAmount, 0, 100);
    }

    private void HandleDeath()
    {
        spriteRenderer.sprite = poweredDown;
    }
}
