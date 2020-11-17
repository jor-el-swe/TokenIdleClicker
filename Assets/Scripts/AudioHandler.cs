using System;
using UnityEngine.UI;
using UnityEngine;

public class AudioHandler: MonoBehaviour
{
    private static Text muteText;

    private void Start()
    {
        muteText = GetComponentInChildren<Text>();
    }

    public static void MuteAllSounds()
    {
        if (AudioListener.pause)
        {
            AudioListener.pause = false;
            muteText.text = "Mute All Sounds";
        }
        else
        {
            AudioListener.pause = true;
            muteText.text = "Unmute All Sounds";
        }
    }
}
