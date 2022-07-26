using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PiecesAnimationHandler : MonoBehaviour
{
    [Header("Parent Transform")]
    [SerializeField] private Transform piecesParent;
    
    [Header("Lerp")]
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float lerpDuration;

    [Header("Piece Landing Position")]
    [SerializeField] private Vector2 landingPosition;
    [SerializeField] private Vector2 landingPositionOffset;
    
    public void PlayStartAnimation()
    {
        foreach (Transform child in piecesParent)
        {
            // Object pool is used some objects can be disabled
            if (child.gameObject.activeSelf)
            {
                child.position = GetRandomPosition(child.position, new Vector3(1.5f,1,0) );
                StartCoroutine(Lerp(child));
            }
        }
    }
    
    IEnumerator Lerp(Transform childTransform)
    {
        float timeElapsed = 0;
        Vector3 startPosition = new Vector3();
        
        // Gets the first active anchor point of the piece. Object pool is used some objects can be disabled
        for (int i = 0; i < childTransform.childCount; i++)
        {
            if (childTransform.GetChild(i).gameObject.activeSelf)
            {
                startPosition = childTransform.GetChild(i).position;
                break;
            }
        }
        
        // The lerp method will lerp the position of the anchor points and add the distance from (nextpos - prevpos) to the pieces transform
        Vector3 prevPos = new Vector3(startPosition.x, startPosition.y, childTransform.position.z);
        Vector3 nexPos;
        
        Vector2 endPosition = GetRandomPosition(landingPosition, landingPositionOffset);
        
        while (timeElapsed < lerpDuration)
        {
            float xPos = Mathf.Lerp(startPosition.x, endPosition.x,  animationCurve.Evaluate(timeElapsed / lerpDuration));
            float yPos = Mathf.Lerp(startPosition.y, endPosition.y,  animationCurve.Evaluate(timeElapsed / lerpDuration));
            
            nexPos = new Vector3(xPos, yPos, childTransform.position.z);
            
            // Add the moved distance of nextpos to prevpos to the transform to move the piece
            childTransform.position += (nexPos - prevPos);
            
            timeElapsed += Time.deltaTime;
            prevPos = nexPos;
            yield return null;
        }
        
    }

    private Vector3 GetRandomPosition(Vector3 position, Vector3 offset)
    {
        float randomX = Random.Range(position.x - offset.x,
            position.x + offset.x);
        float randomY = Random.Range(position.y - offset.y,
            position.y + offset.y);

        return new Vector3(randomX, randomY, position.z);
    }
}
