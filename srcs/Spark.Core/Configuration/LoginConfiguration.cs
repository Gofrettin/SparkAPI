using System;
using System.Collections.Generic;
using Spark.Core.Server;

namespace Spark.Core.Configuration
{
    public class LoginConfiguration : IConfiguration
    {
        public LoginConfiguration(Predicate<WorldServer> serverSelector, Predicate<SelectableCharacter> characterSelector)
        {
            ServerSelector = serverSelector;
            CharacterSelector = characterSelector;
            SelectableCharacters = new List<SelectableCharacter>();
        }

        public LoginConfiguration()
        {
        }

        public List<SelectableCharacter> SelectableCharacters { get; } = new List<SelectableCharacter>();

        public Predicate<WorldServer> ServerSelector { get; }
        public Predicate<SelectableCharacter> CharacterSelector { get; }
    }
}