using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChevron : MonoBehaviour{

	private bool _isActive;
	private BoxCollider2D _collider;
	private Rigidbody2D _rigidbody2D;
	private SpriteRenderer _spriteRenderer;
	private InputController _inputController;

	void Awake(){
		_collider = GetComponent<BoxCollider2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_rigidbody2D = GetComponent<Rigidbody2D>();
		foreach (var ic in FindObjectsOfType<InputController>()){
			if (ic.tag == this.tag){
				_inputController = ic;
			}
		}
	}

	public void Activate(){
		_collider.enabled = true;
		_spriteRenderer.enabled = true;
		_isActive = true;
	}
	
	public void Deactivate(){
		_collider.enabled = false;
		_spriteRenderer.enabled = false;
		_isActive = false;
	}
	
	void Update (){
		if (!_isActive){
			return;
		}
		_rigidbody2D.MovePosition(_inputController.MovePuckPlacementChevron(this.transform.position));
	}
}
