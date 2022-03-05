using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroids : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float respawnTime = 1.2f;

    [Range(1,4)]
    public float spawnDistance = 2f;

    private Vector2 screenBounds;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(asteroidWave());
    }
    void spawnAsteroid()
    {
        GameObject asteroid = Instantiate(asteroidPrefab);
        asteroid.GetComponent<Asteroid>().speed = Random.Range(1, 4);
        asteroid.transform.position = new Vector2(screenBounds.x * spawnDistance, Random.Range(-screenBounds.y, screenBounds.y));
    }
    
    IEnumerator asteroidWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnAsteroid();
        }
    }

}
