using System;
using System.Collections;
using UnityEngine;

public class CalculateTimeSinceQuit : MonoBehaviour {
    private void Awake () {

        var savedDateAndTime = PlayerPrefs.GetString ("OldTimeAndDate");
        StartCoroutine (OnUpdateDateTime ());
        if (savedDateAndTime == "") //Don't use null here!
            return;
        var currentDateAndTime = DateTime.Now;
        var oldDateAndTime = ConvertStringToDateTime (savedDateAndTime);
        ProgressSinceQuit.ElapsedTime = Mathf.Max (0f, (float) (currentDateAndTime - oldDateAndTime).TotalSeconds);
    }
    private void OnApplicationQuit () {
        PlayerPrefs.SetString ("OldTimeAndDate", DateTime.Now.ToString ());
    }
    private IEnumerator OnUpdateDateTime () {
        while (true) {
            PlayerPrefs.SetString ("OldTimeAndDate", DateTime.Now.ToString ());
            yield return new WaitForSeconds (60);
        }
    }
    private DateTime ConvertStringToDateTime (string dateTimeString) {
        var dateTime = DateTime.Parse (dateTimeString);
        return dateTime;
    }
}