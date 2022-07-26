using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    // The List holds the amount of points which are being placed on the anchor points.
    // If there are 4 points placed in an anchor point it will be marked as complete in isComplete array.
    // It does need 4 points because an anchor point is placed in the middle of every plane,
    // And every plane is created from 4 triangles.
    // So basically there must be 4 vertices overlapping wit the anchor point so that an anchor point is filled.
    // If all anchor points have 4 points the level is solved correctly.
    // This List only holds the information of how many times an anchor point has being placed upon.
    private List<List<int>> placedPoints;
    
    // This array holds the information if the anchor point of the grid is complete.
    // If all values are true the level is solved.
    private bool[] isComplete;

    private LevelManager _levelManager;
    
    public bool ignoreDragging { get; set; }

    public static event Action LevelSolved;

    private void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        InitializeParams();
    }

    // Initializes  placedPoints and isComplete with the size of anchorPoints of the grid.
    public void InitializeParams()
    {
        int arraySize  = GetGridAnchorPoints().Count;
      
        isComplete = new bool[arraySize];
        placedPoints = new List<List<int>>();
        
        for (int i = 0; i < arraySize; i++)
        {
            isComplete[i] = false;
            List<int> emptyList = new List<int>();
            placedPoints.Add(emptyList);
        }
    }
    
    // Returns the anchor Point count of the grid.
    public List<Vector3> GetGridAnchorPoints()
    {
        List<Vector3> anchorPoints = new List<Vector3>();

        for (int i = 0; i < transform.childCount; i++)
        {   
            // Object pooling is used so there can be disabled objects.
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                anchorPoints.Add(transform.GetChild(i).position);
            }
        }

        return anchorPoints;
    }


    // This function adds the given indexes of placedpoint list and checks if the grid is complete.
    public void PlaceInGrid(List<int> placedAnchorPointIndexes)
    {
     
        if (placedAnchorPointIndexes.Count == 0) { return; }
        
        for (int i = 0; i < placedAnchorPointIndexes.Count; i++)
        {
            placedPoints[placedAnchorPointIndexes[i]].Add(new int());
            // AddToIsCompleteArray(placedAnchorPointIndexes[i]);
            UpdateIsCompleteArray();
        }
        
        CheckIfGridCompleted();
    }

    // This function removes the given indexes of placedpoint list.
    public void RemoveFromGrid(List<int> placedAnchorPointIndexes)
    {
        if (placedAnchorPointIndexes.Count == 0) { return; }
        
        for (int i = 0; i < placedAnchorPointIndexes.Count; i++)
        {
            if ( placedPoints[placedAnchorPointIndexes[i]].Count > 0)
            {
                placedPoints[placedAnchorPointIndexes[i]].Remove(0);
                // RemoveFromIsCompleteArray(placedAnchorPointIndexes[i]);
                UpdateIsCompleteArray();
            }
        }
    }
    
    // Update Is Complete according to the placed points list.
    // If there are more than 4 points in an anchor point mark is placed as true.
    private void UpdateIsCompleteArray()
    {
        for (int i = 0; i < placedPoints.Count; i++)
        {
            if (placedPoints[i].Count >=4 )
            {
                // Debug.Log($"Completed -> {index}");
                isComplete[i] = true;
            }
            
            if (placedPoints[i].Count < 4 )
            {
                isComplete[i] = false;
            }
        }
    }
    
    // If all values if the isComplete array are true the level is solved
    private void CheckIfGridCompleted()
    {
        // Dont chech if ignore dragging is true. We won't call this function in Generete Level mode
        if (ignoreDragging)
        {
            return;
        }
        
        int gridPlacedPointsCount = 0;
        
        // If there is a false value in the is complete array return
        for (int i = 0; i < isComplete.Length; i++)
        {
            if (!isComplete[i])
            {
                return;
            }
        }
        
        // Clear lists and call next level
        placedPoints.Clear();
        _levelManager.OpenNextLevel();
        LevelSolved?.Invoke();
    }
}
