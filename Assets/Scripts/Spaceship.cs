using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public int health = 100;
    [Range(1,10)]
    public int speed = 1;

    private void Update()
    {
        float verticalMovement = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.position += new Vector3(0, verticalMovement, 0);        
    }

    private void LateUpdate()
    {
        // Clamp movement
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 4), 0);

        if (health == 0)
        {
            //TODO: Change this to HandleDeath()
            // Change sprite to powered down
            // Disable input
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, 100);
    }

    public void Heal(int healthAmount)
    {
        health = Mathf.Clamp(health + healthAmount, 0, 100);
    }
}
