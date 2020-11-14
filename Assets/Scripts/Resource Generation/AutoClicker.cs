using UnityEngine;

namespace Resource_Generation {
    public class AutoClicker : MonoBehaviour {
        [SerializeField] private Data data;

        public void Activate() {
            //TODO add price
            data.AutoClicker = 1;
        }

        //temporary method to stop auto clicker for testing purposes
        public void Stop() {
            data.AutoClicker = 0;
        }
    }
}
