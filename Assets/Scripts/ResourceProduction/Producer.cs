using UnityEngine;
using UnityEngine.UI;

namespace ResourceProduction {
    public class Producer : MonoBehaviour {
        public PopupText popupTextPrefab;
        private float timer;
        private bool isProducing;

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
            (ulong price, int amount) = data.GetActualBulkPrice(NumberOwned);
            Debug.Log("Tokens: " + data.Resource.CurrentAmount + " cost" + price + " amount" + amount);
            if (amount < 1)
                return;
            data.Resource.CurrentAmount -= price;
            NumberOwned += amount;
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
            if (!data.AutoClickerActive || ChangeSinceQuit.Data.ElapsedTime < data.GetActualProductionTime(data.Level))
                return;
            var produce = data.GetActualProductionAmount(data.Level) * (ulong) NumberOwned * (ulong)(1 + castleData.AscensionBonus * PlayerHandler.PlayerLevel) *
                (ulong) Mathf.RoundToInt(ChangeSinceQuit.Data.ElapsedTime / data.GetActualProductionTime(NumberOwned));

            ChangeSinceQuit.Data.ProducedAmount += produce;
            data.Resource.CurrentAmount += produce;
        }
        private void Produce() {
            timer += Time.deltaTime;
            progressBar.FollowProductionTime(data.GetActualProductionTime(NumberOwned), timer);
            if (timer < data.GetActualProductionTime(NumberOwned)) return;
            InstantiatePopupText();
            progressBar.ResetProgressbar();
            data.Resource.CurrentAmount += data.GetActualProductionAmount(data.Level) * (ulong) NumberOwned * (ulong)(1 + castleData.AscensionBonus * PlayerHandler.PlayerLevel);
            timer -= data.GetActualProductionTime(NumberOwned);
            isProducing = false;
        }

        private void InstantiatePopupText() {
            var instance = Instantiate(popupTextPrefab, this.popupTextSpawnPoint.transform);
            instance.GetComponent<Text>().text = $"+{data.GetActualProductionAmount(data.Level) * (ulong) NumberOwned * (ulong)(1 + castleData.AscensionBonus * PlayerHandler.PlayerLevel)}";    
            instance.GetComponent<Text>().color = data.Resource.resourceColor;
        }

        private ulong BuyAmount() {
            ulong buyAmount = BulkPurchase.Data.BuyAmount < 1000 ? (ulong) BulkPurchase.Data.BuyAmount : MaxBulkPurchase();
            return buyAmount;
        }
        private ulong MaxBulkPurchase() {
            ulong buyAmount = data.Resource.CurrentAmount / data.GetActualPrice(NumberOwned);
            return buyAmount;
        }
    }
}