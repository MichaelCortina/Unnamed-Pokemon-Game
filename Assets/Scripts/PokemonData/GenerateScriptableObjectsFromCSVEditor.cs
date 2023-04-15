using System.Collections;
using System.Collections.Generic;
using PokemonData;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GenerateScriptableObjectsFromCSV))]
public class GenerateScriptableObjectsFromCSVEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        if (GUILayout.Button("Generate Scriptable Objects"))
        {
            var currentObject = (GenerateScriptableObjectsFromCSV) target;
            
            currentObject.GenerateScriptableObjects();
        }
    }
}
