using UnityEngine;
using UnityEngine.UI;

namespace Resource_Generation
{
    public class Generator : MonoBehaviour {
        
        [SerializeField] private int generatedAmount = 1;
        [SerializeField] private Resource resource;
        private string OwnedKey => $"{this.name}_owned";
        private string LevelKey => $"{this.name}_level";
        public int NumberOwned
        {
            get => PlayerPrefs.GetInt(OwnedKey, 0);
            set => PlayerPrefs.SetInt(OwnedKey, value);
        }
        public int Level
        {
            get => PlayerPrefs.GetInt(LevelKey, 0);
            set => PlayerPrefs.SetInt(LevelKey, value);
        }
        public void Generate()
        {
            resource.CurrentAmount += generatedAmount;
        }
    }
}
