using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlanets : MonoBehaviour
{
    public GameObject planetPrefab;

    [Range(1,10)]
    public float respawnTime = 1;

    public List<Sprite> planetSprites;

    [Range(1,4)]
    public float spawnDistance = 2f;

    private Vector2 screenBounds;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(planetWave());
    }

    void spawnPlanet()
    {
        GameObject planetObj = Instantiate(planetPrefab);
        Planet planet = planetObj.GetComponent<Planet>();
        planetObj.GetComponent<SpriteRenderer>().sprite = planetSprites[Random.Range(0, planetSprites.Count)];
        planet.speed = Random.Range(0.1f, 1f);
        float randomScale = Random.Range(1f, 5f);
        planet.transform.localScale = new Vector3(randomScale, randomScale, 10);
        planet.transform.position = new Vector2(screenBounds.x * spawnDistance, Random.Range(-screenBounds.y, screenBounds.y));
    }

    IEnumerator planetWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnPlanet();
        }
    }

}
