using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    private GameManager manager;
    private AudioSource source;
    private AudioClip mainSong, leaderboardSong;
    private bool songDone;
    private bool isPlayingLeaderboardMusic;
    private bool muted;
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

        if (manager.isDead)
        {
            LoadLeaderboardSong();
            SetUIText();
        }
        else
        {
            isPlayingLeaderboardMusic = false;
            SetupMainSong();
            songTitle.color = Color.yellow;
            songArtist.color = Color.white;
            source.Play();
            print("Now Playing: " + source.clip.name + " by: " + songArtist.text);
        }
    }

    private void Update()
    {
        if (source) songDone = !source.isPlaying;
        if (Input.GetKeyDown(KeyCode.Space)) SwitchSong();
        if (Input.GetKeyDown(KeyCode.M)) MuteSong();
        if (mainSong && songDone) // song has ended
        {
            SwitchSong();
        }
    }

    void SetupMainSong()
    {
        int randomMainIndex = Random.Range(0, mainSongs.Count);
        mainSong = mainSongs[randomMainIndex];
        //mainSongs.RemoveAt(randomMainIndex);
        source = GetComponent<AudioSource>();
        source.clip = mainSong;
        source.volume = masterSongVolume;
        SetUIText();
    }

    void SetUIText()
    {
        if (!songTitle || !songArtist) return;
        if (!isPlayingLeaderboardMusic)
        {
            songTitle.text = mainSong.name;
            string songArtistString = GetSongArtistString(songTitle.text);
            songArtist.text = songArtistString;
        }
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
    }

    void MuteSong()
    {
        muted = !source.mute;
        source.mute = !source.mute;
        if (muted && !isPlayingLeaderboardMusic)
        {
            print("Muting song");
            songTitle.color = Color.gray;
            songArtist.color = Color.gray;
        }
        else
        {
            print("Unmuting song");
            songTitle.color = Color.yellow;
            songArtist.color = Color.white;
        }
    }

    void LoadLeaderboardSong()
    {
        source = GetComponent<AudioSource>();
        if (source)
        {
            source.Stop();
            int randomLeaderboardIndex = Random.Range(0, leaderboardSongs.Count);
            leaderboardSong = leaderboardSongs[randomLeaderboardIndex];
            source.clip = leaderboardSong;
            string songName = source.clip.name;
            string songArtist = GetSongArtistString(songName);
            source.Play();
            isPlayingLeaderboardMusic = true;
            print("Now Playing: " + songName + " by: " + songArtist);
        }
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
