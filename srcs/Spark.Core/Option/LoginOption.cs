using System;
using System.Collections.Generic;
using Spark.Core.Server;

namespace Spark.Core.Option
{
    public class LoginOption : IOption
    {
        public LoginOption(Predicate<WorldServer> serverSelector, Predicate<SelectableCharacter> characterSelector)
        {
            ServerSelector = serverSelector;
            CharacterSelector = characterSelector;
            SelectableCharacters = new List<SelectableCharacter>();
        }

        public List<SelectableCharacter> SelectableCharacters { get; }

        public Predicate<WorldServer> ServerSelector { get; }
        public Predicate<SelectableCharacter> CharacterSelector { get; }
    }
}