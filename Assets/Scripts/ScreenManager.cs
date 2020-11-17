using UnityEngine;

public class ScreenManager : MonoBehaviour {

    [SerializeField] private Canvas mainScreen;
    [SerializeField] private Canvas upgradeScreen;
    [SerializeField] private Canvas mapScreen;
    

    private void Awake() {
        ShowMain();
    }
    public void OnUpgrade() {
        ShowUpgrade();
    }
    public void OnMap() {
        ShowMap();
    }
    public void OnBackToMain() {
        ShowMain();
    }

    private void ShowMain() {
        mainScreen.enabled = true;
        upgradeScreen.enabled = false;
        mapScreen.enabled = false;
    }
    private void ShowUpgrade() {
        mainScreen.enabled = false;
        upgradeScreen.enabled = true;
        mapScreen.enabled = false;
    }
    private void ShowMap() {
        mainScreen.enabled = false;
        upgradeScreen.enabled = false;
        mapScreen.enabled = true;
    }
}
