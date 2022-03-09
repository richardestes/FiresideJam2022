using UnityEngine;
using UnityEngine.UI;

public class GameManager: MonoBehaviour
{
    // Singleton
    public static GameManager instance;

    public Text ammoText;
    public Text spaceshipHealthText;
    public Text scoreText;
    public float score = 0;
    public bool isDead;
    public string finalScore;

    [SerializeField]
    private Spaceship spaceship;
    [SerializeField]
    private Crosshair crosshair;


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
        if (spaceship.dead)
        {
            isDead = true;
            finalScore = score.ToString("0");
            LevelManager levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
            levelManager.FadeToLevel(2);
        }
        SetupUI();
        IncreaseScoreByTime();
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

    public void PrintFinalScore()
    {
        print("Final Score: " + finalScore);
    }

    public void ResetGameStats()
    {
        spaceship.health = 100;
        crosshair.ammo = 100;
        score = 0;
        isDead = false;
    }
    
}
