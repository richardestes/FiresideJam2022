using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;

public class LeaderboardController : MonoBehaviour
{
    // TODO: Change this to just memberID and submit score with actual score
    // from game manager
    public InputField memberID, playerScore;
    public int leaderboardID;
    public int actualScore;
    private GameManager manager;

    public int maxScores = 5;
    public Text[] onlineScores;

    private void Start()
    {
        if (!manager) manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.statusCode == 200)
            {

                Debug.Log("Successfully started guest session");
                ShowScores();
            }
            else
            {
                Debug.Log("Lootlocker error");
                print(response.Error);
            }
        });
        actualScore = int.Parse(manager.finalScore);
    }

    public void ShowScores()
    {
        LootLockerSDKManager.GetScoreList(leaderboardID, maxScores, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Successfully retrieved scores from leaderboard");
                LootLockerLeaderboardMember[] scores = response.items;
                for (int i = 0; i < scores.Length; i++)
                {
                    onlineScores[i].text = (scores[i].rank + ": " + scores[i].member_id + " - " + scores[i].score);
                }

                if (scores.Length < maxScores)
                {
                    for (int i = scores.Length; i < maxScores; i++)
                    {
                        onlineScores[i].text = (i + 1).ToString() + ". none";
                    }
                }

            }
            else
            {
                Debug.Log("Lootlocker error");
                print(response.Error);
            }
        });
    }

    public void SubmitScore()
    {
        LootLockerSDKManager.SubmitScore(memberID.text, actualScore, leaderboardID, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Successfully submitted score of: " + actualScore + " to leaderboard");
                ShowScores();
            }
            else
            {
                Debug.Log("Lootlocker error");
            }
        });
    }
}
