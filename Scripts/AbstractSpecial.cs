using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSpecial : MonoBehaviour{

  public Sprite puckSprite;
  public Sprite avatarSprite;
  
  protected InputController ic;

  protected PuckMasterPlayer player;
  protected PointAndShoot puck;

  public bool active;

  public void AddInputController(InputController ic){
    this.ic = ic;
  }

  public abstract void Initialise(PuckMasterPlayer _player, PointAndShoot _puck);

  public abstract void StartTurn();
  public void PreShot(){}
  public void IsShooting(){}
  public void PostShot(){}
  public abstract void OnPuckCollision(Collision2D collider2D);
}
