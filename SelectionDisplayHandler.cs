using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionDisplayHandler : MonoBehaviour{

    private SpriteRenderer[] _characterRenderers;
    private CharacterDictionary _characterDictionary;

    public Sprite blankSprite;
    
    private void Awake(){
        _characterRenderers = GetComponentsInChildren<SpriteRenderer>();
        _characterDictionary = FindObjectOfType<CharacterDictionary>();
    }

    public void UpdateSelections(CharacterName[] characters){
        for (int i = 0; i < _characterRenderers.Length; i++){
            if (i >= characters.Length){
                _characterRenderers[i].sprite = blankSprite;
            } else{
                _characterRenderers[i].sprite = _characterDictionary.GetCharacterForEnum(characters[i]).puckSprite;
            }
        }
    }
}
