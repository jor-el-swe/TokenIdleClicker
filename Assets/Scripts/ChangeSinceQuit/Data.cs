namespace ChangeSinceQuit {
    public static class Data {
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
}