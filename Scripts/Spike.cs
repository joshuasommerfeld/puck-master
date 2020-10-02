using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {

	private PuckMasterPlayer[] players;
	
	// Use this for initialization
	void Start (){
		players = FindObjectsOfType<PuckMasterPlayer>();
	}

	private void KillPuck(GameObject puck){
		foreach (var player in players){
			if (player.CompareTag(puck.tag)){
				player.PutPuckOnCooldown(puck);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D puck) {
		KillPuck(puck.gameObject);
	}
}
