﻿using UnityEngine;
using UnityEngine.UI;

namespace ResourceProduction {
    public class Producer : MonoBehaviour {
        public PopupText popupTextPrefab;
        public static event System.Action onUpdateTextEvent;
        private float timer;
        private bool isProducing;
        
        private AudioHandler audiohandler;

        [SerializeField] private Data data;
        [SerializeField] private GameObject produceButton;
        [SerializeField] private ProgressBar progressBar;
        [SerializeField] private Transform popupTextSpawnPoint;
        [SerializeField] private Castle.Data castleData;
        public Data Data => data;
        private string OwnedKey => $"{data.name}_owned";
        public int NumberOwned {
            get => PlayerPrefs.GetInt(OwnedKey, 0);
            private set => PlayerPrefs.SetInt(OwnedKey, value);
        }
        public void Buy() {
            (ulong cost, int buyAmount) = data.GetActualBulkPrice(NumberOwned);
            if (data.Resource.CurrentAmount < cost)
            { 
                audiohandler.Play("nono");
                return;
            }
            audiohandler.Play("click");
            data.Resource.CurrentAmount -= cost;
            NumberOwned += buyAmount;
            UpdateTextEvent();
        }
        public void StartProduction() {
            isProducing = true;
        }
        private void Start() {
            //get audioHandler
            audiohandler = FindObjectOfType<AudioHandler>();
            ProduceAtStart();
        }
        private static void UpdateTextEvent() {
            if (onUpdateTextEvent != null)
                onUpdateTextEvent();
        }
        private void Update() {
            // Disables produceButton when AutoClicker is active and vice versa + checks if you own at least 1 producer
            //produceButton.SetActive(!data.AutoClickerActive && NumberOwned > 0);

            if (data.AutoClickerActive || isProducing) {
                Produce();
            }
        }
        private void ProduceAtStart() {
            if (!data.AutoClickerActive || ChangeSinceQuit.Data.ElapsedTime < data.GetActualProductionTime(data.Level))
                return;
            var produce = data.GetActualProductionAmount(data.Level) * (ulong) NumberOwned *
                (ulong) (1 + castleData.AscensionBonus * PlayerHandler.PlayerLevel) *
                (ulong) Mathf.RoundToInt(ChangeSinceQuit.Data.ElapsedTime / data.GetActualProductionTime(NumberOwned));

            ChangeSinceQuit.Data.ProducedAmount += produce;
            data.Resource.CurrentAmount += produce;
        }
        private void Produce() {
            timer += Time.deltaTime;
            var productionTime = data.GetActualProductionTime(NumberOwned);
            progressBar.FollowProductionTime(productionTime, timer);
            if (timer < productionTime)
                return;
            InstantiatePopupText();
            progressBar.ResetProgressbar();
            data.Resource.CurrentAmount += data.GetActualProductionAmount(data.Level) * (ulong) NumberOwned *
                (ulong) (1 + castleData.AscensionBonus * PlayerHandler.PlayerLevel);
            timer -= productionTime;
            isProducing = false;
        }

        private void InstantiatePopupText() {
            var instance = Instantiate(popupTextPrefab, this.popupTextSpawnPoint.transform);
            instance.GetComponent<Text>().text = $@"+{data.GetActualProductionAmount(data.Level) * (ulong) NumberOwned * 
                                                     (ulong)(1 + castleData.AscensionBonus * PlayerHandler.PlayerLevel)}";
            instance.GetComponent<Text>().color = data.Resource.resourceColor;
        }
    }
}