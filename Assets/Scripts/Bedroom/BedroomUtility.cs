using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BedroomUtility
{
    static int stage;
    static bool badEnding;
    static int attempts;
    static int failCount;
    public static void SetStage(int s)
    {
        if (attempts > 2)
        {
            attempts = 0;
            stage = s + 1;
        }
        else
        {
            stage = s;
        }
    }
    public static int GetStage()
    {
        if (stage == 0)
        {
            Debug.LogError("MISSING STAGE");
            return 1;
        }
        return stage;
    }
    public static void SetBadEnding(bool end)
    {
        if (!end)
        {
            attempts = 0;
        }
        else if (end && attempts == 0)
        {
            failCount++;
        }
        badEnding = end;
    }
    public static bool GetBadEnding()
    {
        return badEnding;
    }
    public static void AddAttempt(int a)
    {
        attempts += a;
    }
    public static int GetAttempts()
    {
        return attempts;
    }
    public static bool BadEndingFinal()
    {
        if (failCount > 2) return true;
        else return false;
    }
}
