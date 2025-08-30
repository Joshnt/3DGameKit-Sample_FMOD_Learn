using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance; // global reference
    public EventReference MenuMusicEvent;
    EventInstance MenuMusicInstance;
    public EventReference LevelMusicEvent;
    EventInstance LevelMusicInstance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;         // set the instance
            DontDestroyOnLoad(gameObject); // optional: persist between scenes
        }
        else
        {
            Destroy(gameObject);     // prevent duplicates
        }
    }

    void PlayMenuMusic()
    {
        if (!MenuMusicInstance.isValid())
        {
            MenuMusicInstance = RuntimeManager.CreateInstance(MenuMusicEvent);
            MenuMusicInstance.start();
        }


        if (LevelMusicInstance.isValid())
        {
            PLAYBACK_STATE playbackState;
            LevelMusicInstance.getPlaybackState(out playbackState);

            if (playbackState != PLAYBACK_STATE.STOPPED)
            {
                LevelMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                LevelMusicInstance.release();
            }
        }
    }

    void PlayLevelMusic()
    {
        if (!LevelMusicInstance.isValid())
        {
            LevelMusicInstance = RuntimeManager.CreateInstance(LevelMusicEvent);
            LevelMusicInstance.start();
        }


        if (MenuMusicInstance.isValid())
        {
            PLAYBACK_STATE playbackState;
            MenuMusicInstance.getPlaybackState(out playbackState);

            if (playbackState != PLAYBACK_STATE.STOPPED)
            {
                MenuMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                MenuMusicInstance.release();
            }
        }
    }

    // Called automatically when a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Start")
        {
            PlayMenuMusic();
        }
        else
        {
            PlayLevelMusic();
        }
    }
}
