using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

		foreach (var abstractSpecial in specials){
			abstractSpecial.enabled = false;
			if (abstractSpecial.active){
				if (!SpecialExists()){
					activeSpecial = abstractSpecial;
					activeSpecial.enabled = true;
				}
			}
		}

		if (SpecialExists()){
			_specialSpriteRenderer.sprite = activeSpecial.puckSprite;
		}
	}

	public void Initialise(PuckMasterPlayer _player, PointAndShoot _puck){
		puck = _puck;
		player = _player;
		foreach (var abstractSpecial in specials){
			abstractSpecial.Initialise(_player, _puck);
		}
	}

	public Sprite GetSpecialAvatar(){
		if (SpecialExists()){
			return activeSpecial.avatarSprite;
		}

		return null;
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
	
	public void OnPuckCollision(Collision2D collider2D){
		if (SpecialExists()){
			activeSpecial.OnPuckCollision(collider2D);
		}
	}
}
