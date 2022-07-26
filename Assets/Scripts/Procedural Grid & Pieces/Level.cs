using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
    // This class is used so to save the level information in the JSON file.
    // From these information the levels can be created.
    
    public int gridSize;
    public List<PieceData> pieces;

    public Level(int gridSize, List<PieceData> pieces)
    {
        this.gridSize = gridSize;
        this.pieces = pieces;
    }
}
