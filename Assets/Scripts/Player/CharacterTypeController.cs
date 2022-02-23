using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTypeController
{
    private Dictionary<CharacterType, Character> _allCharacters;
    private List<CharacterType> _avaibleCharacters;

    public Character CurrentCharacter { get; private set; }

    public CharacterTypeController(List<Character> characters ,List<CharacterType> avaibleCharactersFromStart)
    {
        _allCharacters = new Dictionary<CharacterType, Character>();
        _avaibleCharacters = avaibleCharactersFromStart;

        foreach (var character in characters)
        {
            _allCharacters[character.CharacterType] = character;
            character.gameObject.SetActive(false);
        }
        CurrentCharacter = _allCharacters[avaibleCharactersFromStart[0]];
        CurrentCharacter.gameObject.SetActive(true);
    }
    
    public Character GetCharacter(CharacterType characterType)
    {
        return _allCharacters[characterType];
    }


    public bool TrySetCharacter(CharacterType characterType, out Character character)
    {
        if (_avaibleCharacters.Contains(characterType))
        {
            CurrentCharacter.OnCharacterDisable();
            character = _allCharacters[characterType];
            _allCharacters[characterType].transform.position = CurrentCharacter.gameObject.transform.position;
            CurrentCharacter = _allCharacters[characterType];
            CurrentCharacter.OnCharacterEnable();
            return true;
        }
        else
        {
            character = CurrentCharacter;
            return false;
        }
    }

}

public enum CharacterType
{
    Triangle,
    Square,
    Pentagon
}
