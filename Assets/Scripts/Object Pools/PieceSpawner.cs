using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;


public class PieceSpawner : MonoBehaviour
{
    [SerializeField] private Piece piecePrefab;
    
    private ObjectPool<Piece> _objectPool;
    
    public static event Action<List<Vector3>, Transform ,bool> CreateAnchorPoints;

    private bool anchorPointVisible = false;

    private void Awake() => _objectPool = new ObjectPool<Piece>(
        CreatePiece,
        OnGetPiece,
        OnReleasePiece,
        OnDestroyPiece,
        false,
        16,
        16
        );
   
    private void Start()
    {
        ProceduralPieceGenerator.OnCreatePieces += HandleOnCreatePiece;
    }

    private void OnDestroy()
    {
        ProceduralPieceGenerator.OnCreatePieces -= HandleOnCreatePiece;
    }

    private void HandleOnCreatePiece(Vector3[] verts, int[] tris, List<Vector3> anchorPoints, Color color ,Transform parent)
    {
        Piece piece = _objectPool.Get();
        piece.meshFilter.mesh.vertices = verts;
        piece.meshFilter.mesh.triangles = tris;
        piece.meshRenderer.material.color = color;
        piece.meshCollider.sharedMesh = piece.meshFilter.mesh;
        
        
        if (parent != null)
        {
            piece.transform.SetParent(parent,false);
            piece.transform.position = parent.position;
        }
        
        CreateAnchorPoints?.Invoke(anchorPoints, piece.transform, anchorPointVisible);
    }
    
    private Piece CreatePiece()
    {
        Piece piece = Instantiate(piecePrefab);
        piece.SetPool(_objectPool);
        return piece;
    }

    private void OnGetPiece(Piece piece)
    {
        piece.gameObject.SetActive(true);
    }

    private void OnReleasePiece(Piece piece)
    {
        piece.gameObject.SetActive(false);
    }

    private void OnDestroyPiece(Piece piece)
    {
        Destroy(piece);
    }
    
}