using System.Collections.Generic;
using UnityEngine;

public class BFS
{
    // Created Pieces
    private List<PieceData> _createdPiecesData = new List<PieceData>();
    public List<PieceData> GetCreatedPiecesData { get { return _createdPiecesData; } }
    
    // Piece Data
    private List<int> _pieceSizes;
    private List<Face> _faces;
    private List<Vector3> _anchorPoints;
    
    
    //BFS Data
    private List<int> _visitedFaces;
    private List<int> _facesInQueue;
    
    // Visited array contains information of the visited nodes.
    // This is reseted after each BFS.
    private bool[] _visited;
    
    // Used array contains information if the node/face is already a part of a piece.
    // This list is not reseted.
    private bool[] _used;

    public BFS(List<Face> faces ,List<int> pieceSizes)
    {
        _faces = faces;
        _pieceSizes = pieceSizes;
    }


    public void CreatePiecesWithBFS()
    {
        //The pieces will be created with BFS
        CreateNewPieceMeshes();
        
        //Nodes which are unused are being added to the created pieces
        FindUnusedNodes();
    }
    
    public void CreateNewPieceMeshes()
    {
        InitializeVariables();
        
        int unusedNodeIndex = 0;

        foreach (var pieceSize in _pieceSizes)
        {
            BreadthFirstSearch(unusedNodeIndex, pieceSize);
            unusedNodeIndex = FindUnusedNodeIndex();
        }
    }

    
    private void BreadthFirstSearch(int index, int pieceSize)
    {
        //Reset previous filled List for new BFS
        ResetVisitedList();
        _facesInQueue.Clear();
        
        // Define first node
        _visited[index] = true;
        _facesInQueue.Add(index);
        
        // Algorithm will continue until the new piece has reached its max size or
        // there arent any faces left to reach
        int tempPieceSize = pieceSize;
        while (tempPieceSize > 0)
        {
            if (_facesInQueue.Count == 0)
            {
                break;
            }
            
            // int randomIndex =  Random.Range(0, _facesInQueue.Count);
            
            // The next face is being pulled from the queue
            int faceIndex = _facesInQueue[0];
            Face face = _faces[faceIndex];
            
            // The lists are refreshed.
            _visitedFaces.Add(faceIndex);
            _used[faceIndex] = true;
            _anchorPoints.Add(face.vertices[0]);
            _facesInQueue.Remove(faceIndex);

            // Unused and Unvisited neighbors of the face/node are being found and added to the queue
            List<int> neighbors = face.neighbors;
            foreach (var neighbor in neighbors)
            {
                if (!_visited[neighbor] && !_used[neighbor])
                {
                    _visited[neighbor] = true;
                    _facesInQueue.Add(neighbor);
                }
            }
            
            
            tempPieceSize--;
        }
        // The piece information from the BFS are being added.
        _createdPiecesData.Add(new PieceData(_visitedFaces, _facesInQueue, _anchorPoints, Random.ColorHSV()));

        //Lists are reseted for next BFS
        ResetVisitedList();
        _visitedFaces.Clear();
        _anchorPoints.Clear();
    }
    
    private int FindUnusedNodeIndex()
    {
        for (int i = 0; i < _used.Length; i++)
        {
            if (_used[i] == false)
            {
                return i;
            }
        }
        return -1;
    }

    // This function will find if there are any nodes left which couldn't be reached after the BFS
    public void FindUnusedNodes()
    {
        for (int i = 0; i < _used.Length; i++)
        {
            if (!_used[i])
            {
                FindPieceThatCanReachUnusedNode(i);
            }
        }
    }

    // This function will find the piece which can reach the node/face
    private void FindPieceThatCanReachUnusedNode(int index)
    {
        for (int j = 0; j < _createdPiecesData.Count; j++)
        {
            if (_createdPiecesData[j].possibleNeighbors.Contains(index))
            {
                AddMissingNodes(_createdPiecesData[j].vertices,_createdPiecesData[j]._anchorPoints, index);
                break;
            }
        } 
    }
    
    // The node will be added to the piece.
    // And it will be checked if the neighbors of the newly added node can reach unused node
    // This function is similar to the BFS with slight modifications
    private void AddMissingNodes(List<int> vertices,List<Vector3> anchorPoints, int startIndex)
    {
        // Reset Lists
        _anchorPoints.Clear();
        _facesInQueue.Clear();
        _visitedFaces.Clear();
        
        // Add starting value so the BFS can start
        _facesInQueue.Add(startIndex);
        
        // Check if there are any more nodes in queue
        while (_facesInQueue.Count > 0)
        {
            // The next face is being pulled from the queue
            int faceIndex = _facesInQueue[0];
            Face face = _faces[faceIndex];
            
            // The lists are refreshed.
            _visitedFaces.Add(faceIndex);
            _used[faceIndex] = true;
            _facesInQueue.Remove(faceIndex);
            _anchorPoints.Add(face.vertices[0]);

            // Unused and Unvisited neighbors of the face/node are being found and added to the queue
            List<int> neighbors = face.neighbors;
            foreach (var neighbor in neighbors)
            {
                if (!_visited[neighbor] && !_used[neighbor])
                {
                    _visited[neighbor] = true;
                    _facesInQueue.Add(neighbor);
                }
            }
        }
        // The vertices and anchor points of the new reached nodes are being added to the piece.
        vertices.AddRange(_visitedFaces);
        anchorPoints.AddRange(_anchorPoints);
    }

    private void InitializeVariables()
    {
        _visitedFaces = new List<int>();
        _visited = new bool[_faces.Count];
        _used = new bool[_faces.Count];
        _facesInQueue = new List<int>();
        _anchorPoints = new List<Vector3>();
    }
    
    private void ResetVisitedList()
    {
        for (int i = 0; i < _visited.Length; i++)
        {
            _visited[i] = false;
        }
    }
}