using System;
using UnityEngine;

namespace Castle {
    public class Castle : MonoBehaviour {
        [SerializeField] private Data data;
        public Data Data => data;
        private CastleUI UI => GetComponent<CastleUI>();
        
        private AudioHandler audiohandler;
        private string OwnedKey => $"{data.name}_owned";
        public int NumberOwned {
            get => PlayerPrefs.GetInt(OwnedKey, 0);
            private set => PlayerPrefs.SetInt(OwnedKey, value);
        }

        public void Buy() {
            var castlePrice = data.GetActualPrice(NumberOwned);
            if (data.Resource.CurrentAmount < castlePrice)
            {
                audiohandler.Play("nono");
                return;
            }
            audiohandler.Play("buyStore");
            data.Resource.CurrentAmount -= castlePrice;
            NumberOwned++;
            UI.EnableRandomCastleIcon();
        }

        private void Start()
        {
            //get audioHandler
            audiohandler = FindObjectOfType<AudioHandler>();
        }
    }
}