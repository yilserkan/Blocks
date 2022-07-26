using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private Button saveButton;
    [SerializeField] private Button generateButton;
    [SerializeField] private SceneChanger sceneChanger;

    public void DisableButtons()
    {
        saveButton.interactable = false;
        generateButton.interactable = false;
        Invoke(nameof(EnableGenerateButton), 1f);
    }
    private void EnableGenerateButton()
    {
        generateButton.interactable = true;
    }

    public void ReturnToMainMenu()
    {
        sceneChanger.LoadScene(SceneChanger.MainMenu);
    }

    public void EnableSaveButton()
    {
        if (saveButton.interactable == false)
        {
            saveButton.interactable = true;
        }
    }
}
