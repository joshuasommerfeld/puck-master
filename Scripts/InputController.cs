using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour{

  private GameConfig _config;
  private Camera _camera;

  private float _scrollSpeed = 0.1f;

  private float _selectionCooldown = 0f;

  private void Awake(){
    _config = FindObjectOfType<GameConfig>();
    _camera = Camera.main;
  }

  private void Update(){
    _selectionCooldown -= 0.05f;
  }

  private bool IsGamepad(){
    return _config.GetConfig("inputType") == "gamepad";
  }
  
  private bool IsMouseAndKeyboard(){
    return _config.GetConfig("inputType") == "mouse";
  }

  private void selectCooldown(){
    _selectionCooldown = 0.5f;
  }
  
  public int ChangePuckIndex(){
    if (IsGamepad()){
      if (_selectionCooldown > 0){
        return 0;
      }
      if (Input.GetAxis("Vertical") < -0.8f){
        selectCooldown();
        return 1;
      }
      if (Input.GetAxis("Vertical") > 0.8f){
        selectCooldown();
        return -1;
      }
    }
    
    if (IsMouseAndKeyboard()){
      if (Input.GetKeyDown("down")){
        return 1;
      }
      if (Input.GetKeyDown("up")){
        return -1;
      }
    }

    return 0;
  }

  public bool GetPuckSelect(){
    if (IsGamepad()){
      return Input.GetButtonUp("Submit");
    }

    if (IsMouseAndKeyboard()){
      return Input.GetMouseButtonUp(0);
    }

    return false;
  }
  
  public bool GetPuckPlace(){
    if (IsGamepad()){
      return Input.GetButtonUp("Submit");
    }

    if (IsMouseAndKeyboard()){
      return Input.GetMouseButtonUp(0);
    }

    return false;
  }
  
  public bool IsShootingPuck(){
    if (IsGamepad()){
      return Input.GetButton("Submit");
    }

    if (IsMouseAndKeyboard()){
      return Input.GetMouseButton(0);
    }

    return false;
  }
  
  public bool ShotReleased(){
    if (IsGamepad()){
      return Input.GetButtonUp("Submit");
    }

    if (IsMouseAndKeyboard()){
      return Input.GetMouseButtonUp(0);
    }

    return false;
  }

  public Vector3 MovePuckPlacementChevron(Vector3 currentPosition){
    if (IsGamepad()){
      var input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
      return currentPosition + (input * _scrollSpeed);
    }

    if (IsMouseAndKeyboard()){
      var worldPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
      return Vector3.MoveTowards(currentPosition, worldPoint, 1);
    }
    return Vector3.zero;
  }
}
