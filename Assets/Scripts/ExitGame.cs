using UnityEngine;

public class ExitGame : MonoBehaviour {
    private void FixedUpdate() {
        if (Input.GetButton("Cancel"))
            Exit();
    }

    public void Exit() {
        Application.Quit();
    }
}
