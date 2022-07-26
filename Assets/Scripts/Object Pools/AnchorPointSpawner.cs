using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Pool;

public class AnchorPointSpawner : MonoBehaviour
{
    [SerializeField] private AnchorPoint anchorPointImagePrefab;
    
    private ObjectPool<AnchorPoint> _objectPool;

    private void Awake() => _objectPool = new ObjectPool<AnchorPoint>(
        CreateAnchorPoint,
        OnGetAnchorPoint,
        OnReleaseAnchorPoint,
        OnDestroyAnchorPoint,
        false,
        64,
        64);

    private void Start()
    {
        ProceduralGridGeneration.CreateAnchorPoints += HandleOnCreateAnchorPoints;
        PieceSpawner.CreateAnchorPoints += HandleOnCreateAnchorPoints;
    }

    private void OnDestroy()
    {
        ProceduralGridGeneration.CreateAnchorPoints -= HandleOnCreateAnchorPoints;
        PieceSpawner.CreateAnchorPoints -= HandleOnCreateAnchorPoints;
    }
    
    private void HandleOnCreateAnchorPoints(List<Vector3> anchorPoints, Transform parent, bool visible)
    {
        foreach (var point in anchorPoints)
        {
            AnchorPoint gameObject = _objectPool.Get();
            gameObject.transform.SetParent(parent ,false);
            gameObject.transform.position = point;
            gameObject.transform.position += Vector3.back + parent.position;
            
            //So that the anchor image doesn't show up on pieces unless you click on them
            gameObject.GetComponent<SpriteRenderer>().enabled = visible;
        }
    }

    private AnchorPoint CreateAnchorPoint()
    {
        AnchorPoint anchorPoint = Instantiate(anchorPointImagePrefab, transform);
        anchorPoint.SetPool(_objectPool);
        return anchorPoint;
    }

    private void OnGetAnchorPoint(AnchorPoint point)
    {
        point.gameObject.SetActive(true);
    }

    private void OnReleaseAnchorPoint(AnchorPoint point)
    {
        point.gameObject.SetActive(false);
    }

    private void OnDestroyAnchorPoint(AnchorPoint point)
    {
        Destroy(point);
    }
}
