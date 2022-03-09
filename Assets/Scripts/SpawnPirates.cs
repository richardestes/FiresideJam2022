using System.Collections;
using UnityEngine;

public class SpawnPirates : MonoBehaviour
{

    [SerializeField]
    private GameObject piratePrefab;
    private GameManager manager;
    private Vector2 screenBounds;

    [Range(5f, 180f)]
    public float respawnTime = 180f;
    [Range(1f, 10f)]
    public float spawnDistance = 2f;
    public float scoreThreshold = 100f;

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if (!manager) manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        StartCoroutine(PirateWave());
    }

    void SpawnPirate()
    {
        print("Spawning pirate");
        GameObject pirateObj = Instantiate(piratePrefab);
        Pirate pirate = pirateObj.GetComponent<Pirate>();
        pirate.speed = Random.Range(0.1f, 1f);
        pirate.transform.position = new Vector2(screenBounds.x * spawnDistance, Random.Range(-screenBounds.y, screenBounds.y));
    }

    IEnumerator PirateWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            respawnTime = Mathf.Clamp(respawnTime - 5f, 5, 360); // decrease respawn time
            if (manager.score > scoreThreshold) SpawnPirate();
        }
    }
}
