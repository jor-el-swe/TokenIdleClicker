using UnityEngine;

public class ScreenManager : MonoBehaviour {

    public enum MenuStates {
        Main,
        Upgrade,
        HighScore
    }

    [SerializeField] private GameObject mainScreen;
    [SerializeField] private GameObject upgradeScreen;
    [SerializeField] private GameObject highScoreScreen;

    public MenuStates currentState;

    private void Awake() {
        currentState = MenuStates.Main;
    }

    private void Update() {
        switch (currentState) {
            case MenuStates.Main:
                mainScreen.SetActive(true);
                upgradeScreen.SetActive(false);
                highScoreScreen.SetActive(false);
                break;
            
            case MenuStates.Upgrade:
                upgradeScreen.SetActive(true);
                mainScreen.SetActive(false);
                highScoreScreen.SetActive(false);
                break;
            
            case MenuStates.HighScore:
                highScoreScreen.SetActive(true);
                mainScreen.SetActive(false);
                upgradeScreen.SetActive(false);
                break;
        }
    }

    public void OnUpgrade() {
        currentState = MenuStates.Upgrade;
    }

    public void OnHighScore() {
        currentState = MenuStates.HighScore;
    }
    
    public void OnBackToMain() {
        currentState = MenuStates.Main;
    }
}
