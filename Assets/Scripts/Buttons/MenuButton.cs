using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private SceneChanger sceneChanger;
   
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    
    // Called on button clicks. Plays button press animation.
    public void SetButtonAnimationBoolTrue()
    {
        _animator.SetTrigger("pressed");
    }
    
    public void LoadGenerateLevelScene()
    {
        sceneChanger.LoadScene(SceneChanger.LevelGenerator);
    }
}
