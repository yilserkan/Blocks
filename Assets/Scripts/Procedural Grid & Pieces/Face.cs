using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Face
{
    public Vector3[] vertices { get; private set; }
    
    public List<int> neighbors { get;  set; }
    
    public bool isVisited { get; set; }
    public Face(Vector3[] vertices)
    {
        this.vertices = vertices;
        neighbors = new List<int>();
        isVisited = false;
    }
}