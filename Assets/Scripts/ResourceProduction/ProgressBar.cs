using UnityEngine;
using UnityEngine.UI;

namespace ResourceProduction {
    [ExecuteInEditMode]
    public class ProgressBar : MonoBehaviour {
    
        public Text progressText;

        private Slider slider;

        private void Awake() {
            slider = gameObject.GetComponent<Slider>();
        }

        public void FollowProductionTime(float productionTime, float timer) {
            slider.maxValue = productionTime;
            slider.value = timer;
            progressText.text = Mathf.RoundToInt(timer * 100f / productionTime) + "%";
        }

        public void ResetProgressbar() {
            slider.value = 0f;
            progressText.text = "0%";
        }
    }
}