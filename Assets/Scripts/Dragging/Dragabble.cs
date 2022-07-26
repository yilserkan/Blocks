using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragabble : MonoBehaviour
{
    private Camera _camera;
    private DragController _dragController;
    
    // Drag offset = Distance ( dragged Piece , Mouse click position on the piece )
    // Ensures that the pieces is moving from the place where it is clicked on
    private Vector3 _dragOffset;
    
    // If the piece is placed on the grid this list will hold the indexes 
    // of the anchor points it is placed on.
    private List<int> _placedGridAnchorPointIndexes;

    private bool _isPlaced = false;
    private bool _canDrag = true;
    
    // This functions checks if the child at the index is active.
    // The childs are the anchor points of the piece.
    // As i am using object pools to create anchor points, the pieces may have disabled anchor points.
    private bool IsActive(int index) => transform.GetChild(index).gameObject.activeSelf;

    private float zIndex;
    
    private void Awake()
    {
        _camera = Camera.main;
        _dragController = FindObjectOfType<DragController>();
    }

    private void OnEnable()
    {
        Grid.LevelSolved += HandleLevelSolved;
    }

    private void OnDisable()
    {
        Grid.LevelSolved -= HandleLevelSolved;
    }
    
    private void OnMouseDown()
    {
        if (!_canDrag) { return; }
        
        SetDragOffset();
        
        BringPieceToFront();
        
        ShowAnchorPoints();
        
        if (_isPlaced)
        {
            _dragController.RemoveFromGrid(_placedGridAnchorPointIndexes);
            _isPlaced = false;
        }
    }

    private void OnMouseDrag()
    {
        if (!_canDrag) { return; }
        
        UpdateDraggedPiecePosition();
    }


    private void OnMouseUp()
    {
        if (!_canDrag) { return; }
        
        HideAnchorPoints();
        TryPlacingPiece();
    }
    
    public void InitializePlacedPiecesIndex()
    {
        _placedGridAnchorPointIndexes = new List<int>();
        _dragController.InitializeParams();
    }

    #region OnMouseDown
    
    private void SetDragOffset()
    {
        _dragOffset = transform.position - GetMousePos();
    }

    private void BringPieceToFront()
    {
        Vector3 pos = transform.position;
        zIndex = _dragController.GetSelectedZIndex();
        
        transform.position =
            new Vector3(pos.x, pos.y, zIndex);
    }
    
    private void ShowAnchorPoints()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (IsActive(i))
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
    #endregion

    #region OnMouseDrag

    private void UpdateDraggedPiecePosition()
    {
        Vector2 movedPosition = GetMousePos() + _dragOffset;
        transform.position = new Vector3(movedPosition.x, movedPosition.y, zIndex) ;
    }


    #endregion

    #region OnMouseUp

     private void TryPlacingPiece()
    {
        // Anchor points of the grid
        List<Vector3> gridPoints = _dragController.GetGridAnchorPoints();
        _placedGridAnchorPointIndexes.Clear();
        
        // Overlapp count is the count of anchor points,
        // where the grid anchor points and piece anchor points are overlapping
        int overlappCount = 0;
        
        // This is the count of anchor points the piece has. 
        // The pieces can have disabled anchor points as i am using object pools to create them
        int activeChildCount = 0;

        // These variables hold the first overlapping anchor points.
        // If all anchor points of the piece are overlapping,
        // the piece will be placed in the grid according to the distance of these anchor points.
        bool anchorPointsInitialized = false;
        Vector3 gridAnchorPoint = new Vector3();
        Vector3 pieceAnchorPoint = new Vector3();
        
        for (int i = 0; i < transform.childCount; i++)
        {
            if (IsActive(i))
            {
                activeChildCount++;
                
                for (int j = 0; j < gridPoints.Count; j++)
                {
                    
                    if ( PositionsOverlapp(transform.GetChild(i).position, gridPoints[j]))
                    {
                        // Sets the first overlapping anchor points.
                        if (!anchorPointsInitialized)
                        {
                            gridAnchorPoint = gridPoints[j];
                            pieceAnchorPoint = transform.GetChild(i).position;
                            
                            anchorPointsInitialized = true;
                        }
                        
                        // Add overlapping point ti list
                        _placedGridAnchorPointIndexes.Add(j);
                        overlappCount++;
                        break;
                    }
                
                }
            }
        }
        
        // If all points overlapp place piece in grid
        if (overlappCount == activeChildCount)
        {
            PlacePieceInPosition(gridAnchorPoint, pieceAnchorPoint);
            _dragController.PlacePieceInGrid(_placedGridAnchorPointIndexes);
            _isPlaced = true;
        }
    }

    // Calculate the distance from the anchor points which were initalized in TryPlacingPiece function
    // Set piece position according to the distance of the anchor points.
    private void PlacePieceInPosition(Vector3 gridAnchorPos, Vector3 pieceAnchorPos)
    {
        Vector3 placeOffset = pieceAnchorPos - gridAnchorPos;
        placeOffset.z = 0;
        transform.position -= placeOffset;
        
    }

    // If the distance of the points are smaller than .5f return true.
    private bool PositionsOverlapp(Vector3 pointA, Vector3 pointB)
    {
        pointA.z = 0;
        pointB.z = 0;

        if (Vector3.Distance(pointA, pointB) < .5f)
        {
            return true;
        }

        return false;
    }

    private Vector3 GetMousePos()
    {
        Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        return mousePos;
    }



    private void HideAnchorPoints()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (IsActive(i))
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    #endregion
    
    // To prevent an error which occurs when you drag a piece from the animation after you solved a level 
    private void HandleLevelSolved()
    {
        _canDrag = false;
        
        Invoke(nameof(EnableDragging), 2f);
    }

    private void EnableDragging()
    {
        _canDrag = true;
    }

}
