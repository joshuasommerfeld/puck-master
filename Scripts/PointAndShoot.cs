using System;
using UnityEngine;

public class PointAndShoot : MonoBehaviour{
	private Rigidbody2D _rb;
	private PuckHighlight _puckHighlight;
	private TrailRenderer _trailRenderer;
	private Guid _uuid;
	private int _cooldown;

	private SpecialManager _special;

	public GameObject onExplosionInstantiate;

	public enum PuckPhase{
		OUT_OF_PLAY,
		READY,
		IN_PLAY
	}

	private bool _isActive = false;
	
	private PuckPhase _currentPhase = PuckPhase.READY;
	
	/*
	 * Lifecycle events
	 */
	
	void Awake(){
		_uuid = Guid.NewGuid();
		_rb = GetComponent<Rigidbody2D>();
		_puckHighlight = GetComponentInChildren<PuckHighlight>();
		_trailRenderer = GetComponentInChildren<TrailRenderer>();
		_special = GetComponentInChildren<SpecialManager>();
		_trailRenderer.enabled = false;
	}
	
	/*
	 * Mutation events
	 */

	public void Initialise(PuckMasterPlayer _player){
		_special.Initialise(_player, this);
		this.tag = _player.tag;
	}
	
	public void AddInputController(InputController ic){
		_special.AddInputController(ic);
	}

	public void Activate(){
		_isActive = true;
	}

	public void Deactivate(){
		DisableHighlight();
		_isActive = false;
	}
	
	public void Ready(){
		_currentPhase = PuckPhase.READY;
	}
	
	public void SetCooldown(int c){
		_cooldown = c;
	}

	public void DecrementCooldown(){
		_cooldown--;
	}
	
	public void InPlay(){
		_trailRenderer.enabled = true;
		_currentPhase = PuckPhase.IN_PLAY;
	}
	
	public void ScorePuckAndPutOnCooldown(){
		_currentPhase = PuckPhase.OUT_OF_PLAY;
		_trailRenderer.enabled = false;
		DisableHighlight();
		SetCooldown(3);

		Instantiate(onExplosionInstantiate, this.transform.position, Quaternion.identity);
	}
	
	public void EnableHighlight(){
		_puckHighlight.EnableHighlight();
	}
	
	public void DisableHighlight(){
		_puckHighlight.DisableHighlight();
	}
	
	public void ShootPuck(Vector3 force){
		_rb.AddForce(force);
	}
	
	public void StopPuck(){
		_rb.velocity = Vector3.zero;
	}

	/*
	* Getters events
	*/
	
	public SpecialManager GetPuckSpecial(){
		return _special;
	}
	
	public bool IsInPlay(){
		return !_currentPhase.Equals(PuckPhase.OUT_OF_PLAY);
	}

	public PuckPhase GetPuckPhase(){
		return _currentPhase;
	}
	
	public string GetPuckPhaseAsString(){
		return _currentPhase.ToString();
	}
	
	public bool IsActive(){
		return _isActive;
	}
	
	public Guid GetID(){
		return _uuid;
	}
	
	public bool HasGuid(Guid uuid){
		return _uuid.Equals(uuid);
	}

	public int GetCooldown(){
		return _cooldown;
	}
	
	public Boolean IsHighlighted(){
		return _puckHighlight.IsHighlighted();
	}
	
	public Boolean IsStopped(){
		return _rb.velocity.magnitude < 0.05;
	}
}