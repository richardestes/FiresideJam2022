using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton
    static GameManager _instance;

    public Text ammoText;
    public Text scoreText;
    public float score = 0;
    [Range(1, 5)]
    public int maxEnemies = 3;
    public bool isDead;
    public bool isMusicMuted;
    public string finalScore;
    public Image[] healthPoints;

    [SerializeField]
    private Spaceship spaceship;
    [SerializeField]
    public Crosshair crosshair;


    private void Start()
    {
        if (!GetInstance().spaceship) GetInstance().spaceship = GameObject.FindGameObjectWithTag("Spaceship").GetComponent<Spaceship>();
        if (!GetInstance().crosshair) GetInstance().crosshair = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Crosshair>();
        if (!GetInstance().scoreText) GetInstance().scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>(); // for leaderboard scene
        if (!GetInstance().ammoText) GetInstance().ammoText = GameObject.FindGameObjectWithTag("AmmoText").GetComponent<Text>();
        GetInstance().LoadHealthBarImages();
    }

    private void Update()
    {
        if (GetInstance().isDead) return;
        if (spaceship.dead)
        {
            GetInstance().isDead = true;
            GetInstance().finalScore = GetInstance().score.ToString("0");
            LevelManager levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
            levelManager.FadeToLevel(1);
        }
        GetInstance().IncreaseScoreByTime();
    }

    // Singleton Shit
    public static GameManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new GameObject("GameManger").AddComponent<GameManager>();
            DontDestroyOnLoad(_instance.gameObject);
        }
        return _instance;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            Debug.Log("Game Manager instance created.");
            _instance = this;
            DontDestroyOnLoad(gameObject);
            if (!GetInstance().scoreText) GetInstance().scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>(); // for leaderboard scene
            if (!GetInstance().ammoText) GetInstance().ammoText = GameObject.FindGameObjectWithTag("AmmoText").GetComponent<Text>();
            if (!GetInstance().spaceship) GetInstance().spaceship = GameObject.FindGameObjectWithTag("Spaceship").GetComponent<Spaceship>();
            if (!GetInstance().crosshair) GetInstance().crosshair = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Crosshair>();
            GetInstance().LoadHealthBarImages();
        }
        else if (_instance != this)
        {
            Debug.Log("Game Manager instance deleting duplicate.");
            Destroy(gameObject);
            if (!GetInstance().scoreText) GetInstance().scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>(); // for leaderboard scene
            if (!GetInstance().ammoText) GetInstance().ammoText = GameObject.FindGameObjectWithTag("AmmoText").GetComponent<Text>();
            if (!GetInstance().spaceship) GetInstance().spaceship = GameObject.FindGameObjectWithTag("Spaceship").GetComponent<Spaceship>();
            if (!GetInstance().crosshair) GetInstance().crosshair = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Crosshair>();
            GetInstance().LoadHealthBarImages();
        }
    }

    // Added if statement checks for restarting level. Don't try to update
    // if they don't exist yet :)
    public void UpdateUI()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) return; // don't run on leaderboard scene
        GetInstance().UpdateHealthBar();
        if (!GetInstance().ammoText) GetInstance().ammoText = GameObject.FindGameObjectWithTag("AmmoText").GetComponent<Text>();
        if (!GetInstance().scoreText) GetInstance().scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        if (GetInstance().ammoText) GetInstance().ammoText.text = GetInstance().crosshair.ammo.ToString();
        int scoreInt = Mathf.RoundToInt(GetInstance().score);
        if (GetInstance().scoreText) GetInstance().scoreText.text = scoreInt.ToString();
    }

    void LoadHealthBarImages()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) return;
        healthPoints = new Image[10];
        var healthBarObj = GameObject.FindGameObjectWithTag("HealthBar");
        for (int i = 0; i < healthBarObj.transform.childCount; i++)
        {
            Transform child = healthBarObj.transform.GetChild(i);
            Image healthBar = child.gameObject.GetComponent<Image>();
            GetInstance().healthPoints[i] = healthBar;

            // On builds, Unity doesn't like to load these in the correct order, 
            // so we manually set their colors
            if (i < 2)
                healthBar.color = new Color(1f, 0f, 0f, 1f); // red
            else if (i >= 2 && i < 4)
                healthBar.color = new Color(1f, 0.41f, 0f, 1f); // orange
            else if (i <= 4 && i < 6)
                healthBar.color = new Color(0.925f, 0.864f, 0f, 1f); // yellow
            else
                healthBar.color = new Color(0.004f, 0.991f, 0f, 1f); // green

            // Debug.Log("Health Bar element " + i + " color: " + healthBar.color); // DEBUG
        }
    }

    void IncreaseScoreByTime()
    {
        float pointIncrease = 1f;
        GetInstance().score += pointIncrease * Time.deltaTime;
        GetInstance().UpdateUI();
    }

    public void IncreaseScore(float points)
    {
        GetInstance().score += points;
    }

    public void PrintFinalScore()
    {
        print("Final Score: " + finalScore);
    }

    public void ResetGameStats()
    {
        print("Resetting game stats");
        GetInstance().spaceship.dead = false;
        GetInstance().isDead = false;
        GetInstance().spaceship.health = 100;
        GetInstance().crosshair.ammo = 100;
        GetInstance().finalScore = "0";
        GetInstance().score = 0f;
        GetInstance().LoadHealthBarImages();
        GetInstance().UpdateUI();
    }

    void UpdateHealthBar()
    {
        // Check to see if spaceship has been instantiated yet.
        // If we don't check, we get a MissingReferenceException
        // from UpdateUI() on every new run.
        if (!GetInstance().spaceship) return;

        for (int i = 0; i < GetInstance().healthPoints.Length; i++)
        {
            GetInstance().healthPoints[i].enabled = !GetInstance().DisplayHealthPoint(GetInstance().spaceship.health, i);
        }
    }

    bool DisplayHealthPoint(float health, int pointNumber)
    {
        return ((pointNumber * 10) >= health);
    }

}
