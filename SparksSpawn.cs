using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparksSpawn : MonoBehaviour{
  public GameObject sparks;

  private void OnCollisionEnter2D(Collision2D other){
    Instantiate(sparks, other.contacts[0].point, other.transform.rotation);
  }
}
