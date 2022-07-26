using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator
{ 
    public List<Face> Faces { get; private set; }
    private Vector3[] _vertices;
    private int[] _triangles;
    private List<Vector3> _anchorPoints;

    private int _gridSize;
    private float _cellSize;
    
    public GridGenerator(int gridSize, float cellSize)
    {
        _gridSize = gridSize;
        _cellSize = cellSize;
    }
    
    public List<Vector3> GetAnchorPoints { get { return _anchorPoints; } }

    // Create a plane according to the grid size.
    // If the grid size is For Ex 3x3 there will be 9 planes.
    // Each of these planes consists of 4 triangles.
    // The triangles are not sharing any vertices as this will make it easier to create pieces out of it.
    // A plane is divided like the example below.
    // The grid size decides how many of these planes are created verticallly and horzinontally
    //  ---------
    // | \ 1  / |    
    // |  \ /   |  . .
    // | 0/ \2  |    
    // | / 3 \  |   
    //  -------- 
    //      .      .
    //      .        .
    public void CreateProceduralGrid()
      {
          Faces = new List<Face>();
          _vertices = new Vector3[_gridSize * _gridSize * 12];
          _triangles = new int[_gridSize * _gridSize * 12];
          _anchorPoints = new List<Vector3>();

          int vertexIndex = 0;
          float vertexOffset = _cellSize * 0.5f;
  
          // So that the grid boards pivot point is always at the center
          float startPosition = vertexOffset * (_gridSize - 1);
  
          // Create Grid according to size
          for (int y = 0; y < _gridSize; y++)
          {
              for (int x = 0; x < _gridSize; x++)
              {
                  // Creates 4 triangles and adds them together to create a plane. 
                  
                  // To calculate the position of the created plane so that they dont overlapp.
                  Vector3 cellOffset = new Vector3(x * _cellSize, -y * _cellSize, 0);
                  
                  _vertices[vertexIndex]      = new Vector3(-startPosition,startPosition, 0) + cellOffset;
                  _vertices[vertexIndex + 1]  = new Vector3(-startPosition-vertexOffset, startPosition-vertexOffset, 0) + cellOffset;
                  _vertices[vertexIndex + 2]  = new Vector3(-startPosition-vertexOffset,  startPosition + vertexOffset, 0) + cellOffset;
                 
                  Faces.Add(new Face(new []{_vertices[vertexIndex], _vertices[vertexIndex + 1]  ,_vertices[vertexIndex + 2]}));
  
                  _vertices[vertexIndex + 3]  = new Vector3(-startPosition,startPosition, 0) + cellOffset;
                  _vertices[vertexIndex + 4]  = new Vector3(-startPosition-vertexOffset, startPosition+vertexOffset, 0) + cellOffset;
                  _vertices[vertexIndex + 5]  = new Vector3( -startPosition+vertexOffset,  startPosition+vertexOffset, 0) + cellOffset;
                  
                  Faces.Add(new Face(new []{_vertices[vertexIndex + 3], _vertices[vertexIndex + 4]  ,_vertices[vertexIndex + 5]}));
                  
                  _vertices[vertexIndex + 6]  = new Vector3(-startPosition,startPosition, 0) + cellOffset;
                  _vertices[vertexIndex + 7]  = new Vector3(-startPosition+vertexOffset,   startPosition+vertexOffset, 0) + cellOffset;
                  _vertices[vertexIndex + 8]  = new Vector3(-startPosition+vertexOffset,  startPosition-vertexOffset, 0) + cellOffset;
                  
                  Faces.Add(new Face(new []{_vertices[vertexIndex + 6], _vertices[vertexIndex + 7]  ,_vertices[vertexIndex + 8]}));
                  
                  _vertices[vertexIndex + 9]  = new Vector3( -startPosition,startPosition, 0) + cellOffset;
                  _vertices[vertexIndex + 10] = new Vector3( -startPosition+vertexOffset, startPosition-vertexOffset, 0) + cellOffset;
                  _vertices[vertexIndex + 11] = new Vector3(-startPosition-vertexOffset, startPosition-vertexOffset, 0) + cellOffset;
                  
                  Faces.Add(new Face(new []{_vertices[vertexIndex + 9], _vertices[vertexIndex + 10]  ,_vertices[vertexIndex + 11]}));
                    
                  _anchorPoints.Add(_vertices[vertexIndex]);
                  vertexIndex += 12;
              }
          }
          
          // As the vertives of the triangles are not connected the triangle order will go from 0 - count of triangles 
          for (int i = 0; i < _triangles.Length; i++)
          {
              _triangles[i] = i;
          }
      }
        public void CreateMesh(Mesh mesh, MeshRenderer meshRenderer, Transform parent)
        {
            mesh.Clear();
            mesh.vertices = _vertices;
            mesh.triangles = _triangles;
            meshRenderer.material.color = Color.gray;
            meshRenderer.gameObject.transform.SetParent(parent);
                
            mesh.RecalculateNormals();
        }
}
