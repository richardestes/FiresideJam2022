using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Singleton
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    public Text ammoText;
    public Text spaceshipHealthText;
    public Text scoreText;
    public float score = 0;
    [Range(1, 5)]
    public int maxEnemies = 3;
    public bool isDead;
    public bool reset = false;
    public bool isMusicMuted;
    public string finalScore;
    public Image[] healthPoints;

    [SerializeField]
    private Spaceship spaceship;
    [SerializeField]
    private Crosshair crosshair;


    private void Start()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0) // Main scene
        {
            if (!spaceship) spaceship = GameObject.FindGameObjectWithTag("Spaceship").GetComponent<Spaceship>();
            if (!crosshair) crosshair = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Crosshair>();
            if (healthPoints.Length < 1) LoadHealthBarImages();
        }
        else // leaderboard scene
        {
            if (!scoreText) scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>(); // for leaderboard scene
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Update()
    {
        if (isDead) return;
        if (spaceship.dead)
        {
            isDead = true;
            finalScore = score.ToString("0");
            score = 0f;
            LevelManager levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
            levelManager.FadeToLevel(1);
        }
        if (!isDead && spaceship)
        {
            UpdateUI();
            UpdateHealthBar();
            IncreaseScoreByTime();
        }
    }

    // Added if statement checks for restarting level. Don't try to update
    // if they don't exist yet :)
    void UpdateUI()
    {
        if (ammoText) ammoText.text = crosshair.ammo.ToString();
        int scoreInt = Mathf.RoundToInt(score);
        if (scoreText) scoreText.text = scoreInt.ToString();
    }

    void LoadHealthBarImages()
    {
        GameObject[] healthObjects = GameObject.FindGameObjectsWithTag("HealthPoints");
        for (int i = 0; i < healthObjects.Length; i++)
        {
            Image healthBar = healthObjects[i].GetComponent<Image>();
            healthPoints[i] = healthBar;
        }
    }

    void IncreaseScoreByTime()
    {
        float pointIncrease = 1f;
        score += pointIncrease * Time.deltaTime;
    }

    public void IncreaseScore(float points)
    {
        print("Old score: " + score);
        score += points;
        print("New score: " + score);
    }

    public void PrintFinalScore()
    {
        print("Final Score: " + finalScore);
    }

    public void ResetGameStats()
    {
        spaceship.dead = false;
        isDead = false;
        spaceship.health = 100;
        crosshair.ammo = 100;
        score = 0;
        reset = true;
    }

    void UpdateHealthBar()
    {
        // Check to see if spaceship has been instantiated yet.
        // If we don't check, we get a MissingReferenceException
        // from UpdateUI() on every new run.
        if (!spaceship) return;

        for (int i = 0; i < healthPoints.Length; i++)
        {
            healthPoints[i].enabled = !DisplayHealthPoint(spaceship.health, i);
        }
    }

    bool DisplayHealthPoint(float health, int pointNumber)
    {
        return ((pointNumber * 10) >= health);
    }

}
