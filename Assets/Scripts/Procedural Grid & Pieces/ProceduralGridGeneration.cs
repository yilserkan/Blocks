using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class ProceduralGridGeneration
{
    private int _gridSize = 3;  
    public int GridSize { get { return _gridSize; } set { _gridSize = value; } }
    
    private float _cellSize = 1; 
    public float GetCellSize { get { return _cellSize; } }
    
    private Transform _parent;
    public Transform Parent { get { return _parent; } set { _parent = value; } }

    public static event Action<List<Vector3>, Transform, bool> CreateAnchorPoints;
    public static event Action ResetPiecesAndAnchorPoints;

    private GameObject _grid;
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    private GridGenerator _gridGenerator;

    private bool _anchorPointVisible = true;
    private bool _initialized = false;

    public void CreateGrid()
    {
        if (!_initialized)
        {
            InitializeGrid();
        }
        
        ResetPiecesAndAnchorPoints?.Invoke();
        
        _gridGenerator = new GridGenerator(_gridSize, _cellSize);
        _gridGenerator.CreateProceduralGrid();
        _gridGenerator.CreateMesh(_meshFilter.mesh, _meshRenderer, _parent );
        
        _meshFilter.name = "Grid";
        
        CreateAnchorPoints?.Invoke(_gridGenerator.GetAnchorPoints, _grid.transform ,_anchorPointVisible);
    }

    private void InitializeGrid()
    {
        _grid = new GameObject();
        _meshFilter = _grid.AddComponent<MeshFilter>();
        _meshRenderer = _grid.AddComponent<MeshRenderer>();
        _grid.AddComponent<Grid>();
        _initialized = true;
    }

}