using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeploymentArea : MonoBehaviour{

	private BoxCollider2D[] _walls;
	private SpriteRenderer _sprite;
	private InputController _inputController;

	private SpawnChevron _spawnChevron;
	
	// Use this for initialization
	void Awake (){
		_walls = GetComponents<BoxCollider2D>();
		_sprite = GetComponent<SpriteRenderer>();
		_spawnChevron = GetComponentInChildren<SpawnChevron>();
	}

	void Start(){
		Deactivate();
	}

	public void Deactivate(){
		_sprite.enabled = false;
		_spawnChevron.Deactivate();
		foreach (var wall in _walls){
			wall.enabled = false;
		}		
	}

	public void Activate(){
		_sprite.enabled = true;
		_spawnChevron.Activate();
		foreach (var wall in _walls){
			wall.enabled = true;
		}		
	}

	public void DeployPuck(PointAndShoot puck){
		puck.transform.position = _spawnChevron.transform.position;
		puck.InPlay();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
