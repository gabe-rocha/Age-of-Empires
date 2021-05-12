public static class GameData
{
    public enum EventTypes
    {
        GameReady,
        GameStarted,
        GamePaused,
        GameUnpaused,
        GameWin,
        GameOver,

        TouchBegan,
        TouchMoved,
        TouchEnded,

        GatherablesAmountChanged,
        TappedToPlay
    }

    public enum Faction
    {
        Player,
        Enemy,
        Enemy2,
        Enemy3
    }

    //DATA VARIABLES - Must be reset when Scene Loads
    //************************************************************************************
    public static int currentLevel;
    public static float gravityForce = -9.8f;

    //************************************************************************************

    public static void ResetAllData()
    {

    }

    public static void LoadPlayerPrefs()
    {
        
    }

    public static void SavePlayerPrefs()
    {
        
    }
}
