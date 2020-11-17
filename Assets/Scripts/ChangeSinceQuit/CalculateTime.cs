using System;
using System.Collections;
using UnityEngine;

namespace ChangeSinceQuit {
    public class CalculateTime : MonoBehaviour {
        private void Awake() {

            var savedDateAndTime = PlayerPrefs.GetString("OldTimeAndDate");
            StartCoroutine(OnUpdateDateTime());
            if (savedDateAndTime == "") //Don't use null here!
                return;
            var currentDateAndTime = DateTime.Now;
            var oldDateAndTime = ConvertStringToDateTime(savedDateAndTime);
            Data.ElapsedTime = Mathf.Max(0f, (float) (currentDateAndTime - oldDateAndTime).TotalSeconds);
        }
        private void OnDestroy() {
            PlayerPrefs.SetString("OldTimeAndDate", DateTime.Now.ToString());
            Data.ElapsedTime = 0;
            Data.ProducedAmount = 0;
        }
        private IEnumerator OnUpdateDateTime() {
            while (true) {
                PlayerPrefs.SetString("OldTimeAndDate", DateTime.Now.ToString());
                yield return new WaitForSeconds(10);
            }
        }
        private DateTime ConvertStringToDateTime(string dateTimeString) {
            var dateTime = DateTime.Parse(dateTimeString);
            return dateTime;
        }
    }
}