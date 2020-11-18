using UnityEngine;

namespace Castle {
    public class Castle : MonoBehaviour {
        [SerializeField] private Data data;
        public Data Data => data;
        private CastleUI UI => GetComponent<CastleUI>();
        private string OwnedKey => $"{data.name}_owned";
        public int NumberOwned {
            get => PlayerPrefs.GetInt(OwnedKey, 0);
            private set => PlayerPrefs.SetInt(OwnedKey, value);
        }
        
        public void DeleteCastleIconKeys() => UI.DeleteCastleIconKeys();
        
        public void Buy() {
            var castlePrice = data.GetActualPrice(NumberOwned);
            if (data.Resource.CurrentAmount < castlePrice)
                return;
            data.Resource.CurrentAmount -= castlePrice;
            NumberOwned++;
            UI.EnableRandomCastleIcon();
        }
    }
}