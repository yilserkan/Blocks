using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AnchorPoint : MonoBehaviour
{
    public bool IsPlaced { get; set; }
    
    private IObjectPool<AnchorPoint> _pool;

    public void SetPool(IObjectPool<AnchorPoint> objectPool) => _pool = objectPool;

    private void OnEnable()
    {
        ProceduralGridGeneration.ResetPiecesAndAnchorPoints += ResetPiecesAndAnchorPoints;
    }
    
    private void OnDisable()
    {
        ProceduralGridGeneration.ResetPiecesAndAnchorPoints -= ResetPiecesAndAnchorPoints;
    }

    private void ResetPiecesAndAnchorPoints()
    {
        _pool.Release(this);
    }

}