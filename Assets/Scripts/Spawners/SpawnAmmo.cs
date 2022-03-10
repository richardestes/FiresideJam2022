using System.Collections;
using UnityEngine;

public class SpawnAmmo : MonoBehaviour
{
    private Vector2 screenBounds;

    [SerializeField]
    private GameObject ammoPrefab;
    [SerializeField]
    private GameManager manager;

    [Range(1f, 360f)]
    public float respawnTime = 1f;
    [Range(1f, 5f)]
    public float spawnDistance = 2f;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //if (!manager) manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        StartCoroutine(AmmoWave());
    }

    void SpawnAmmoPowerup()
    {
        print("Spawning ammo");
        GameObject ammoObj = Instantiate(ammoPrefab);
        Ammo ammo = ammoObj.GetComponent<Ammo>();
        ammo.transform.position = new Vector2(screenBounds.x * spawnDistance, Random.Range(-screenBounds.y, screenBounds.y));
    }

    IEnumerator AmmoWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            respawnTime = Mathf.Clamp(respawnTime + 15f, 1, 360); // increase respawn time for skill curve
            SpawnAmmoPowerup();
        }
    }

}
