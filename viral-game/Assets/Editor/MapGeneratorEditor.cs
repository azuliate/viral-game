using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CustomEditor (typeof (MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGen = (MapGenerator)target;

        if (DrawDefaultInspector()) 
        {
            if(mapGen.autoUpdate){
                mapGen.GenerateMap();
            }
        }

        if(GUILayout.Button("Generate"))
        {
            mapGen.GenerateMap();
        }
    }
}
