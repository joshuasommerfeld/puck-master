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
    Debug.Log( _player.GetActivePuck().GetPuckSpecial().GetSpecialAvatar());
    _image.sprite = _player.GetActivePuck().GetPuckSpecial().GetSpecialAvatar();
  }
}
