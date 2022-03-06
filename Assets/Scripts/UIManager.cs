using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int score = 0;

    [SerializeField]
    private Spaceship spaceship;
    [SerializeField]
    private Crosshair crosshair;

    public Text ammoText;
    public Text spaceshipHealthText;
    public Text scoreText;


    private void Start()
    {
        if (!spaceship) spaceship = GameObject.FindGameObjectWithTag("Spaceship").GetComponent<Spaceship>();
        if (!crosshair) crosshair = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Crosshair>();
    }

    private void Update()
    {
        spaceshipHealthText.text = spaceship.health.ToString();
        ammoText.text = crosshair.ammo.ToString();
        scoreText.text = score.ToString();
    }

    public void IncreaseScore(int points)
    {
        score += points;
    }
}
