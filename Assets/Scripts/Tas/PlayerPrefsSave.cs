using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsSave : MonoBehaviour
{
    public static bool OnSound
    {
        set { PlayerPrefs.SetInt("OnSound", value ? 1 : 0); }
        get { return PlayerPrefs.GetInt("OnSound", 1) == 1; }
    }
    public static bool OnMusic
    {
        set { PlayerPrefs.SetInt("OnMusic", value ? 1 : 0); }
        get { return PlayerPrefs.GetInt("OnMusic", 1) == 1; }
    }
    public static bool OnVibrate
    {
        set { PlayerPrefs.SetInt("OnVibrate", value ? 1 : 0); }
        get { return PlayerPrefs.GetInt("OnVibrate", 1) == 1; }
    }
    public static bool rateDone
    {
        set { PlayerPrefs.SetInt("rateDone", value ? 0 : 1); }
        get { return PlayerPrefs.GetInt("rateDone", 0) == 0; }
    }
    public static int level
    {
        set { PlayerPrefs.SetInt("level", value); }
        get { return PlayerPrefs.GetInt("level", 0); }
    }
    public static int levelstart
    {
        set { PlayerPrefs.SetInt("levelstart", value); }
        get { return PlayerPrefs.GetInt("levelstart", 0); }
    }
    public static int interCount
    {
        set { PlayerPrefs.SetInt("interCount", value); }
        get { return PlayerPrefs.GetInt("interCount", 0); }
    }

    public static bool IsAcceptNotiAndroid13
    {
        get => PlayerPrefs.GetInt("IsAcceptNotiAndroid13", 0) == 1;
        set => PlayerPrefs.SetInt("IsAcceptNotiAndroid13", value ? 1 : 0);
    }
    public static bool FIRSTOPENAPP
    {
        get => PlayerPrefs.GetInt("KEYFIRSTOPENAPP", 1) == 1;
        set => PlayerPrefs.SetInt("KEYFIRSTOPENAPP", value ? 1 : 0);
    }

    public static int NormalLevel
    {
        set { PlayerPrefs.SetInt("NormalLevel", value); }
        get { return PlayerPrefs.GetInt("NormalLevel", 0); }
    }
}
