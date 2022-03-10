using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private GameManager manager;
    private int levelToLoad;

    public Text scoreText;
    public Animator animator;

    private void Start()
    {
        if (!manager) manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (!scoreText) scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        if (scoreText) scoreText.text = manager.finalScore; // should only fire on Leaderboard Scene
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        Cursor.visible = true;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void RestartGame()
    {
        manager.ResetGameStats();
        SceneManager.LoadScene(0);
    }
}
