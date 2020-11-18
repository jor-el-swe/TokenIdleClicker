using UnityEngine;
using UnityEngine.UI;

namespace Castle {
    public class CastleUI : MonoBehaviour {
        [SerializeField] private Text buyText;
        [SerializeField] private Text numberOwnedText;
        [SerializeField] private Image[] castleIcons;
        private Data data;
        private Castle Castle => GetComponent<Castle>();

        private void Start() {
            data = Castle.Data;
        }
        private void FixedUpdate() {
            UpdateOwnedText();
            UpdateBuyText();
        }
        private void UpdateOwnedText() {
            numberOwnedText.text = $"Castles: {Castle.NumberOwned}";
        }
        private void UpdateBuyText() {
            buyText.text = $"Buy Castle\n {SuffixHelper.GetString(data.GetActualPrice(Castle.NumberOwned))} Tokens";
        }
    }
}
