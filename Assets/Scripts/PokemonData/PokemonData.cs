using System;
using UnityEngine;

namespace PokemonData
{

    [CreateAssetMenu]
    public class PokemonData : ScriptableObject
    {
        [SerializeField] private short number;
        [SerializeField] private string sprite;
        [SerializeField] private string type1;
        [SerializeField] private string type2;
        [SerializeField] private string ability1;
        [SerializeField] private string ability2;
        [SerializeField] private string hiddenAbility;
        [SerializeField] private string notes;
        [SerializeField] private string bst;
        [SerializeField] private short hp;
        [SerializeField] private short att;
        [SerializeField] private short def;
        [SerializeField] private short sAtt;
        [SerializeField] private short sDef;
        [SerializeField] private short spd;
    }
}