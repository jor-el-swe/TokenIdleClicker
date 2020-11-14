using UnityEngine;

[CreateAssetMenu]
public class TimeSinceQuit : ScriptableObject {
    private float elapsedTime = 0;
    public float ElapsedTime {
        get => elapsedTime;
        set => elapsedTime = value;
    }
}