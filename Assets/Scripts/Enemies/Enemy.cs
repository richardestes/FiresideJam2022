
using UnityEngine;

public class Enemy: MonoBehaviour
{
    private Spaceship spaceship;
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    [SerializeField]
    private ProjectileSpawner projectileSpawner;

    [Range(5f,30f)]
    public float speed = 5f;
    [Range(1f, 5f)]
    public float fireRate = 3f;
    public int damage = 15;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0); // move object to left
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        spaceship = GameObject.FindGameObjectWithTag("Spaceship").GetComponent<Spaceship>();
        if (!projectileSpawner) projectileSpawner = gameObject.GetComponent<ProjectileSpawner>();
        projectileSpawner.fireTime = fireRate;
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
            spaceship.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
