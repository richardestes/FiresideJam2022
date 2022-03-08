using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MusicHandler : MonoBehaviour
{
    private GameManager manager;
    private AudioSource source;
    private AudioClip mainSong, leaderboardSong;
    private bool playerDead;
    private bool songDone;
    private Dictionary<string, string> titleArtistPairs = new Dictionary<string, string>();

    public TMP_Text songTitle;
    public TMP_Text songArtist;

    public List<AudioClip> mainSongs;
    public List<AudioClip> bossSongs;
    public List<AudioClip> leaderboardSongs;

    public float masterSongVolume = 0.5f;

    private void Start()
    {
        if (!manager) manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        SetupTitleArtistPairs();
        SetupGameSongs();
        source.Play();
        print("Now Playing: " + source.clip.name + " by: " + songArtist.text);

    }

    private void Update()
    {
        songDone = !source.isPlaying;
        if (playerDead) return;
        if (manager.isDead)
        {
            playerDead = true;
            LoadLeaderboardSong();
        }
        if (Input.GetKeyDown(KeyCode.Space)) SwitchSong();
        if (mainSong && songDone) // song has ended
        {
            SwitchSong();
        }
    }

    void SetupGameSongs()
    {
        int randomMainIndex = Random.Range(0, mainSongs.Count);
        mainSong = mainSongs[randomMainIndex];
        //mainSongs.RemoveAt(randomMainIndex);

        int randomLeaderboardIndex = Random.Range(0, leaderboardSongs.Count);
        leaderboardSong = leaderboardSongs[randomLeaderboardIndex];

        source = GetComponent<AudioSource>();
        source.clip = mainSong;
        source.volume = masterSongVolume;
        SetUIText();
    }

    void SetUIText()
    {
        songTitle.text = mainSong.name;
        string songArtistString = GetSongArtistString(songTitle.text);
        songArtist.text = songArtistString;
    }

    void SwitchSong()
    {
        source.Stop();
        print("Switching song...");
        int randomMainIndex = Random.Range(0, mainSongs.Count);
        mainSong = mainSongs[randomMainIndex];
        //mainSongs.RemoveAt(randomMainIndex);
        SetUIText();
        source.clip = mainSong;
        source.volume = masterSongVolume;
        source.Play();
        print("Now Playing: " + source.clip.name + " by: " + songArtist.text);
    }

    void LoadLeaderboardSong()
    {
        source.Stop();
        source.clip = leaderboardSong;
        source.Play();
        print("Now Playing: " + source.clip.name);
    }

    void SetupTitleArtistPairs()
    {
        List<string> creammamireSongs = new List<string>
            {
                "Alien Crime Lord", "ALieNNatioN", "At the Door",
                "Brooklyn Bridge to Chorus", "Chances", "Did My Best",
                "Drag Queen", "Games", "Happy Ending", "Hawaii", "Human Sadness",
                "Instant Crush", "Ize of the World", "Juicebox", "Leave It In My Dreams",
                "Machu Picchu", "Metabolism", "Not The Same Anymore", "One Way Trigger",
                "One Way Trigger (Mellow)", "River of Brakelights", "Selfless",
                "The End Has No End", "Taken For A Fool", "The Adults Are Talking", "The Eternal Tao",
                "The Modern Age", "Tourist", "Why Are Sundays So Depressing",
                "You Only Live Once"
            };
        foreach (string song in creammamireSongs)
            titleArtistPairs.Add(song, "creammamire");
        titleArtistPairs.Add("Where No Eagles Fly", "Better Kinetic");
        titleArtistPairs.Add("11th Dimension", "Dreamers On The Run");
    }

    string GetSongArtistString(string songTitle)
    {
        titleArtistPairs.TryGetValue(songTitle, out string artistString);
        return artistString;
    }
}
