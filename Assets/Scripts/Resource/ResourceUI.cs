using System;
using UnityEngine;
using UnityEngine.UI;

namespace Resource {
    public class ResourceUI : MonoBehaviour {
        [SerializeField] private Text resourceText;
        [SerializeField] private Text resourceSuffixText;
        [SerializeField] private ResourceObject resource;
        
        private void Update()
        {
            var tokenString = SuffixHelper.GetString(resource.CurrentAmount, true);
            string[] words = tokenString.Split(new string[] { "\n" }, StringSplitOptions.None);
            resourceText.text = words[0];
            if (words.Length < 2) {
                resourceSuffixText.text = "";
                return;
            }
            resourceSuffixText.text = words[1];
        }
    }
}
