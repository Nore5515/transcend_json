using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    GameObject settingsPanel;

    [SerializeField]
    Toggle musicMute;

    [SerializeField]
    Toggle effectsMute;

    public void Awake()
    {
        musicMute.isOn = GameState.musicMuted;
        //try
        //{
        //    musicMute.isOn = GameState.musicMuted;
        //    effectsMute.isOn = GameState.effectsMuted;
        //}
        //catch (Exception e)
        //{
        //    Debug.Log(e.Data.ToString());
        //}
    }

    public void Resume()
    {
        settingsPanel.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void MuteEffects()
    {
        GameState.effectsMuted = !GameState.effectsMuted;
        effectsMute.isOn = GameState.effectsMuted;
    }

    public void MuteMusic()
    {
        try
        {
            if (musicMute.isOn)
            {
                GameState.musicMuted = false;
            }
            else
            {
                GameState.musicMuted = true;
            }


            GameObject audio = GameObject.FindGameObjectWithTag("music");
            if (audio != null)
            {
                audio.GetComponent<AudioSource>().mute = musicMute.isOn;
            }
            Debug.Log(string.Format("Muisc mute hit, gameState:musicMuted {0}, musicMute.isOn {1}, audioSource.mute {2}", GameState.musicMuted, musicMute.isOn, audio.GetComponent<AudioSource>().mute));
        }
        catch (Exception e)
        {
            Debug.Log(e.Data.ToString());
        }
    }

    public void OnEffectSliderChange()
    {

    }

    public void OnMusicSliderChange()
    {

    }
}
