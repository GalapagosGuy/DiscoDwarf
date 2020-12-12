using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance = null;
    public PlayerMovement movement;
    public GameObject beatHitMarker = null;

    public GameObject[] musicListenersGO = null;

    [SerializeField]
    private float beatTempo = 120.0f;
    [SerializeField]
    private float beforeBeatAcceptableInput = 0.05f;
    [SerializeField]
    private float afterBeatAcceptableInput = 0.05f;

    private AudioSource musicAudioSource = null;
    private float timeBetweenBeats = 0.0f;
    private bool canDoAction = false;
    private IMusicListener[] musicListeners = null;

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

        List<IMusicListener> listOfListeners = new List<IMusicListener>();

        foreach (GameObject go in musicListenersGO)
        {
            if (go.GetComponent<IMusicListener>() != null)
            {
                listOfListeners.Add(go.GetComponent<IMusicListener>());
            }
        }

        musicListeners = listOfListeners.ToArray();
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

    private bool beatStarted = false;
    private bool beatFinished = false;

    private void LateUpdate()
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


        CheckBeatStart();
        CheckBeatFinish();
        CheckBeatCore();
    }

    //check beat start
    private void CheckBeatStart()
    {
        if (songTime >= nextBeatCheckpoint - beforeBeatAcceptableInput && songTime <= nextBeatCheckpoint)
        {
            if (!beatStarted)
                foreach (IMusicListener musicListener in musicListeners)
                    musicListener.OnBeatStart();

            beatStarted = true;
        }
        else
            beatStarted = false;
    }

    //check beat finish
    private void CheckBeatFinish()
    {
        if (songTime <= previousBeatCheckpoint + afterBeatAcceptableInput && songTime >= previousBeatCheckpoint)
        {
            beatFinished = true;
        }
        else
        {
            if (beatFinished)
                foreach (IMusicListener musicListener in musicListeners)
                    musicListener.OnBeatFinished();

            beatFinished = false;
        }
    }

    private void CheckBeatCore()
    {
        if (songTime >= nextBeatCheckpoint)
        {
            Debug.Log("Beat!");
            previousBeatCheckpoint = nextBeatCheckpoint;
            nextBeatCheckpoint += timeBetweenBeats;

            foreach (IMusicListener musicListener in musicListeners)
                musicListener.OnBeatCenter();
        }
    }
}
