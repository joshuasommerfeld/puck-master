using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorer : MonoBehaviour{

	private PuckMasterPlayer[] players;
	public int scoreValue = 1;
	
	// Use this for initialization
	void Start (){
		players = FindObjectsOfType<PuckMasterPlayer>();
	}

	private void AddScore(GameObject puck, int score){
		foreach (var player in players){
			if (player.CompareTag(puck.tag)){
				player.AddToScore(score);
				player.PutPuckOnCooldown(puck);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D puck) {

		if (puck.gameObject.GetComponent<PointAndShoot>() == null){
			return;
		}
		
		Debug.Log(gameObject.tag + " scorer hit by puck of " + puck.gameObject.tag);
		var puckTag = puck.gameObject.tag;
		if (CompareTag(puckTag)){
			// The puck belongs to the player who owns this goal, -1 score
			AddScore(puck.gameObject, -scoreValue);
		} else{
			// this puck belongs to another player entering an opponents goal, +1 score;
			AddScore(puck.gameObject, scoreValue);
		}
	}
}
