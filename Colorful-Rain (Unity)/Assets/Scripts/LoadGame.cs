using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    public Button[] levelBtns;
    void Start()
    {
        // 0 = locked, 1 = unlocked, -1 = key not set
        SetLevelStates("Level1Enabled", 1);
        SetLevelStates("Level2Enabled", 0);
        SetLevelStates("Level3Enabled", 0);
        SetLevelStates("Level4Enabled", 0);
        SetLevelStates("Level5Enabled", 0);
        SetLevelStates("Level6Enabled", 0);
        SetLevelStates("Level7Enabled", 0);
        SetLevelStates("Level8Enabled", 0);
        SetLevelStates("Level9Enabled", 0);
        SetLevelStates("Level10Enabled", 0);

        CheckLevelStates();
    }

    private void CheckLevelStates()
    {
        for (int i = 1; i <= 10; i++)
        {
            string levelKey = "Level" + i + "Enabled";
            if (IsLevelLocked(levelKey))
            {
                // Disable button
                levelBtns[i - 1].interactable = false;
            }
            else
            {
                // Enable button
                levelBtns[i - 1].interactable = true;
            }
        }
    }

    private void SetLevelStates(string levelKey, int value)
    {
        if (PlayerPrefs.GetInt(levelKey, -1) == -1)
            PlayerPrefs.SetInt(levelKey, value);
        else
            PlayerPrefs.SetInt(levelKey, PlayerPrefs.GetInt(levelKey));
    }
    private bool IsLevelLocked(string levelKey)
    {
        if (PlayerPrefs.GetInt(levelKey) == 0)
            return true;
        else
            return false;
    }

    public void UnlockLevel(string levelKey)
    {
        PlayerPrefs.SetInt(levelKey, 1);

        CheckLevelStates();
    }
}
