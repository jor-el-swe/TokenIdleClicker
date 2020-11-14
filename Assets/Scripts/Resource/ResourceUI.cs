using UnityEngine;
using UnityEngine.UI;

namespace Resource {
    public class ResourceUI : MonoBehaviour {
        [SerializeField] private Text resourceText;
        [SerializeField] private Resource resource;

        private void Update() {
            resourceText.text = $"Tokens:\n{resource.CurrentAmount}";
        }
    }
}
