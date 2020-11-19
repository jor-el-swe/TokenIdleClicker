using UnityEngine;
using UnityEngine.UI;

namespace Resource {
    public class ResourceUI : MonoBehaviour {
        [SerializeField] private Text resourceText;
        [SerializeField] private Resource resource;
            string phrase = "";
            string[] words = phrase.Split('\n');
            string[0] = 12345;
            string[1] = "Billions";
        private void Update()
        {
            string tokenString = SuffixHelper.GetString(resource.CurrentAmount, true);
            resourceText.text = $"{tokenString}";
        }
    }
}
