using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroids : MonoBehaviour
{
    private Vector2 screenBounds;

    [SerializeField]
    private GameObject asteroidPrefab;

    [Range(1, 10)]
    public float respawnTime = 1;
    [Range(1, 4)]
    public float spawnDistance = 2f;
    public List<Sprite> asteroidSprites;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(AsteroidWave());
    }
    void SpawnAsteroid()
    {
        GameObject asteroidObj = Instantiate(asteroidPrefab);
        Asteroid asteroid = asteroidObj.GetComponent<Asteroid>();
        int randomSeed = Random.Range(0, asteroidSprites.Count);
        asteroidObj.GetComponent<SpriteRenderer>().sprite = asteroidSprites[randomSeed];
        asteroid.speed = Random.Range(1, 10);
        float randomScale = Random.Range(1.5f, 5f);
        asteroid.transform.localScale = new Vector3(randomScale, randomScale, asteroid.transform.position.z);
        asteroid.transform.position = new Vector3(screenBounds.x * spawnDistance, Random.Range(-screenBounds.y, screenBounds.y), 4);
        Vector2 asteroidSize = asteroidObj.GetComponent<SpriteRenderer>().bounds.size;
        float sizeDamageRatio = asteroidSize.x * 10;
        asteroid.SetDamage(sizeDamageRatio);
    }

    IEnumerator AsteroidWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnAsteroid();
        }
    }

}
