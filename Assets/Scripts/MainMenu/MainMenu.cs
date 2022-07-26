using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private Transform levelButtonParent;
    [SerializeField] private SceneChanger sceneChanger;
    
    private List<Level> _levels;
    
    void Start()
    {
        ReadLevelDatas();
        CreateLevelButtons();
    }

    private void ReadLevelDatas()
    {
        _levels = JSONSaveSystem.ReadRomJson<Level>();
    }

    // Create the level buttons in level selection section according to the amount of levels in the JSON file
    private void CreateLevelButtons()
    {
        for (int i = 0; i < _levels.Count; i++)
        {
            GameObject button = Instantiate(levelButtonPrefab, levelButtonParent);
            button.GetComponentInChildren<TextMeshProUGUI>().text = $"Level {i+1}";
            
            LevelButton levelButton = button.GetComponent<LevelButton>();
            levelButton.LevelIndex = i;
            levelButton.SceneChanger = sceneChanger;
        }
    }
}
