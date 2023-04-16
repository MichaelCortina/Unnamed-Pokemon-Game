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
        public (PokemonType, PokemonType) Types => 
            (PokemonTypeFromString(type1), PokemonTypeFromString(type2));

        public (string?, string?) Abilities => (ability1, ability2);
        public string? HiddenAbility => hiddenAbility;
        public short Bst => bst;
        public short Hp => hp;
        public short Att => att;
        public short Def => def;
        public short SAtt => sAtt;
        public short SDef => sDef;
        public short Spd => spd;
        
        /// <summary>
        /// converts the provided string into the corresponding
        /// PokemonType Enum value
        /// </summary>
        /// <param name="typeName"> a valid name of a pokemon type </param>
        /// <returns>
        /// the type of the pokemon if found, or if the string is null
        /// or the type was not found PokemonType.Invalid is returned
        /// </returns>
        public static PokemonType PokemonTypeFromString(string typeName) =>
            typeName switch
            {
                "Grass" => PokemonType.Grass,
                "Ghost" => PokemonType.Ghost,
                "Water" => PokemonType.Water,
                "Normal" => PokemonType.Normal,
                "Ice" => PokemonType.Ice,
                "Fire" => PokemonType.Fire,
                "Flying" => PokemonType.Flying,
                "Bug" => PokemonType.Bug,
                "Dark" => PokemonType.Dark,
                "Dragon" => PokemonType.Dragon,
                "Fairy" => PokemonType.Fairy,
                "Fighting" => PokemonType.Fighting,
                "Rock" => PokemonType.Rock,
                "Steel" => PokemonType.Steel,
                "Ground" => PokemonType.Ground,
                "Electric" => PokemonType.Electric,
                "Poison" => PokemonType.Poison,
                _ => PokemonType.Invalid,
            };

        /// <summary>
        /// returns the percentage of damage that an attack with the <c>attackType</c>
        /// would deal to a pokemon with type <c>defenderType</c>
        /// </summary>
        /// <param name="attackType"> the type of the attack </param>
        /// <param name="defenderType"> the type of the defending pokemon </param>
        /// <returns> a float between 0 and 1 representing the effectiveness of the attack </returns>
        public static float Effectiveness(PokemonType attackType, PokemonType defenderType) =>
            attackType switch
            {
                PokemonType.Normal => defenderType switch
                {
                    PokemonType.Normal
                        or PokemonType.Fighting => .5f,
                    PokemonType.Ghost => 0f,
                    _ => 1f
                },
                PokemonType.Fighting => defenderType switch
                {
                    PokemonType.Fighting
                        or PokemonType.Flying
                        or PokemonType.Fairy => 0.5f,
                    PokemonType.Normal 
                        or PokemonType.Rock
                        or PokemonType.Steel
                        or PokemonType.Ice
                        or PokemonType.Dark => 2f,
                    PokemonType.Ghost => 0f,
                    _ => 1f
                },
                PokemonType.Flying => defenderType switch
                {
                    PokemonType.Flying
                        or PokemonType.Rock
                        or PokemonType.Electric
                        or PokemonType.Ice => .5f,
                    PokemonType.Fighting
                        or PokemonType.Bug
                        or PokemonType.Grass => 2f,
                    _ => 1f
                },
                PokemonType.Poison => defenderType switch
                {
                    PokemonType.Poison => .5f,
                    PokemonType.Grass 
                        or PokemonType.Fairy => 2f,
                    PokemonType.Steel => 0f,
                    _ => 1f
                }, //TODO: Add the rest of the cases to this switch statement, it is late and I am lazy - Big Mike
                PokemonType.Grass => defenderType switch
                {
                    PokemonType.Flying 
                        or PokemonType.Poison 
                        or PokemonType.Bug
                        or PokemonType.Fire
                        or PokemonType.Grass
                        or PokemonType.Ice => .5f,
                    PokemonType.Ground
                        or PokemonType.Rock
                        or PokemonType.Water => 2f,
                    _ => 1f
                },
            };
    }

    public enum PokemonType
    {
        Grass,
        Ghost,
        Water,
        Normal,
        Ice,
        Fire,
        Flying,
        Bug,
        Dark,
        Dragon,
        Fairy,
        Fighting,
        Rock,
        Steel,
        Ground,
        Electric,
        Poison,
        Invalid,
    }
}