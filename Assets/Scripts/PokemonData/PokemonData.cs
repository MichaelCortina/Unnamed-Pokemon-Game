using System;
using JetBrains.Annotations;
using UnityEngine;

namespace PokemonData
{
    [CreateAssetMenu]
    public class PokemonData : ScriptableObject
    {
        [SerializeField] private short number;
        [SerializeField] private string type1;
        [SerializeField] private string type2;
        [SerializeField] private string ability1;
        [SerializeField] private string ability2;
        [SerializeField] private string hiddenAbility;
        [SerializeField] private string notes;
        [SerializeField] private short bst;
        [SerializeField] private short hp;
        [SerializeField] private short att;
        [SerializeField] private short def;
        [SerializeField] private short sAtt;
        [SerializeField] private short sDef;
        [SerializeField] private short spd;

        #nullable enable
        
        private Sprite? _backingSprite;
        public Sprite Sprite
        {
            get
            {
                if (_backingSprite is null)
                    _backingSprite = Resources.Load<Sprite>($"PokemonSprites/{name}");
                
                return _backingSprite;
            }
        }

        public short Number => number;
        public (string, string?) Types => (type1, type2);
        public (string?, string?) Abilities => (ability1, ability2);
        public string? HiddenAbility => hiddenAbility;
        public short Bst => bst;
        public short Hp => hp;
        public short Att => att;
        public short Def => def;
        public short SAtt => sAtt;
        public short SDef => sDef;
        public short Spd => spd;
    }
}