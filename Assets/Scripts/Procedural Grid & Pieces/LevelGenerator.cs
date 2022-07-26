
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Parent Transforms")]
    [SerializeField] private Transform piecesParent;
    [SerializeField] private Transform gridParent;

    [Header("Components")]
    [SerializeField] private Animator _animator;
    [SerializeField] private TMP_Dropdown _dropdown;

    private int _gridSize;
    private int _pieceAmount;

    private ProceduralGridGeneration _proceduralGridGeneration = new ProceduralGridGeneration();
    private ProceduralPieceGenerator _proceduralPieceGenerator = new ProceduralPieceGenerator();

    private Level _level;
    
    // Called On Generate Button Click
    public void GenerateNewLevel()
    {
        PlayStartAnimation();
        
        ReadDifficulty();
        
        CreateGrid();
     
        CreatePieces();
      
    }
    
    // Called On Save Button Click
    // Adds the created level to JSON file
    public void SaveLevel()
    {
        PlayLevelSuccessAnimation();
        
        List<Level> levels = new List<Level>();
        levels.Add(new Level(_proceduralGridGeneration.GridSize, _proceduralPieceGenerator.GetPieceData));
 
        if (levels != null)
        {
            JSONSaveSystem.SaveToJSON(levels ,true);
        }
    }

    
    private void ReadDifficulty()
    {
        switch (_dropdown.options[_dropdown.value].text)
        {
            case "Easy":
                _gridSize = 4;
                _pieceAmount = Random.Range(5, 8);
                break;
            case "Medium":
                _gridSize = 5;
                _pieceAmount = Random.Range(8, 11);
                break;
            case "Hard":
                _gridSize = 6;
                _pieceAmount = Random.Range(11, 13);
                break;
        }
    }

    private void CreateGrid()
    {
        _proceduralGridGeneration.GridSize = _gridSize;
        _proceduralGridGeneration.Parent = gridParent;
        _proceduralGridGeneration.CreateGrid();
    }

    private void CreatePieces()
    {
        _proceduralPieceGenerator.GridSize = _gridSize;
        _proceduralPieceGenerator.PieceAmount = _pieceAmount;
        _proceduralPieceGenerator.Parent = piecesParent;
        _proceduralPieceGenerator.CreatePieces();
        ResetPiecesAnchorPointList();
    }
    
    private void ResetPiecesAnchorPointList()
    {
        for (int i = 0; i < piecesParent.childCount; i++)
        {
            // Object pool is used so we need to check if the child gameobject is active
            if (piecesParent.GetChild(i).gameObject.activeSelf)
            {
                piecesParent.GetChild(i).GetComponent<Dragabble>().InitializePlacedPiecesIndex();
            }
        }
    }
    
    private void PlayStartAnimation()
    {
        _animator.SetTrigger("generateLevel");
    }

    private void PlayLevelSuccessAnimation()
    {
        _animator.SetTrigger("levelSave");
    }
}
