using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialManager : MonoBehaviour{

	private AbstractSpecial _special;

	private SpriteRenderer _specialSpriteRenderer;

	private bool SpecialExists(){
		return _special != null;
	}
	
	// Use this for initialization
	void Start (){
		_specialSpriteRenderer = GetComponent<SpriteRenderer>();
		_special = GetComponent<AbstractSpecial>();
		if (SpecialExists()){
			_specialSpriteRenderer.sprite = _special.puckSprite;
		}
	}

	public void StartTurn(){
		if (SpecialExists()){
			_special.StartTurn();
		}
	}

	public void PreShot(){
		if (SpecialExists()){
			_special.PreShot();
		}
	}

	public void IsShooting(){
		if (SpecialExists()){
			_special.IsShooting();
		}
	}

	public void PostShot(){
		if (SpecialExists()){
			_special.PostShot();
		}
	}
}
