using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckHighlight : MonoBehaviour{

	private SpriteRenderer _puckHighlight;
	
	void Awake (){
		_puckHighlight = GetComponent<SpriteRenderer>();
	}

	public Boolean IsHighlighted(){
		return _puckHighlight.enabled;
	}
	
	public void EnableHighlight(){
		_puckHighlight.enabled = true;
	}
	
	public void DisableHighlight(){
		_puckHighlight.enabled = false;
	}
}
