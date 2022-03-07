using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBatteries : MonoBehaviour
{
    private Vector2 screenBounds;

    [SerializeField]
    private GameObject batteryPrefab;

    private UIManager manager;

    private int score;

    [Range(1, 10)]
    public float respawnTime = 1;
    [Range(1, 4)]
    public float spawnDistance = 2f;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if (!manager) manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>();
        StartCoroutine(BatteryWave());
    }

    private void Update()
    {
        score = manager.score;
    }

    void SpawnBattery()
    {
        print("Spawning battery");
        GameObject batteryObj = Instantiate(batteryPrefab);
        Battery battery = batteryObj.GetComponent<Battery>();
        battery.speed = Random.Range(0.1f, 1f);
        battery.transform.position = new Vector2(screenBounds.x * spawnDistance, Random.Range(-screenBounds.y, screenBounds.y));
    }

    IEnumerator BatteryWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            if (score > 200) SpawnBattery();
        }
    }

}
