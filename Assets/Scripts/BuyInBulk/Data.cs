using UnityEngine;

namespace BulkPurchase {
    public class Data : MonoBehaviour {
        static int buyAmount;
        public static int BuyAmount {
            get => buyAmount;
            set => buyAmount = value;
        }
    }
}