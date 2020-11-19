using UnityEngine;

public class ExitGame : MonoBehaviour {
    private void Update() {
        if (Input.GetButton("Cancel"))
            Exit();
    }

    public void Exit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}