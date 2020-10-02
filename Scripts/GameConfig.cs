using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour{
  private Dictionary<string, string> _config = new Dictionary<string, string>();

  public bool isGamepad;

  void Awake(){
    Initialise(new Dictionary<string, string>());
  }

  public void Initialise(Dictionary<string, string> config){
    _config = config;
    _config.Add("inputType", isGamepad ? "gamepad" : "mouse");
  }
  
  public void AddConfig(string key, string value){
    _config.Add(key, value);
  }
  
  public string GetConfig(string key){
    return _config[key];
  }
}
