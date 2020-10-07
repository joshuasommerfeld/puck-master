using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSpecial : MonoBehaviour{

  public Sprite puckSprite;

  public void StartTurn(){}
  public void PreShot(){}
  public void IsShooting(){}
  public void PostShot(){}
}
