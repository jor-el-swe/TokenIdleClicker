using UnityEngine;

public static class ProgressSinceQuit {
    private static float elapsedTime;
    private static int producedAmount;
    public static float ElapsedTime {
        get => elapsedTime;
        set => elapsedTime = value;
    }
    public static int ProducedAmount {
        get => producedAmount;
        set => producedAmount = value;
    }

}