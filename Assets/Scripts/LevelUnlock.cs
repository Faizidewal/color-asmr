using UnityEngine.UI;
using UnityEngine;

public class LevelUnlock : MonoBehaviour
{
    public Button[] LevelButtons;
    void Start()
    {
        foreach (Button b in LevelButtons)
            b.interactable = false;
        int THISLEVEL = PlayerPrefs.GetInt("THISLEVEL", 1);

        for (int i = 0; i < THISLEVEL; i++)
            LevelButtons[i].interactable = true;
    }


}
