using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSpecial : MonoBehaviour{

  public Sprite puckSprite;

  protected InputController ic;

  protected PuckMasterPlayer player;
  protected PointAndShoot puck;

  public void AddInputController(InputController ic){
    this.ic = ic;
  }

  public abstract void Initialise(PuckMasterPlayer _player, PointAndShoot _puck);
  
  public void StartTurn(){}
  public void PreShot(){}
  public void IsShooting(){}
  public void PostShot(){}
}
