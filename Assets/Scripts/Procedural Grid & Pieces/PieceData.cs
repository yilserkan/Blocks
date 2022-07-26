using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PieceData
{
    public List<int> vertices;
    public List<int> possibleNeighbors { get; set; }

    public List<Vector3> _anchorPoints;

    public Color color;
    public PieceData(List<int> verts, List<int> neighbors, List<Vector3> anchorPoints, Color color)
    {
        vertices = new List<int>();
        foreach (var vert in verts)
        {
            vertices.Add(vert);
        }

        possibleNeighbors = new List<int>();
        foreach (var neighbor in neighbors)
        {
            possibleNeighbors.Add(neighbor);
        }

        _anchorPoints = new List<Vector3>();
        foreach (var point in anchorPoints)
        {
            _anchorPoints.Add(point);
        }

        this.color = color;
    }
}