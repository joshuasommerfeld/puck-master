using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wombat : AbstractSpecial{

    public GameObject wombatPoop;
    
    public override void Initialise(PuckMasterPlayer _player, PointAndShoot _puck){
        player = _player;
        puck = _puck;
    }
    
    public override void StartTurn(){}

    // Update is called once per frame
    void Update(){
        if (!puck.IsActive()){
            return;
        }
        
        if (player.GetCurrentPhase() == PuckMasterPlayer.TurnPhase.POST_SHOT){
            if (ic.GetSubmit()){
                Poop();
            }
        }
    }

    void Poop(){
        var velocity2 = puck.gameObject.GetComponent<Rigidbody2D>().velocity;
        Vector3 velocity = velocity2;
        var positon = puck.gameObject.transform.position;
        var spawnPoint = positon - (velocity.normalized * 0.4f);
        
        Instantiate(wombatPoop, spawnPoint, puck.gameObject.transform.rotation);
    }

    public override void OnPuckCollision(Collision2D collider2D){ }
}
