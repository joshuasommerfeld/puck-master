using System;
using UnityEngine;

public class PointAndShoot : MonoBehaviour{
	private Rigidbody2D _rb;
	private PuckHighlight _puckHighlight;
	private TrailRenderer _trailRenderer;

	private Guid _uuid;
	private int _cooldown;

	public GameObject onExplosionInstantiate;

	public enum PuckPhase{
		OUT_OF_PLAY,
		READY,
		IN_PLAY
	}

	private bool _isActive = false;
	
	private PuckPhase _currentPhase = PuckPhase.READY;
	
	void Awake(){
		_uuid = Guid.NewGuid();
		_rb = GetComponent<Rigidbody2D>();
		_puckHighlight = GetComponentInChildren<PuckHighlight>();
		_trailRenderer = GetComponentInChildren<TrailRenderer>();
		_trailRenderer.enabled = false;
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
	
	public void Ready(){
		_currentPhase = PuckPhase.READY;
	}

	public void InPlay(){
		_trailRenderer.enabled = true;
		_currentPhase = PuckPhase.IN_PLAY;
	}
	
	public void Activate(){
		_isActive = true;
	}

	public void Deactivate(){
		DisableHighlight();
		_isActive = false;
	}

	public Guid GetID(){
		return _uuid;
	}
	
	public bool HasGuid(Guid uuid){
		return _uuid.Equals(uuid);
	}

	public void SetCooldown(int c){
		_cooldown = c;
	}

	public void DecrementCooldown(){
		_cooldown--;
	}

	public int GetCooldown(){
		return _cooldown;
	}

	public void ScorePuckAndPutOnCooldown(){
		_currentPhase = PuckPhase.OUT_OF_PLAY;
		_trailRenderer.enabled = false;
		DisableHighlight();
		SetCooldown(3);

		Instantiate(onExplosionInstantiate, this.transform.position, Quaternion.identity);
	}

	public Boolean IsHighlighted(){
		return _puckHighlight.IsHighlighted();
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

	public Boolean isStopped(){
		return _rb.velocity.magnitude < 0.05;
	}
}