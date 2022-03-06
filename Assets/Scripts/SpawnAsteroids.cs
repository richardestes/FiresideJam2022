using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroids : MonoBehaviour
{
    public GameObject asteroidPrefab;

    [Range(1,10)]
    public float respawnTime = 1;

    public List<Sprite> asteroidSprites;

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
        GameObject asteroidObj = Instantiate(asteroidPrefab);
        Asteroid asteroid = asteroidObj.GetComponent<Asteroid>();
        asteroidObj.GetComponent<SpriteRenderer>().sprite = asteroidSprites[Random.Range(0, asteroidSprites.Count)];
        asteroid.speed = Random.Range(1, 10);
        float randomScale = Random.Range(1.5f, 5f);
        asteroid.transform.localScale = new Vector3(randomScale, randomScale, asteroid.transform.position.z);
        asteroid.transform.position = new Vector2(screenBounds.x * spawnDistance, Random.Range(-screenBounds.y, screenBounds.y));
        Vector2 asteroidSize = asteroidObj.GetComponent<SpriteRenderer>().bounds.size;
        float sizeDamageRatio = asteroidSize.x * 10;
        asteroid.SetDamage(sizeDamageRatio);
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
