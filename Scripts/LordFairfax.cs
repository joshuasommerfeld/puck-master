using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LordFairfax : AbstractSpecial {
  public override void Initialise(PuckMasterPlayer _player, PointAndShoot _puck){
    player = _player;
    puck = _puck;
  }
  public override void StartTurn(){}
  
  private void Update(){
    if (!puck.IsActive()){
      return;
    }

    if (player.GetCurrentPhase() == PuckMasterPlayer.TurnPhase.POST_SHOT){
      if (ic.GetSubmit()){
        StopPuck();
      }
    }
  }

  public void StopPuck(){
    puck.StopPuck();
  }
  public override void OnPuckCollision(Collision2D collider2D){ }
}
