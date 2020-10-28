using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckMasterGameManager : MonoBehaviour{

	private PuckMasterPlayer[] _players;
	private int _activePlayer;
	private PuckCollisionEffect _puckCollisionEffect;

	private void Awake(){
		_players = FindObjectsOfType<PuckMasterPlayer>();
		_puckCollisionEffect = FindObjectOfType<PuckCollisionEffect>();

		for (int i = 0; i < _players.Length; i++) {
			_players[i].SetPlayerNumber(i);
		}
	}

	// Use this for initialization
	void Start(){
		_activePlayer = 0;
		CycleActivePlayer(0);
	}

	public int GetActivePlayer(){
		return _activePlayer;
	}

	public PuckMasterPlayer[] GetPlayers(){
		return _players;
	}
	
	private void SetActivePlayer(int? newPlayer){
		if (newPlayer == null) {
			_activePlayer++;
			if (_activePlayer >= _players.Length){
				_activePlayer = 0;
			}
		} else {
			_activePlayer = newPlayer.Value;
		}
	}
	
	private void CycleActivePlayer(int? newPlayer){
		SetActivePlayer(newPlayer);

		for (int i = 0; i < _players.Length; i++) {
			if (i == _activePlayer) {
				_players[i].Activate(); 
			} else {
				_players[i].Deactivate();
			}
		}
	}
	
	public void EndTurn(){
		CycleActivePlayer(null);
		_puckCollisionEffect.StopZoomingToPoint();
	}

	public PuckMasterPlayer GetPlayerWithTag(string playerTag){
		foreach (var player in _players){
			if (player.tag.Equals(playerTag)){
				return player;
			}
		}

		return null;
	}

	public Boolean AllPucksAreStopped(){
		foreach (var player in _players){
			foreach (var pointAndShoot in player.GetPucks()){
				if (!pointAndShoot.IsStopped()){
					return false;
				}
			}
		}

		return true;
	}
}
