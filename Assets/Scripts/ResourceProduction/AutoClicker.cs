using System;
using UnityEngine;
using UnityEngine.UI;

namespace ResourceProduction {
    public class AutoClicker : MonoBehaviour {
        [SerializeField] private Data data;
        [SerializeField] private int price;
        [SerializeField] private Resource.Resource priceResource;
        [SerializeField] private string textPrefix = "Auto Clicker";
        [SerializeField] private Text buyText;

        private void Start() {
            buyText.text = $"{textPrefix} {price} {priceResource.name}";
        }

        public void Buy() {
            if (priceResource.CurrentAmount < price)
                return;
            priceResource.CurrentAmount -= price;
            data.AutoClicker = 1;
            buyText.text = "";
        }

        // Temporary method to remove auto clicker for testing purposes
        // Delete this method when it's not needed anymore
        public void Remove() {
            data.AutoClicker = 0;
            buyText.text = $"{textPrefix} {price} {priceResource.name}";
        }
    }
}
