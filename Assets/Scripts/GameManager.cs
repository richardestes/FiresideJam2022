using UnityEngine;
using UnityEngine.UI;

public class GameManager: MonoBehaviour
{
    // Singleton
    public static GameManager instance;

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
        instance = this;
        if (!spaceship) spaceship = GameObject.FindGameObjectWithTag("Spaceship").GetComponent<Spaceship>();
        if (!crosshair) crosshair = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Crosshair>();
        if (!scoreText) scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>(); // for leaderboard scene
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (isDead) return;
        SetupUI();
        IncreaseScoreByTime();
        if (spaceship.dead) isDead = true;
    }
    void SetupUI()
    {
        spaceshipHealthText.text = spaceship.health.ToString();
        ammoText.text = crosshair.ammo.ToString();
        int scoreInt = Mathf.RoundToInt(score);
        scoreText.text = scoreInt.ToString();
    }

    void IncreaseScoreByTime()
    {
        float pointIncrease = 1f;
        score += pointIncrease * Time.deltaTime;
    }

    public void IncreaseScore(float points)
    {
        score += points;
    }

    
}
