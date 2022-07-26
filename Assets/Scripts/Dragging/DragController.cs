using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    // This will be false in Level Generat mode.
    // When it is false the the Grid script wont try to load next the next level
    // after each piece is placed successfully.
    [SerializeField] public  bool ignoreDragging = false;
    
    public  float selectedZIndex = -2.2f;

    private  Grid _grid;
    
    void Update()
    {
        if (_grid == null)
        {
            _grid = FindObjectOfType<Grid>();
            if (_grid != null)
            {
                _grid.ignoreDragging = ignoreDragging;
            }
        }
    }
    

    public  List<Vector3> GetGridAnchorPoints()
    {
        return _grid.GetGridAnchorPoints();
    }

    // Decrease the zindex and return the value.
    // So that the dragged piece will be in front of all pieces
    public  float GetSelectedZIndex()
    {
        selectedZIndex -= .005f;
        return selectedZIndex;
    }

    // Resets ZIndex Value is called after each level.
    private void ResetSelectedIndex()
    {
        selectedZIndex = -2.2f;
    }

    public  void PlacePieceInGrid(List<int> anchorPoints)
    {
        _grid.PlaceInGrid(anchorPoints);
    }
    
    public  void RemoveFromGrid(List<int> anchorPoints)
    {
        _grid.RemoveFromGrid(anchorPoints);
    }

    public void InitializeParams()
    {
        if (_grid != null)
        {
            ResetSelectedIndex();
            _grid.InitializeParams();
        }
    }
}
