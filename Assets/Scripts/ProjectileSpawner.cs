using System.Collections;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    private Vector2 screenBounds;
    [SerializeField]
    private GameManager manager;
    [SerializeField]
    private GameObject projectilePrefab;
    [Range(1f, 5f)]
    public float fireTime = 3f;

    public float spawnDistance = 0.5f;

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if (!manager) manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        StartCoroutine(ProjectileWave());
    }

    private void Update()
    {
        if (transform.position.x < screenBounds.x * -1.5f) // off-screen
            Destroy(gameObject);
    }

    void FireProjectile()
    {
        print("Firing projectile");
        GameObject projectileObj = Instantiate(projectilePrefab);
        Projectile projectile = projectileObj.GetComponent<Projectile>();
        projectile.transform.position = transform.position; // set spawn location to projectile spawner GameObject
    }

    IEnumerator ProjectileWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireTime);
            FireProjectile();
        }
    }    
    
}
