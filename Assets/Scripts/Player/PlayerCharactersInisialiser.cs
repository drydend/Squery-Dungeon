using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCharactersInisialiser : MonoBehaviour
{
    [SerializeField]
    private List<CharacterType> _avaibleCharacterFromStart;
    [SerializeField]
    private List<Character> _charactersPrefabs;
    [SerializeField]
    private Player _player;
    private CharacterTypeController _characterTypeController;

    private void Awake()
    {
        foreach (var character in _charactersPrefabs)
        {
            character.Initialize();
        }
        _characterTypeController = new CharacterTypeController(_charactersPrefabs, _avaibleCharacterFromStart);
        _player.Initialize(_characterTypeController);
    }


}
