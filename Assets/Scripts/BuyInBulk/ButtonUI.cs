using UnityEngine;
using UnityEngine.UI;
namespace BulkPurchase {
    public class ButtonUI : MonoBehaviour {
        [SerializeField] private int[] buyPerClick;
        [SerializeField] private Text buttonText;
        private int index;
        private void Start() {
            Data.BuyAmount = buyPerClick[index];
            UpdateText();
        }
        public void OnChangeAmount() {

            int oldIndex = (index + 1) < buyPerClick.Length ? index++ : index = 0;
            Data.BuyAmount = buyPerClick[index];
            UpdateText();
        }
        private void UpdateText() {
            buttonText.text = buyPerClick[index] < 1000 ? buyPerClick[index].ToString("x 0") : "Max";
        }
    }
}