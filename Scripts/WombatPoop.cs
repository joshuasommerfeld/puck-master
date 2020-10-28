using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WombatPoop : MonoBehaviour{

  public GameObject explosion;

  private Boolean _hasBeenHit = false;
  private double _cooldown = 0.1f;

  private double _liftetime = 0f;
  
  void OnCollisionEnter2D(){
    if (_liftetime > 0.5f){
      _hasBeenHit = true;
    }
  }

  private void Update(){
    _liftetime += Time.deltaTime;
    
    if (_hasBeenHit){
      _cooldown -= Time.deltaTime;

      if (_cooldown < 0){
        Instantiate(explosion, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
      }
    }
  }
}
