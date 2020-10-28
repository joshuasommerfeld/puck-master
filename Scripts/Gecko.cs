using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gecko : AbstractSpecial{

    public GameObject puckBloom;
    private double _coyoteTime;
    private FixedJoint2D _stuckTo;
    
    public override void Initialise(PuckMasterPlayer _player, PointAndShoot _puck){
        player = _player;
        puck = _puck;

        if (active){
            var spawnPos = new Vector3(transform.position.x, transform.position.y, 1);
            var obj = Instantiate(puckBloom, spawnPos, transform.rotation);
            obj.transform.parent = gameObject.transform;
            obj.GetComponent<SpriteRenderer>().color = _player.GetPlayerColor();
        }
    }

    public override void StartTurn(){
        _coyoteTime = 0.01f;
        Destroy(_stuckTo);
    }

    private bool IsStickyTime(){
        return active &&
               puck.IsActive() &&
               puck.GetPuckPhase().Equals(PointAndShoot.PuckPhase.IN_PLAY) &&
               player.GetCurrentPhase().Equals(PuckMasterPlayer.TurnPhase.POST_SHOT);
    }
    
    private void Update(){
        if (IsStickyTime()){
            _coyoteTime -= Time.deltaTime;
        }
    }

    public override void OnPuckCollision(Collision2D collider2D){
        if (!puck.IsActive() || !player.GetCurrentPhase().Equals(PuckMasterPlayer.TurnPhase.POST_SHOT)){
            return;
        }
        if (_stuckTo == null){
            _stuckTo = collider2D.gameObject.AddComponent<FixedJoint2D>();
            _stuckTo.anchor = collider2D.contacts[0].point;
            _stuckTo.connectedBody = collider2D.contacts[0].otherRigidbody;
            _stuckTo.enableCollision = false;
        }
    }
}
