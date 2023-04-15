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
    public class GenerateScriptableObjectsFromCSV : ScriptableObject
    {
        // file path of the csv to read from 
        [SerializeField] private string dataFilePath;
        
        // file path of the directory to write to
        [SerializeField] private string outputDirectoryPath;
        
        // defines the name of the type to be created along with its namespace
        [SerializeField] private string namespaceQualifiedTypeName
            = "PokemonData.PokemonData";
        
        // to obtain the scriptData of a ScriptableObject create an instance of the 
        // object in the unity editor, then go into the .asset file of the newly created
        // scriptable object and copy the contents following m_Script:
        [SerializeField] private string scriptData
            = "{fileID: 11500000, guid: 0c98e410bef84e189121b23a02321f9e, type: 3}";
        
        // indicates which line should be used for the scriptable object file name
        [SerializeField] private int nameIndex = 2;
        
        /// <summary>
        /// creates a .asset file filled with data for each line from the provided
        /// CSV file at path <c>dataFilePath</c>, all backing fields of the scriptable
        /// object are left un-filled. as well as properties. All of the .asset files
        /// created will be placed in the directory specified by <c>outputDirectoryPath</c>,
        /// and the names of the files will be specified by the string at index
        /// <c>nameIndex</c> in the CSV. The type of the .asset files generated will be
        /// specified by the proper <c>namespaceQualifiedTypeName</c> and <c>scriptData</c>
        /// fields corresponding to the type of scriptable object to be created.
        /// </summary>
        [ContextMenu("Generate Scriptable Objects")]
        public void GenerateScriptableObjects()
        {
            string[] lines = File.ReadAllLines(dataFilePath);

            // this only works under the assumption the target script is in the 
            // same assembly. Likely for the current scale of the project but if
            // it becomes an issue in the future it can be changed accordingly -Big Mike
            var assembly = typeof(GenerateScriptableObjectsFromCSV).Assembly;
            var type = assembly.GetType(namespaceQualifiedTypeName);
            
            // get all fields in the target type to fill with data
            var fields = type.GetRuntimeFields();
            
            for (int i = 1; i < lines.Length; i++)
            {
                string[] splitLine = lines[i].Split(',');
                string filename = splitLine[nameIndex];

                using StreamWriter writer = new(
                    $"{outputDirectoryPath}/{filename}.asset");

                writer.Write(
                    "%YAML 1.1                                      \n" +
                    "%TAG !u! tag:unity3d.com,2011:                 \n" +
                    "--- !u!114 &11400000                           \n" +
                    "MonoBehaviour:                                 \n" +
                    "  m_ObjectHideFlags: 0                         \n" +
                    "  m_CorrespondingSourceObject: {fileID: 0}     \n" +
                    "  m_PrefabInstance: {fileID: 0}                \n" +
                    "  m_PrefabAsset: {fileID: 0}                   \n" +
                    "  m_GameObject: {fileID: 0}                    \n" +
                    "  m_Enabled: 1                                 \n" +
                    "  m_EditorHideFlags: 0                         \n" + 
                   $"  m_Script: {scriptData}                       \n" +
                   $"  m_Name: {filename}                           \n" +
                    "  m_EditorClassIdentifier:                     \n");

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

