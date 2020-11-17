using UnityEngine;
using UnityEngine.UI;

namespace ResourceProduction {
    public class ProducedSinceQuitUI : MonoBehaviour {
        [SerializeField] private Text textUI;
        [SerializeField] private int destroyTime = 10;
        private void Start () {
            this.Invoke ("UpdateText", 0.01f);
        }
        private void UpdateText () {
            Debug.Log (ProgressSinceQuit.ProducedAmount);
            if (ProgressSinceQuit.ProducedAmount == 0) {
                Destroy (this.gameObject);
                return;
            }

            textUI.text = $"Tokens produced while gone: \n";
            textUI.text += SuffixHelper.GetString(ProgressSinceQuit.ProducedAmount);
            textUI.enabled = true;

            Destroy (this.gameObject, destroyTime);
        }
    }
}