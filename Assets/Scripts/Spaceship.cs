using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, 100);
    }

    public void Heal(int healthAmount)
    {
        health = Mathf.Clamp(health + healthAmount, 0, 100);
    }

    private void Update()
    {
        if (health == 0)
            Destroy(this.gameObject);
    }
}
