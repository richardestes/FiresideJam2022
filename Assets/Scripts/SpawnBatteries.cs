using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBatteries : MonoBehaviour
{
    private Vector2 screenBounds;

    [SerializeField]
    private GameObject batteryPrefab;

    private UIManager manager;

    [Range(1, 360)]
    public float respawnTime = 1;
    [Range(1f, 10f)]
    public float spawnDistance = 2f;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if (!manager) manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>();
        StartCoroutine(BatteryWave());
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
            respawnTime = Mathf.Clamp(respawnTime + 5f, 1, 360); // increase respawn time for skill curve
            SpawnBattery();

            // If you want to wait until a certain score has been obtained in respawn window,
            // this is the execution order
            // oldScore = score;
            // yield return new WaitForSeconds(respawnTime);
            // if (score > oldScore + 100) // Did player get certain score in respawn window
            // {
            //      DO SHIT
            // }

        }
    }

}
