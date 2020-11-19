using UnityEngine;
using UnityEngine.UI;

namespace Resource {
    public class ResourceUI : MonoBehaviour {
        [SerializeField] private Text resourceText;
        [SerializeField] private Text resourceSuffixText;
        [SerializeField] private Resource resource;
       // tokenstring split in two at \n, substring

       string tokenString = "The quick brown fox jumps over the lazy dog.";
       string[] words = ResourceText.Split('\n');
        private void Update()
        {
            var tokenString = SuffixHelper.GetString(resource.CurrentAmount, true);
            string[] w
            resourceText.text = $"{tokenString}";
        }
    }
}
