using UnityEngine;

public static class GameController
{
    private static int collectableCount;
    private static int score;
    private static bool poisoned;

    public static int Score
    {
        get { return score; }
    }

    public static bool HasFailed
    {
        get { return poisoned; }
    }

    public static bool gameOver
    {
        get { return collectableCount <= 0 || poisoned; }
    }

    public static void Init()
    {
        collectableCount = 8;
        score = 0;
        poisoned = false;
    }

    public static void Collect()
    {
        collectableCount--;
        score++;
    }

    public static void Fail()
    {
        poisoned = true;
    }
}
