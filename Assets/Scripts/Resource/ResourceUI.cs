using UnityEngine;
using UnityEngine.UI;

namespace Resource {
    public class ResourceUI : MonoBehaviour {
        [SerializeField] private Text resourceText;
        [SerializeField] private Resource resource;

        private void Update()
        {
            string tokenString = SuffixHelper.GetString(resource.CurrentAmount);
            resourceText.text = $"{tokenString}";
        }
    }
}
