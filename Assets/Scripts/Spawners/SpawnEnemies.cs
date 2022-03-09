using System.Collections;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{

    [SerializeField]
    private GameObject enemyPrefab;
    private GameManager manager;
    private Vector2 screenBounds;

    [Range(5f, 180f)]
    public float respawnTime = 180f;
    [Range(1f, 5f)]
    public float spawnDistance = 2f;
    public float scoreThreshold = 100f;

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if (!manager) manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        StartCoroutine(EnemyWave());
    }

    void SpawnEnemy()
    {
        print("Spawning pirate");
        GameObject pirateObj = Instantiate(enemyPrefab);
        Enemy enemy = pirateObj.GetComponent<Enemy>();
        enemy.speed = Random.Range(0.1f, 1f);
        enemy.fireRate = Random.Range(1f, 3f);
        enemy.transform.position = new Vector2(screenBounds.x * spawnDistance, Random.Range(-screenBounds.y, screenBounds.y));
    }

    IEnumerator EnemyWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            respawnTime = Mathf.Clamp(respawnTime - 5f, 5, 360); // decrease respawn time
            if (manager.score > scoreThreshold) SpawnEnemy();
        }
    }
}
