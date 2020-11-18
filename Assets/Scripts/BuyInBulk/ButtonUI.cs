using UnityEngine;
using UnityEngine.UI;
namespace BulkPurchase {
    public class ButtonUI : MonoBehaviour {
        public static ButtonUI buttonUI;
        public event System.Action onButtonPress;
        [SerializeField] private int[] buyPerClick;
        [SerializeField] private Text buttonText;

        private int index;

        public void OnChangeAmount() {

            int oldIndex = (index + 1) < buyPerClick.Length ? index++ : index = 0;
            Data.BuyAmount = buyPerClick[index];
            UpdateText();
            SendEvent();
        }
        private void Awake() {
            buttonUI = this;
            Data.BuyAmount = buyPerClick[index];
        }
        private void Start() {
            SendEvent();
            UpdateText();
        }
        private void SendEvent() {
            if (onButtonPress == null)
                return;
            onButtonPress();
        }
        private void UpdateText() {
            buttonText.text = buyPerClick[index] < 101 ? buyPerClick[index].ToString("x 0") : "Max";
        }
    }
}