using UnityEngine;

namespace Castle {
    [CreateAssetMenu (fileName = "CastleData", menuName = "ScriptableObjects/CastleData")]
    public class Data : ScriptableObject {
        [SerializeField] private Resource.Resource resource;
        [SerializeField] private int price = 52092672;
        [SerializeField] private float priceMultiplier = 1.2f;
        [SerializeField] private float ascensionBonus = 15.9f;

        public Resource.Resource Resource => resource;

        public float AscensionBonus => ascensionBonus;

        public ulong GetActualPrice(int numberOwned) {
            return (ulong) (price * Mathf.Pow (priceMultiplier, numberOwned));
        }
    }
}
