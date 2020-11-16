using System;
using UnityEngine;

public class CalculateTimeSinceQuit : MonoBehaviour {
    private void Awake () {
        var savedDateAndTime = PlayerPrefs.GetString ("OldTimeAndDate");
        if (savedDateAndTime == "") //Don't use null here!
            return;
        var currentDateAndTime = DateTime.Now;
        var oldDateAndTime = ConvertStringToDateTime (savedDateAndTime);
        ProgressSinceQuit.ElapsedTime = Mathf.Max (0f, (float) (currentDateAndTime - oldDateAndTime).TotalSeconds);
    }
    private void FixedUpdate () {
        PlayerPrefs.SetString ("OldTimeAndDate", DateTime.Now.ToString ());
    }
    private DateTime ConvertStringToDateTime (string dateTimeString) {
        var dateTime = DateTime.Parse (dateTimeString);
        return dateTime;
    }
}