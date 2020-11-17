using UnityEngine;
using UnityEngine.UI;

namespace ResourceProduction {
    public class Producer : MonoBehaviour {
        private float timer;
        private bool isProducing;

        [SerializeField] private Data data;
        [SerializeField] private GameObject produceButton;
        [SerializeField] private ProgressBar progressBar;

        public Data Data => data;
        private string OwnedKey => $"{data.name}_owned";
        public int NumberOwned {
            get => PlayerPrefs.GetInt(OwnedKey, 0);
            private set => PlayerPrefs.SetInt(OwnedKey, value);
        }
        public void Buy() {
            if (data.Resource.CurrentAmount < data.GetActualPrice(NumberOwned))
                return;
            data.Resource.CurrentAmount -= data.GetActualPrice(NumberOwned);
            NumberOwned++;
        }
        public void StartProduction() {
            isProducing = true;
        }
        private void Start() {
            ProduceAtStart();
        }
        private void Update() {
            // Disables produceButton when AutoClicker is active and vice versa + checks if you own at least 1 producer
            produceButton.SetActive(!data.AutoClickerActive && NumberOwned > 0);

            if (data.AutoClickerActive || isProducing) {
                Produce();
            }
        }
        private void ProduceAtStart() {
            if (!data.AutoClickerActive || ProgressSinceQuit.ElapsedTime < data.GetActualProductionTime(data.Level))
                return;
            var produce = data.GetActualProductionAmount(data.Level) * (ulong) NumberOwned *
                (ulong) Mathf.RoundToInt(ProgressSinceQuit.ElapsedTime / data.GetActualProductionTime(NumberOwned));

            ProgressSinceQuit.ProducedAmount += produce;
            data.Resource.CurrentAmount += produce;
        }
        private void Produce() {
            timer += Time.deltaTime;
            progressBar.FollowProductionTime(data.GetActualProductionTime(NumberOwned), timer);
            if (timer < data.GetActualProductionTime(NumberOwned)) return;
            progressBar.ResetProgressbar();
            data.Resource.CurrentAmount += data.GetActualProductionAmount(data.Level) * (ulong) NumberOwned;
            timer -= data.GetActualProductionTime(NumberOwned);
            isProducing = false;
        }
    }
}