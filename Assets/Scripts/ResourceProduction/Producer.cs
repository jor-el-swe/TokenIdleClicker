using UnityEngine;
using UnityEngine.UI;

namespace ResourceProduction {
    public class Producer : MonoBehaviour {
        private float timer;
        private bool isProducing;

        [SerializeField] private Data data;
        [SerializeField] private GameObject produceButton;
        [SerializeField] private Text buyText;
        [SerializeField] private Text upgradeText;
        [SerializeField] private Text numberOwnedText;
        [SerializeField] private Text levelText;
        [SerializeField] private Text productionTimeText;
        [SerializeField] private ProgressBar progressBar;

        private string OwnedKey => $"{data.name}_owned";
        private string LevelKey => $"{data.name}_level";

        private int NumberOwned {
            get => PlayerPrefs.GetInt(OwnedKey, 0);
            set => PlayerPrefs.SetInt(OwnedKey, value);
        }

        public void Buy() {
            if (data.Resource.CurrentAmount < data.GetActualPrice(NumberOwned))
                return;
            data.Resource.CurrentAmount -= data.GetActualPrice(NumberOwned);
            NumberOwned++;
            UpdateBuyText();
            UpdateOwnedText();
        }

        public void StartProduction() {
            isProducing = true;
        }

        private void Start() {
            UpdateBuyText();
            UpdateOwnedText();
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

        private void UpdateOwnedText() {
            numberOwnedText.text = NumberOwned.ToString();
            productionTimeText.text = data.GetActualProductionTime(NumberOwned).ToString("Production Time: 0.00");
        }

        private void UpdateBuyText() {
            buyText.text = $"Buy\n {SuffixHelper.GetString(data.GetActualPrice(NumberOwned))} Tokens ";
            
        }
    }
}