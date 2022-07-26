using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class neighbor
{
    [Test]
    public void with_index_0_in_4x4grid_are_found_correctly()
    {
        ProceduralPieceGenerator proceduralPieceGenerator = new ProceduralPieceGenerator()
            { GridSize = 4, CellSize = 1, PieceAmount = 5 };

        List<int> testNeighbors = proceduralPieceGenerator.FindAllNeighborsOfAccordingIndex(0);

        List<int> correctNeighbors = new List<int>(){1,3};
 
        Assert.AreEqual(correctNeighbors, testNeighbors);

    }

    [Test]
    public void with_index_14_in_4x4grid_are_found_correctly()
    {
        ProceduralPieceGenerator proceduralPieceGenerator = new ProceduralPieceGenerator()
            { GridSize = 4, CellSize = 1, PieceAmount = 5 };

        List<int> testNeighbors = proceduralPieceGenerator.FindAllNeighborsOfAccordingIndex(14);

        List<int> correctNeighbors = new List<int>(){13,15};
 
        Assert.AreEqual(correctNeighbors, testNeighbors);

    }
    
    [Test]
    public void with_index_55_in_4x4grid_are_found_correctly()
    {
        ProceduralPieceGenerator proceduralPieceGenerator = new ProceduralPieceGenerator()
            { GridSize = 4, CellSize = 1, PieceAmount = 5 };

        List<int> testNeighbors = proceduralPieceGenerator.FindAllNeighborsOfAccordingIndex(55);

        List<int> correctNeighbors = new List<int>(){52,54};
 
        Assert.AreEqual(correctNeighbors, testNeighbors);

    }
    
    [Test]
    public void with_index_41_in_4x4grid_are_found_correctly()
    {
        ProceduralPieceGenerator proceduralPieceGenerator = new ProceduralPieceGenerator()
            { GridSize = 4, CellSize = 1, PieceAmount = 5 };

        List<int> testNeighbors = proceduralPieceGenerator.FindAllNeighborsOfAccordingIndex(41);

        List<int> correctNeighbors = new List<int>(){40,42,27};
 
        Assert.AreEqual(correctNeighbors, testNeighbors);

    }
    
    [Test]
    public void with_index_9_in_4x4grid_are_found_correctly()
    {
        ProceduralPieceGenerator proceduralPieceGenerator = new ProceduralPieceGenerator()
            { GridSize = 4, CellSize = 1, PieceAmount = 5 };

        List<int> testNeighbors = proceduralPieceGenerator.FindAllNeighborsOfAccordingIndex(9);

        List<int> correctNeighbors = new List<int>(){8,10};
 
        Assert.AreEqual(correctNeighbors, testNeighbors);

    }
    
    [Test]
    public void with_index_16_in_5x5grid_are_found_correctly()
    {
        ProceduralPieceGenerator proceduralPieceGenerator = new ProceduralPieceGenerator()
            { GridSize = 5, CellSize = 1, PieceAmount = 5 };

        List<int> testNeighbors = proceduralPieceGenerator.FindAllNeighborsOfAccordingIndex(16);

        List<int> correctNeighbors = new List<int>(){17,19,14};
 
        Assert.AreEqual(correctNeighbors, testNeighbors);

    }
    
    [Test]
    public void with_index_20_in_5x5grid_are_found_correctly()
    {
        ProceduralPieceGenerator proceduralPieceGenerator = new ProceduralPieceGenerator()
            { GridSize = 5, CellSize = 1, PieceAmount = 5 };

        List<int> testNeighbors = proceduralPieceGenerator.FindAllNeighborsOfAccordingIndex(20);

        List<int> correctNeighbors = new List<int>(){21,23};
 
        Assert.AreEqual(correctNeighbors, testNeighbors);

    }
    
    [Test]
    public void with_index_58_in_5x5grid_are_found_correctly()
    {
        ProceduralPieceGenerator proceduralPieceGenerator = new ProceduralPieceGenerator()
            { GridSize = 5, CellSize = 1, PieceAmount = 5 };

        List<int> testNeighbors = proceduralPieceGenerator.FindAllNeighborsOfAccordingIndex(58);

        List<int> correctNeighbors = new List<int>(){57,59};
 
        Assert.AreEqual(correctNeighbors, testNeighbors);

    }
    
    [Test]
    public void with_index_24_in_6x6grid_are_found_correctly()
    {
        ProceduralPieceGenerator proceduralPieceGenerator = new ProceduralPieceGenerator()
            { GridSize = 6, CellSize = 1, PieceAmount = 5 };

        List<int> testNeighbors = proceduralPieceGenerator.FindAllNeighborsOfAccordingIndex(24);

        List<int> correctNeighbors = new List<int>(){25,27};
 
        Assert.AreEqual(correctNeighbors, testNeighbors);

    }
    
    [Test]
    public void with_index_5_in_6x6grid_are_found_correctly()
    {
        ProceduralPieceGenerator proceduralPieceGenerator = new ProceduralPieceGenerator()
            { GridSize = 6, CellSize = 1, PieceAmount = 5 };

        List<int> testNeighbors = proceduralPieceGenerator.FindAllNeighborsOfAccordingIndex(5);

        List<int> correctNeighbors = new List<int>(){4,6};
 
        Assert.AreEqual(correctNeighbors, testNeighbors);

    }
    
    [Test]
    public void with_index_143_in_6x6grid_are_found_correctly()
    {
        ProceduralPieceGenerator proceduralPieceGenerator = new ProceduralPieceGenerator()
            { GridSize = 6, CellSize = 1, PieceAmount = 5 };

        List<int> testNeighbors = proceduralPieceGenerator.FindAllNeighborsOfAccordingIndex(143);

        List<int> correctNeighbors = new List<int>(){140,142};
 
        Assert.AreEqual(correctNeighbors, testNeighbors);

    }
    
}
