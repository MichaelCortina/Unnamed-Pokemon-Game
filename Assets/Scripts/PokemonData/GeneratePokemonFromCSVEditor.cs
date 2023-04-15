using System.Collections;
using System.Collections.Generic;
using PokemonData;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GeneratePokemonFromCSV))]
public class GeneratePokemonFromCSVEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        if (GUILayout.Button("Generate Pokemon"))
        {
            var currentObject = (GeneratePokemonFromCSV) target;
            
            currentObject.GenerateScriptableObjects();
        }
    }
}
