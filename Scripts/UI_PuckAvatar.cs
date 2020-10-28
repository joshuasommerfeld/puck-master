using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PuckAvatar : MonoBehaviour{
  private Image _image;
  private PuckMasterPlayer _player;

  private void Awake(){
    _image = GetComponent<Image>();
  }

  public void RegisterPlayer(PuckMasterPlayer pmp){
    _player = pmp;
  }

  private void Update(){
    var image = _player.GetActivePuck().GetPuckSpecial().GetSpecialAvatar();
    if (image){
      _image.sprite = image;
      _image.color = Color.white;
    } else{
      _image.color = Color.clear;
    }
  }
}
