using UnityEngine;
using UnityEngine.UI;

namespace Resource_Generation
{
    public class Generator : MonoBehaviour
    {
        [SerializeField] private int generatedAmount = 1;
        [SerializeField] private Resource resource;

        public void Generate()
        {
            resource.CurrentAmount += generatedAmount;
        }
    }
}
