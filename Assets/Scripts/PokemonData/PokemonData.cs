using UnityEngine;

namespace PokemonData
{
    [CreateAssetMenu]
    public class PokemonData : ScriptableObject
    {
        [SerializeField] private string pokemonName;
    }
}