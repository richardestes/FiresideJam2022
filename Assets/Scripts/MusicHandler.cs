using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    private GameManager manager;
    private AudioSource source;
    private AudioClip mainSong, leaderboardSong;
    private bool playerDead;
    private bool songDone;
    public List<AudioClip> mainSongs;
    public List<AudioClip> leaderboardSongs;
    public float masterSongVolume = 0.5f;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        int randomMainIndex = Random.Range(0, mainSongs.Count);
        mainSong = mainSongs[randomMainIndex];
        mainSongs.RemoveAt(randomMainIndex);

        int randomLeaderboardIndex = Random.Range(0, leaderboardSongs.Count);
        leaderboardSong = leaderboardSongs[randomLeaderboardIndex];

        source = GetComponent<AudioSource>();
        source.clip = mainSong;
        source.volume = masterSongVolume;
        source.Play();
        print("Now Playing: " + source.clip.name);
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
        if (mainSong && songDone) // song has ended
        {
            SwitchSong();
        }    
    }

    void SwitchSong()
    {
        source.Stop();
        print("Switching song...");
        int randomMainIndex = Random.Range(0, mainSongs.Count);
        mainSong = mainSongs[randomMainIndex];
        mainSongs.RemoveAt(randomMainIndex);
        source.clip = mainSong;
        source.volume = masterSongVolume;
        source.Play();
        print("Now Playing: " + source.clip.name);
    }

    void LoadLeaderboardSong()
    {
        source.Stop();
        source.clip = leaderboardSong;
        source.Play();
        print("Now Playing: " + source.clip.name);
    }
}
