using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    // This class is attached to the Select Level Buttons
    // As the buttons are created they are given an index 
    // If the button is clicked on the index gets saved in player prefs
    // This way we can load the easily the selected level.
    
    public static string PlayerPrefsLevelIndex = "LevelIndex";
    public int LevelIndex { get; set; }

    public SceneChanger SceneChanger { get; set; }

    public void OpenLevel()
    {
        PlayerPrefs.SetInt(PlayerPrefsLevelIndex, LevelIndex);

        SceneChanger.LoadScene(SceneChanger.Game);
    }
}
