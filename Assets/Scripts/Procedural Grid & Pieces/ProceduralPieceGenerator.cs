using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProceduralPieceGenerator
{
    private int _gridSize;
    public int GridSize { get { return _gridSize; } set { _gridSize = value; } }
    
    
    private float _cellSize = 1;
    public float CellSize { get { return _cellSize; } set { _cellSize = value; } }
    
    
    private int _pieceAmount = 5;
    public int PieceAmount { get { return _pieceAmount; } set { _pieceAmount = value; } }

    
    private Transform _parent;
    public Transform Parent { get { return _parent; } set { _parent = value; } }
    
    
    private List<PieceData> _createdPieces = new List<PieceData>();
    public List<PieceData> GetPieceData { get { return _createdPieces; } set { _createdPieces = value; } }
    
    
    public static event Action<Vector3[],int[] ,List<Vector3>, Color, Transform> OnCreatePieces;
   
    private List<Face> _faces;
    private List<int> _pieceSizes;

    private GridGenerator _gridGenerator;
    private BFS _bfs;
    
    public void CreatePieces()
    {
        CreateGrid();
        
        FindNeighborsOfTheFacesInTheGrid();
        CalculatePossibleSizesOfNewPieces();

        _bfs = new BFS(_faces, _pieceSizes);
        _bfs.CreatePiecesWithBFS();
        
        _createdPieces = _bfs.GetCreatedPiecesData;
        
        CreateMeshesOfAllPieces();
    }

    public void CreateGrid()
    {
        _gridGenerator = new GridGenerator(_gridSize, _cellSize);
        _gridGenerator.CreateProceduralGrid();
        _faces = _gridGenerator.Faces;
    }
    
    // This function finds all neighbor indexes of the face.
    // Neighbors are needed so that we can perform BFS
    private void FindNeighborsOfTheFacesInTheGrid()
    {
        for (int i = 0; i < _faces.Count; i++)
        {
            List<int> neigbors = FindAllNeighborsOfAccordingIndex(i);
            var face = _faces[i];
            face.neighbors = neigbors;
            _faces[i] = face;
        }
    }
    
    
    // A grid is being created from planes which are brought together
    // And these planes are created from 4 triangles.
    // This function finds all neighbors of the triangles from the given index.
    // As a plane consists of 4 trinangles there are 4 different cases. The rest repeats itself so we can use modulo
    //  -------
    // | \ 1  / |   This represents how a plane is divided in 4 triangles. 
    // |  \ /   |   The numbers represent the modulos of a plane
    // | 0/ \2  |   According to this the index 0 has the neighbors 1, 3
    // | / 3 \  |   0 will also have a neighbor on his left side which will be -2 of the index at 0
    // --------     
    // The other formulas where created in the same way.
    // As this is an area where errors can occur easily there are unit tests in EditModeTests/neighbors.cs 
    public List<int> FindAllNeighborsOfAccordingIndex(int i)
    {
        List<int> neighbors = new List<int>();
        
        if (i % 4 == 0)
        {
            neighbors.Add(i+1);
            neighbors.Add(i+3);

            if (i - 2 >= 0 && i % (_gridSize * 4) != 0 )
            {
                neighbors.Add(i-2);
            }
        }

        else if(i % 4 == 1)
        {
            neighbors.Add(i-1);
            neighbors.Add(i+1);

            if ( i > 1 + ((_gridSize - 1) * 4))
            {
                neighbors.Add(i-((4 * (_gridSize-1)))-2);
            }
        }

        else if(i % 4 == 2)
        {
            neighbors.Add(i-1);
            neighbors.Add(i+1);

            if ((i - ((4 * (_gridSize-1)) + 2)) % (_gridSize * 4) != 0)
            {
                neighbors.Add(i+2);
            }
        }
        else
        {
            neighbors.Add(i-3);
            neighbors.Add(i-1);
            if (i < 4 * _gridSize * (_gridSize - 1) + 3 )
            {
                neighbors.Add(i+((4 * (_gridSize-1))) +2 );
            }
        }

        return neighbors;
    }

    // This function divides a number into random smaller numbers so that the sum of the divided numbers doesn't change
    // For Ex.  20 ->  5,11,4
    private void CalculatePossibleSizesOfNewPieces()
    {
        _pieceSizes = new List<int>();
        
        int trianglesAmount = _gridSize * _gridSize * 4;
        
        // We substract the piece amount so that every piece will have a size of minimum 1.
        // This is added inside the loop for every piece again as + 1.
        trianglesAmount -= _pieceAmount;
        
        for (int i = 0; i < _pieceAmount; i++)
        {
            // If its the last piece add the remainig amount + 1 which we had substracted before.
            if (i == _pieceAmount - 1)
            {
                _pieceSizes.Add(trianglesAmount+1);
                break;
            }
            
            // Get a random number from 0 - amount/3 so that the pieces are not to big.
            int randomNumber = Random.Range(0, Mathf.Clamp(trianglesAmount/3, 0, trianglesAmount)) + 1;
            _pieceSizes.Add(randomNumber);
            trianglesAmount -= randomNumber;
        }
    }

    public void CreateMeshesOfAllPieces()
    {
        for (int i = 0; i < _createdPieces.Count; i++)
        {
            List<Vector3> verts = new List<Vector3>();
            List<int> tris = new List<int>();
            for (int j = 0; j < _createdPieces[i].vertices.Count; j++)
            {
                verts.AddRange( _faces[_createdPieces[i].vertices[j]].vertices );
            }
            for (int k = 0; k < _createdPieces[i].vertices.Count*3; k++)
            {
                tris.Add(k);
            }
           
            OnCreatePieces?.Invoke(verts.ToArray(), tris.ToArray(), _createdPieces[i]._anchorPoints, _createdPieces[i].color ,_parent);
        }
    }
}