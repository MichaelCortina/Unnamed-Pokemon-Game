using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace PokemonData
{
    
    /// <summary>
    /// Given the path to a csv file <c>dataFilePath</c> containing a list of
    /// data for various pokemon, this script will generate a PokemonData ScriptableObject
    /// for each pokemon listed in the file. The PokemonData Objects will be created if
    /// one does not already exist, if an object with the provided name already exists,
    /// the data in that object will instead be overriden. The outputted PokemonData will
    /// be generated in the folder <c>outputDirectoryPath</c>.
    /// <br/><br/>
    /// To generate the PokemonData use the custom editor button on the script during edit
    /// mode.
    /// </summary>
    [CreateAssetMenu]
    public class GeneratePokemonFromCSV : ScriptableObject
    {
        [SerializeField] private string dataFilePath;
        [SerializeField] private string outputDirectoryPath;
        [SerializeField] private int nameIndex = 2;

#nullable enable
        [ContextMenu("Generate Pokemon")]
        public void GenerateScriptableObjects()
        {
            string[] lines = File.ReadAllLines(dataFilePath);
            var fields = typeof(PokemonData).GetRuntimeFields();
            
            for (int i = 1; i < lines.Length; i++)
            {
                string[] splitLine = lines[i].Split(',');
                string filename = splitLine[nameIndex];

                using StreamWriter writer = new(
                    $"{outputDirectoryPath}/{filename}.asset");

                writer.Write(
                    "%YAML 1.1                                                                          \n" +
                    "%TAG !u! tag:unity3d.com,2011:                                                     \n" +
                    "--- !u!114 &11400000                                                               \n" +
                    "MonoBehaviour:                                                                     \n" +
                    "  m_ObjectHideFlags: 0                                                             \n" +
                    "  m_CorrespondingSourceObject: {fileID: 0}                                         \n" +
                    "  m_PrefabInstance: {fileID: 0}                                                    \n" +
                    "  m_PrefabAsset: {fileID: 0}                                                       \n" +
                    "  m_GameObject: {fileID: 0}                                                        \n" +
                    "  m_Enabled: 1                                                                     \n" +
                    "  m_EditorHideFlags: 0                                                             \n" +
                    "  m_Script: {fileID: 11500000, guid: 0c98e410bef84e189121b23a02321f9e, type: 3}    \n" +
                   $"  m_Name: {filename}                                                               \n" +
                    "  m_EditorClassIdentifier:                                                         \n");

                int j = 0;
                foreach (FieldInfo field in fields)
                {
                    if (j == nameIndex) j++; //skip index containing filename
                    if (field.Name[0] == '_') continue; //skip all backing variables
                    writer.Write($"  {field.Name}: {splitLine[j++]} \n");
                }
            }
            
        }
    }
}

