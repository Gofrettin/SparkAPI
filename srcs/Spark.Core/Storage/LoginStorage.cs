using System;
using System.Collections.Generic;
using Spark.Core.Server;

namespace Spark.Core.Storage
{
    public class LoginStorage : IStorage
    {
        public List<SelectableCharacter> SelectableCharacters { get; }
        
        public Predicate<WorldServer> ServerSelector { get; }
        public Predicate<SelectableCharacter> CharacterSelector { get; }

        public LoginStorage(Predicate<WorldServer> serverSelector, Predicate<SelectableCharacter> characterSelector)
        {
            ServerSelector = serverSelector;
            CharacterSelector = characterSelector;
            SelectableCharacters = new List<SelectableCharacter>();
        }
    }
}