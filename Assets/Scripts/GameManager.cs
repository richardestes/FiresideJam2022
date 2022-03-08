using UnityEngine;
using UnityEngine.UI;

public class GameManager: MonoBehaviour
{
    public float score = 0;

    [SerializeField]
    private Spaceship spaceship;
    [SerializeField]
    private Crosshair crosshair;

    public Text ammoText;
    public Text spaceshipHealthText;
    public Text scoreText;

    public bool isDead;

    private void Start()
    {
        if (!spaceship) spaceship = GameObject.FindGameObjectWithTag("Spaceship").GetComponent<Spaceship>();
        if (!crosshair) crosshair = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Crosshair>();
    }

    private void Update()
    {
        if (isDead) return;
        spaceshipHealthText.text = spaceship.health.ToString();
        ammoText.text = crosshair.ammo.ToString();
        int scoreInt = Mathf.RoundToInt(score);
        scoreText.text = scoreInt.ToString();
        float pointIncrease = 1f;
        score += pointIncrease * Time.deltaTime;
        if (spaceship.dead) isDead = true;
    }

    public void IncreaseScore(float points)
    {
        score += points;
    }
}
