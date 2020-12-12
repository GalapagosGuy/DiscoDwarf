using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance = null;
    public PlayerMovement movement;

    [SerializeField]
    private float beatTempo = 120.0f;
    [SerializeField]
    private float beforeBeatAcceptableInput = 0.05f;
    [SerializeField]
    private float afterBeatAcceptableInput = 0.05f;

    private AudioSource musicAudioSource = null;
    private float timeBetweenBeats = 0.0f;
    private bool canDoAction = false;

    public bool CanDoAction { get => canDoAction; }

    private void Awake()
    {
        if (MusicManager.Instance == null)
            MusicManager.Instance = this;
        else
            Destroy(this);

        musicAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        timeBetweenBeats = 60.0f / beatTempo;

        StartPlayingMusic();
    }

    public void StartPlayingMusic()
    {
        if (!musicAudioSource.isPlaying)
            InitializeMusic();
    }

    private float previousFrameTime = 0.0f;
    private float lastReportedPlayheadPosition = 0.0f;

    private float songTime = 0.0f;
    private float lastReportedSongTime = 0.0f;
    private float nextBeatCheckpoint = 0.0f;
    private float previousBeatCheckpoint = 0.0f;

    private void InitializeMusic()
    {
        previousFrameTime = Time.time;
        lastReportedPlayheadPosition = 0.0f;
        nextBeatCheckpoint = timeBetweenBeats;
        musicAudioSource.Play();
    }

    private void Update()
    {
        songTime += Time.time - previousFrameTime;
        previousFrameTime = Time.time;

        if (musicAudioSource.time != lastReportedPlayheadPosition)
        {
            songTime = (songTime + musicAudioSource.time) / 2.0f;
            lastReportedPlayheadPosition = musicAudioSource.time;
        }

        if ((songTime >= nextBeatCheckpoint - beforeBeatAcceptableInput) || (songTime <= previousBeatCheckpoint + afterBeatAcceptableInput))
            canDoAction = true;
        else
            canDoAction = false;

        if (songTime >= nextBeatCheckpoint)
        {
            Debug.Log("pop");
            previousBeatCheckpoint = nextBeatCheckpoint;
            nextBeatCheckpoint += timeBetweenBeats;
        }
    }

    /*private float currentTimeToBeat = 0.0f;
    private float lastAudioFrame = 0.0f;
    private float deltaAudioFrame = 0.0f;

    private void Update()
    {
        deltaAudioFrame = musicAudioSource.time - lastAudioFrame;
        lastAudioFrame = musicAudioSource.time;

        currentTimeToBeat += deltaAudioFrame;

        if (currentTimeToBeat >= timeBetweenBeats)
        {
            Debug.Log("pop");

            currentTimeToBeat -= timeBetweenBeats;
        }
    }*/
}
