using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDictionary: MonoBehaviour{
    private readonly Dictionary<CharacterName, PuckCharacter> _characterDictionary = new Dictionary<CharacterName, PuckCharacter>();
    private void Awake(){
        foreach (var puckCharacter in GetComponentsInChildren<PuckCharacter>()){
            _characterDictionary.Add(puckCharacter.name, puckCharacter);
        }
    }

    public PuckCharacter GetCharacterForEnum(CharacterName characterName){
        return _characterDictionary[characterName];
    }
}
