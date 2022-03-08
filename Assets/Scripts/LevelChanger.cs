using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;

    private int levelToLoad;

    private void Update()
    {
        // TODO: Change this to change when player has died
        if(Input.GetKeyDown(KeyCode.N))
        {
            FadeToLevel(1);
        }
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
