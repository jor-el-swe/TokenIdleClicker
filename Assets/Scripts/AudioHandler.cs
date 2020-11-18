using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class AudioHandler: MonoBehaviour
{
    public AudioMixerGroup music;
    public AudioMixerGroup sfx;
    public Text muteSoundsText;
    public Text muteMusicText;
    private static bool musicMuted;

    private void Start()
    {
        muteSoundsText.text = "Mute All Sounds";
        muteMusicText.text = "Mute Music";
        AudioListener.pause = false;
    }

    public void MuteAllSounds()
    {
        if (AudioListener.pause)
        {
            AudioListener.pause = false;
            muteSoundsText.text = "Mute All Sounds";
        }
        else
        {
            AudioListener.pause = true;
            muteSoundsText.text = "Unmute All Sounds";
        }
    }

    public void ToggleMusic()
    {
        if (!musicMuted)
        {
            music.audioMixer.SetFloat("musicVol", -80);
            muteMusicText.text = "Unmute Music";
            musicMuted = true;
        }
        else
        {
            music.audioMixer.SetFloat("musicVol", 0);
            muteMusicText.text = "Mute Music";
            musicMuted = false;
        }

    }

    public void PlayMusic()
    {
        music.audioMixer.SetFloat("musicVol", 0);
    }

    public void StopMusic()
    {
        music.audioMixer.SetFloat("musicVol", -80);
    }
}
