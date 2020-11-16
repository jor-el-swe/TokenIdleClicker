using UnityEngine;
using UnityEngine.UI;
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

        textUI.enabled = true;
        textUI.text = ProgressSinceQuit.ProducedAmount.ToString ($"Tokens produced while gone: \n 0");
        Destroy (this.gameObject, destroyTime);
    }
}