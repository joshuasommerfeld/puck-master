using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpecialManager : MonoBehaviour{

	protected AbstractSpecial[] specials;

	protected AbstractSpecial activeSpecial;

	protected PointAndShoot puck;
	protected PuckMasterPlayer player;

	private SpriteRenderer _specialSpriteRenderer;

	private bool SpecialExists(){
		return activeSpecial != null;
	}
	
	// Use this for initialization
	void Awake (){
		_specialSpriteRenderer = GetComponent<SpriteRenderer>();
		specials = GetComponents<AbstractSpecial>();
		var activeSpecials = specials.Where(_as => _as.enabled).ToArray();
		if (activeSpecials.Length > 0){
			activeSpecial = activeSpecials[0];
		}
		
		if (SpecialExists()){
			_specialSpriteRenderer.sprite = activeSpecial.puckSprite;
		}
	}

	public void Initialise(PuckMasterPlayer _player, PointAndShoot _puck){
		puck = _puck;
		player = _player;
		activeSpecial.Initialise(_player, _puck);
	}
	
	public void AddInputController(InputController ic){
		foreach (var _special in specials){
			_special.AddInputController(ic);
		}
	}

	public void StartTurn(){
		if (SpecialExists()){
			activeSpecial.StartTurn();
		}
	}

	public void PreShot(){
		if (SpecialExists()){
			activeSpecial.PreShot();
		}
	}

	public void IsShooting(){
		if (SpecialExists()){
			activeSpecial.IsShooting();
		}
	}

	public void PostShot(){
		if (SpecialExists()){
			activeSpecial.PostShot();
		}
	}
}
