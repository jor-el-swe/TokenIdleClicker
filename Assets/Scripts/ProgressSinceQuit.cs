using UnityEngine;

public static class ProgressSinceQuit {
    private static float elapsedTime;
    private static ulong producedAmount;
    public static float ElapsedTime {
        get => elapsedTime;
        set => elapsedTime = value;
    }
    public static ulong ProducedAmount {
        get => producedAmount;
        set => producedAmount = value;
    }

}