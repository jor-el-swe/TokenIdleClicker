﻿using System;
using UnityEngine;

public class CalculateTimeSinceQuit : MonoBehaviour {

    [SerializeField] private TimeSinceQuit timeSinceQuit; //Might change later to public float property and private float
    void Awake () {
        var savedDateAndTime = PlayerPrefs.GetString ("OldTimeAndDate");
        if (savedDateAndTime == "") //Don't use null here!
            return;
        var currentDateAndTime = DateTime.Now;
        var oldDateAndTime = ConvertStringToDateTime (savedDateAndTime);
        timeSinceQuit.ElapsedTime = Mathf.Max (0f, (float) (currentDateAndTime - oldDateAndTime).TotalSeconds);
    }
    void OnApplicationQuit () {
        PlayerPrefs.SetString ("OldTimeAndDate", DateTime.Now.ToString ());
        timeSinceQuit.ElapsedTime = 0;
    }
    DateTime ConvertStringToDateTime (string dateTimeString) {
        var dateTime = DateTime.Parse (dateTimeString);
        return dateTime;
    }
}