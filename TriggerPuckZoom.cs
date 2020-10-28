using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPuckZoom : MonoBehaviour
{	
    private PuckCollisionEffect _puckCollisionEffect;
    private PointAndShoot _puck;
    
    // Start is called before the first frame update
    void Awake(){
        _puckCollisionEffect = FindObjectOfType<PuckCollisionEffect>();
    }

    public void Initialise(PointAndShoot puck){
        _puck = puck;
    }


    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other){
        if (
            _puck.GetPuckPhase() == PointAndShoot.PuckPhase.IN_PLAY &&
            _puck.IsActive() &&
            !_puck.IsStopped()
        ){
            _puckCollisionEffect.StartZoomingToPoint(other.transform.position);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other){
        _puckCollisionEffect.StopZoomingToPoint();
    }
}
