﻿using UnityEngine;
using UnityEngine.UI;

namespace ChangeSinceQuit {
    public class ProducedUI : MonoBehaviour {
        [SerializeField] private Text textUI;
        [SerializeField] private int destroyTime = 10;

        public void OnClickDestroy() {
            Destroy(this.gameObject);
        }
        private void Start() {
            this.Invoke("UpdateText", 0.01f);
        }
        private void UpdateText() {
            if (Data.ProducedAmount == 0) {
                Destroy(this.gameObject);
                return;
            }
            textUI.text = $"Tokens produced while gone: \n";
            textUI.text += SuffixHelper.GetString(Data.ProducedAmount, false);
            textUI.enabled = true;
            Destroy(this.gameObject, destroyTime);
        }
    }
}