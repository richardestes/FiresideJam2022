using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlanets : MonoBehaviour
{
    private Vector2 screenBounds;

    [SerializeField]
    private GameObject planetPrefab;

    [Range(1,10)]
    public float respawnTime = 1;
    [Range(1,4)]
    public float spawnDistance = 2f;
    public List<Sprite> planetSprites;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(PlanetWave());
    }

    void SpawnPlanet()
    {
        GameObject planetObj = Instantiate(planetPrefab);
        Planet planet = planetObj.GetComponent<Planet>();
        planetObj.GetComponent<SpriteRenderer>().sprite = planetSprites[Random.Range(0, planetSprites.Count)];
        planet.speed = Random.Range(0.1f, 1f);
        float randomScale = Random.Range(1f, 5f);
        planet.transform.localScale = new Vector3(randomScale, randomScale, 10);
        planet.transform.position = new Vector3(screenBounds.x * spawnDistance, Random.Range(-screenBounds.y, screenBounds.y), 5);
    }

    IEnumerator PlanetWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnPlanet();
        }
    }

}
