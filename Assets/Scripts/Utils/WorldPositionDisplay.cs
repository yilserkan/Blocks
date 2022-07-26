using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

public static class WorldPositionDisplay
{
    // Isn't used in the game. Helps to print the global position of the selected object.
    // Function can be find under Debug/Print Global Position 
    
    [MenuItem(("Debug/Prin Global Position"))]
    public static void PrintGlobalPosition()
    {
        if (Selection.activeGameObject != null)
        {
            Debug.Log(Selection.activeGameObject.name + "is at" + Selection.activeGameObject.transform.position);
        }
    }

}
