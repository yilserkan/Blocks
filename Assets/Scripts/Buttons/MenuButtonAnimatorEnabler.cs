using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonAnimatorEnabler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Button playButton;
    [SerializeField] private Button generateButton;

    //Called From Canvas Animation
    public void EnableAnimators()
    {
        playButton.GetComponent<Animator>().enabled = true;
        generateButton.GetComponent<Animator>().enabled = true;
    }

    public void SlideToLevels()
    {
        animator.SetBool("slideTrigger", true);
    }
    
    public void SlideToMainMenu()
    {
        animator.SetBool("slideTrigger", false);
    }
}
