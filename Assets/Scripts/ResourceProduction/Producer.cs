using UnityEngine;
using UnityEngine.UI;

namespace ResourceProduction {
    public class Producer : MonoBehaviour {
        private float timer;
        private bool isProducing;

        [SerializeField] private Data data;
        [SerializeField] private GameObject produceButton;
        [SerializeField] private Text buyText;
        [SerializeField] private Text upgradeText;
        [SerializeField] private Text numberOwnedText;
        [SerializeField] private Text levelText;
        [SerializeField] private Text productionTimeText;
        [SerializeField] private int maxLevel;
        [SerializeField] private TimeSinceQuit timeSinceQuit;
        //[SerializeField] private Text producedSinceQuitText;
        [SerializeField] private ProgressBar progressBar;

        private string OwnedKey => $"{data.name}_owned";
        private string LevelKey => $"{data.name}_level";

        private int NumberOwned {
            get => PlayerPrefs.GetInt (OwnedKey, 1);
            set => PlayerPrefs.SetInt (OwnedKey, value);
        }

        private int Level {
            get => PlayerPrefs.GetInt (LevelKey, 0);
            set => PlayerPrefs.SetInt (LevelKey, value);
        }

        public void Buy () {
            if (data.Resource.CurrentAmount < data.GetActualPrice (NumberOwned))
                return;
            data.Resource.CurrentAmount -= data.GetActualPrice (NumberOwned);
            NumberOwned++;
            UpdateBuyText ();
            UpdateOwnedText ();
        }

        public void StartProduction () {
            isProducing = true;
        }

        public void Upgrade () {
            if (!(Level < maxLevel) || data.Resource.CurrentAmount < data.GetActualUpgradePrice (Level))
                return;
            data.Resource.CurrentAmount -= data.GetActualUpgradePrice (Level);
            Level++;
            UpdateLevelText ();
        }

        private void Start () {
            UpdateBuyText ();
            UpdateLevelText ();
            UpdateOwnedText ();
            ProduceAtStart ();
        }

        private void Update () {
            // Disables produceButton when AutoClicker is active and vice versa
            produceButton.SetActive (!data.AutoClickerActive);

            if (data.AutoClickerActive) {
                Produce ();
                return;
            }

            if (isProducing)
                Produce ();
        }
        private void ProduceAtStart () {
            //producedSinceQuitText.enabled = false;
            if (!data.AutoClickerActive || timeSinceQuit.ElapsedTime < 1)
                return;
            var produce = data.GetActualProductionAmount (Level) * NumberOwned * Mathf.RoundToInt (timeSinceQuit.ElapsedTime / data.GetActualProductionTime (NumberOwned));
            data.Resource.CurrentAmount += produce;
            //producedSinceQuitText.text = produce.ToString ("Tokens produced while gone:\n 0");
            //producedSinceQuitText.enabled = true;
            this.Invoke ("TempDisableText", 5); //Remove later
        }
        private void TempDisableText () { //Remove later!
            //producedSinceQuitText.enabled = false;
        }
        private void Produce () {
            timer += Time.deltaTime;
            progressBar.FollowProductionTime(data.GetActualProductionTime(NumberOwned), timer);
            if (timer < data.GetActualProductionTime (NumberOwned)) return;
            progressBar.ResetProgressbar();
            data.Resource.CurrentAmount += data.GetActualProductionAmount (Level) * NumberOwned;
            timer -= data.GetActualProductionTime (NumberOwned);
            isProducing = false;
        }

        private void UpdateOwnedText () {
            numberOwnedText.text = NumberOwned.ToString ();
            productionTimeText.text = data.GetActualProductionTime (NumberOwned).ToString ("Production Time: 0.00");
        }

        private void UpdateLevelText () {
            upgradeText.text = $"Upgrade {data.GetActualUpgradePrice(Level)} Tokens ";
            levelText.text = Level.ToString ();
        }

        private void UpdateBuyText () {
            buyText.text = $"Buy {data.GetActualPrice(NumberOwned)} Tokens ";
        }
    }
}