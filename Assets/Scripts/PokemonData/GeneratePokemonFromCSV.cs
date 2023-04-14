using System.IO;
using System.Linq;
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
    public class GeneratePokemonFromCSV : ScriptableObject
    {
        [SerializeField] private string dataFilePath;
        [SerializeField] private string outputDirectoryPath;

        public void GenerateScriptableObjects()
        {
            string[] lines = File.ReadAllLines(dataFilePath);
                
            foreach (var arr in lines.Select(s => s.Split(',')))
            {
                string name = arr[0];

                PokemonData pokemon = FindPokemonWithName(name);

                if (pokemon == null)
                    pokemon = CreateInstance<PokemonData>();
                
                
            }
        }

        private PokemonData FindPokemonWithName(string name)
        {
            throw new System.NotImplementedException();
        }
    }

    public class PokemonData : ScriptableObject
    {
        public string Name { get; init; }
    }
}

