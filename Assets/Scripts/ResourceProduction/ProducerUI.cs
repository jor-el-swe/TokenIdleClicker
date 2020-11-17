using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ResourceProduction;
using UnityEngine.UI;

namespace ResourceProduction
{
    public class ProducerUI : MonoBehaviour
    {
        
        [SerializeField] private Text buyText;
        [SerializeField] private Text productionTimeText;
        [SerializeField] private Text numberOwnedText;
        [SerializeField] private Image shopIcon;
        private Data data;
        private Producer Producer => GetComponent<Producer>();

        private void UpdateShopIcon() {
            shopIcon.enabled = Producer.NumberOwned > 0;
        }
        private void Start() {
            data = Producer.Data;
        }
        public void FixedUpdate() {
            UpdateOwnedText();
            UpdateBuyText();
            UpdateShopIcon();
        }
        private void UpdateOwnedText() {
            numberOwnedText.text = Producer.NumberOwned.ToString();
            productionTimeText.text = data.GetActualProductionTime(Producer.NumberOwned).ToString("Production Time: 0.00");
        }
        private void UpdateBuyText() {
            buyText.text = $"Buy\n {SuffixHelper.GetString(data.GetActualPrice(Producer.NumberOwned))} Tokens ";
        }
    }
}
