using System;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Piece : MonoBehaviour
{
    private IObjectPool<Piece> _pool;
    
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public MeshCollider meshCollider;

    public void SetPool(IObjectPool<Piece> objectPool) => _pool = objectPool;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
    }

    private void OnEnable()
    {
        ProceduralGridGeneration.ResetPiecesAndAnchorPoints += ResetPiecesAndPiece;
    }

    private void OnDisable()
    {
        ProceduralGridGeneration.ResetPiecesAndAnchorPoints -= ResetPiecesAndPiece;
    }

    private void ResetPiecesAndPiece()
    {
        meshFilter.mesh = null;
        _pool.Release(this);
    }
}