using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Parent Transforms")]
    [SerializeField] private Transform createdPiecesParent;
    [SerializeField] private Transform piecesParent;
    [SerializeField] private Transform gridParent;
    
    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private PiecesAnimationHandler piecesAnimationHandler;
    
    [Header("Scripts")]
    [SerializeField] private SceneChanger sceneChanger;
    
    [Header("Components")]
    [SerializeField] private TMP_Text levelText;
    
    private List<Level> _levels;
    private Level currentLevel;
    private int currentLevelIndex;

    private ProceduralGridGeneration _proceduralGridGeneration = new ProceduralGridGeneration();
    private ProceduralPieceGenerator _proceduralPieceGenerator = new ProceduralPieceGenerator();
    private void Start()
    {
        ReadLevelDatas();
        
        CreateNewLevel();
    }

    // Gets levels from JSON file and the level index from Player Prefs
    public void ReadLevelDatas()
    {
        _levels = JSONSaveSystem.ReadRomJson<Level>();
        currentLevelIndex = PlayerPrefs.GetInt(LevelButton.PlayerPrefsLevelIndex, 0);
    }

  
    public void CreateNewLevel()
    {
        // If there aren't any more levels return to Main Menu
        if (_levels.Count <= 0 || _levels.Count <= currentLevelIndex)
        {
            sceneChanger.LoadScene(SceneChanger.MainMenu);
            return;
        } 
        
        levelText.text = $"LEVEL {currentLevelIndex + 1}";
        Invoke(nameof(CreateLevel) ,1f);
        Invoke(nameof(PlayPiecesStartAnimation) ,2f);
        
    }
    
    private void CreateLevel()
    {
        currentLevel = _levels[currentLevelIndex];
        
        CreateGrid();
        CreatePieces();
        
        PlayStartAnimation();
    }

    private void CreateGrid()
    {
        _proceduralGridGeneration.GridSize = currentLevel.gridSize;
        _proceduralGridGeneration.Parent = gridParent;
        _proceduralGridGeneration.CreateGrid();
    }

    private void CreatePieces()
    {
        _proceduralPieceGenerator.GridSize = currentLevel.gridSize;
        _proceduralPieceGenerator.GetPieceData = currentLevel.pieces;
        _proceduralPieceGenerator.Parent = createdPiecesParent;
        _proceduralPieceGenerator.CreateGrid();
        _proceduralPieceGenerator.CreateMeshesOfAllPieces();
        ResetPiecesAnchorPointList();
    }

 
    private void ResetPiecesAnchorPointList()
    {
        for (int i = 0; i < piecesParent.childCount; i++)
        {
            if (piecesParent.GetChild(i).gameObject.activeSelf)
            {
                piecesParent.GetChild(i).GetComponent<Dragabble>().InitializePlacedPiecesIndex();
            }
        }
    }
    public void OpenNextLevel()
    {
        PlayLevelSuccessAnimation();
        currentLevelIndex++;
        CreateNewLevel();
    }

    #region Animations

    private void PlayPiecesStartAnimation()
    {
        piecesAnimationHandler.PlayStartAnimation();
    }
    
    private void PlayStartAnimation()
    {
        animator.SetTrigger("startLevel");
    }
    public void PlayLevelSuccessAnimation()
    {
        animator.SetTrigger("levelSuccess");
        // Invoke(nameof(GenerateNewLevel), .90f);
    }

    #endregion
    
}
