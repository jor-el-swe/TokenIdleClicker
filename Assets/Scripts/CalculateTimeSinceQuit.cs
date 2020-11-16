using System;
using UnityEngine;

public class CalculateTimeSinceQuit : MonoBehaviour {
    void Awake () {
        var savedDateAndTime = PlayerPrefs.GetString ("OldTimeAndDate");
        if (savedDateAndTime == "") //Don't use null here!
            return;
        var currentDateAndTime = DateTime.Now;
        var oldDateAndTime = ConvertStringToDateTime (savedDateAndTime);
        ProgressSinceQuit.ElapsedTime = Mathf.Max (0f, (float) (currentDateAndTime - oldDateAndTime).TotalSeconds);
    }
    void FixedUpdate () {
        PlayerPrefs.SetString ("OldTimeAndDate", DateTime.Now.ToString ());
        ProgressSinceQuit.ElapsedTime = 0;
    }
    DateTime ConvertStringToDateTime (string dateTimeString) {
        var dateTime = DateTime.Parse (dateTimeString);
        return dateTime;
    }
}