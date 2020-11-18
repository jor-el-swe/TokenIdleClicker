using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class AudioHandler: MonoBehaviour
{
    public float fadeTime = 2f;
    
    public Sound[] sounds;
    
    public AudioMixerGroup music;
    public AudioMixerGroup sfx;
    
    public Text muteSoundsText;
    public Text muteMusicText;

    private static bool musicMuted;

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
    public void Play(string soundName)
    {
        var s = Array.Find(sounds, sound => sound.soundName == soundName);
        s?.audioSource.Play();
    }
    
    public void Pause(string soundName)
    {
        var s = Array.Find(sounds, sound => sound.soundName == soundName);
        s?.audioSource.Pause();
    }
    
    public void UnPause(string soundName)
    {
        var s = Array.Find(sounds, sound => sound.soundName == soundName);
        s?.audioSource.UnPause();
    }
    
    public void SetPitch(string soundName, float pitch)
    {
        var s = Array.Find(sounds, sound => sound.soundName == soundName);
        if (s == null) return;
        s.audioSource.pitch = pitch;
    }
    
    
    public void FadeOutMusic(string soundName)
    {
        var s = Array.Find(sounds, sound => sound.soundName == soundName);
        StartCoroutine(StartFade(s.audioSource, fadeTime, 0f));
    }

    public void FadeInMusic(string soundName)
    {
        var s = Array.Find(sounds, sound => sound.soundName == soundName);
        StartCoroutine(StartFade(s.audioSource, fadeTime, s.volume));
    }

    private static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
    private void Awake()
    {
        foreach (var sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.loop = sound.loop;
        }
    }
    
    private void Start()
    {
        Debug.Log("playing first song");
        Play("Level1SongDemo");
        
        
        muteSoundsText.text = "Mute All Sounds";
        muteMusicText.text = "Mute Music";
        AudioListener.pause = false;
    }
}
