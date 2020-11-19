using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ResourceProduction {
    public class ProducerUI : MonoBehaviour {

        [SerializeField] private Text buyText;
        [SerializeField] private Text buyTextNumber;
        [SerializeField] private Text productionTimeText;
        [SerializeField] private Text numberOwnedText;
        [SerializeField] private Image shopIcon;
        [SerializeField] private Button produceButton;
        [SerializeField] private Animator buyButtonAnim;

        private Data data;
        private Producer producer => GetComponent<Producer>();

        public void UpdateOnClick()
        {
            buyButtonAnim.SetTrigger("Pressed");
        }
        private void Start() {
            data = producer.Data;
            BulkPurchase.ButtonUI.buttonUI.onButtonPress += UpdateBuyText;
            Producer.onUpdateTextEvent += UpdateAllUI;
            StartCoroutine(OnUpdateUI());
            if (produceButton != null)
                DisableProduceButton();
        }
        private IEnumerator OnUpdateUI() {
            while (true) {
                UpdateAllUI();
                if (produceButton != null)
                    DisableProduceButton();
                yield return new WaitForSeconds(1);
            }
        }
        private void UpdateAllUI() {
            UpdateBuyText();
            UpdateOwnedText();
            UpdateShopIcon();
        }
        private void UpdateOwnedText() {
            var numberOwned = producer.NumberOwned;
            var thresholdLevel = data.GetThresholdLevel(numberOwned);
            var currentThreshold = data.IncreaseSpeedThresholds[thresholdLevel];
            numberOwnedText.text = $"{numberOwned} / {currentThreshold}";
            productionTimeText.text = data.GetActualProductionTime(numberOwned).ToString("Production Time: 0.00");
        }
        private void UpdateBuyText() {
            (ulong cost, int amount) = data.GetActualBulkPrice(producer.NumberOwned);
            buyTextNumber.text = $"{amount}";
            buyText.text = $"\n {SuffixHelper.GetString(cost, false)} Tokens ";
            if (buyButtonAnim != null)
                UpdateBuyAnim(cost);
        }
        private void UpdateShopIcon() {
            shopIcon.enabled = producer.NumberOwned > 0;
        }
        private void OnDestroy() {
            BulkPurchase.ButtonUI.buttonUI.onButtonPress -= UpdateBuyText;
            Producer.onUpdateTextEvent -= UpdateAllUI;
        }
        private void DisableProduceButton() {
            if (!data.AutoClickerActive && producer.NumberOwned > 0) {
                produceButton.interactable = true;
            } else {
                produceButton.interactable = false;
            }
        }
        private void UpdateBuyAnim(ulong cost) {
            if (data.Resource.CurrentAmount >= cost) {
                buyButtonAnim.SetBool("IsDisabled", false);
            } 
            else {
                buyButtonAnim.SetBool("IsDisabled", true);
            }
        }
    }
}