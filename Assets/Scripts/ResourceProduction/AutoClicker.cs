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
            buyText.text = $"{textPrefix}: {price} {priceResource}";
        }

        public void Buy() {
            if (priceResource.CurrentAmount < price)
                return;
            data.AutoClicker = 1;
            buyText.text = "";
        }

        // Temporary method to remove auto clicker for testing purposes
        public void Remove() {
            data.AutoClicker = 0;
            buyText.text = $"{textPrefix}: {price} {priceResource}";
        }
    }
}
