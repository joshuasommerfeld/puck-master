using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPmp : PuckMasterPlayer {
  
	public int clickForceDelta = 10;
	
	private int _clickForce = 0;

	private InputController _inputController;
	
	private new void Awake(){
		base.Awake();
		_inputController = GetComponent<InputController>();
		foreach (var puck in pucks){
			puck.AddInputController(_inputController);
		}
	}

	protected override void HandleShooting(){
		switch (GetActivePuck().GetPuckPhase()){
				case PointAndShoot.PuckPhase.READY:
					deploymentArea.Activate();
					if (_inputController.GetPuckPlace()){
						deploymentArea.DeployPuck(GetActivePuck());
						deploymentArea.Deactivate();
					}
					break;
				case PointAndShoot.PuckPhase.IN_PLAY:
					aimChevron.Activate();
					if (_inputController.IsShootingPuck()){
						currentPhase = TurnPhase.IS_SHOOTING;
						aimChevron.LockChevron();
						powerIndicator.Activate(GetActivePuck().transform.position, aimChevron.transform.position);
					}
					break;
		}
	}

	protected override void TriggerShot(){
		if (_inputController.ShotReleased()){
			currentPhase = TurnPhase.HAS_SHOT;
		  powerIndicator.Deactive();
		}
	}

	private void IterateActivePuck(int direction){
		var pucksinPlay = GetPucksInPlay();
		if (pucksinPlay.Length == 0){
			Debug.Log("forced turn end");
			this.EndTurn();
		}
				
		activePuck = activePuck + direction;
		if (activePuck >= pucksinPlay.Length){
			activePuck = 0;
		}
		if (activePuck < 0){
			activePuck = pucksinPlay.Length - 1;
		}
		
		foreach (var puck in GetPucksInPlay()){
			puck.DisableHighlight();
		}
		GetActivePuck().EnableHighlight();
	}

	protected override void HandlePreTurn(){
		IterateActivePuck(0);
		currentPhase = TurnPhase.SELECT_PUCK;
	}
	
	protected override void HandleSelectPuck(){
		var i = _inputController.ChangePuckIndex();
		if(i != 0){
			IterateActivePuck(i);
		}

		if (_inputController.GetPuckSelect()){
			currentPhase = TurnPhase.PRE_SHOT;
		}
	}

	private Vector3 ShotDirection(){
		var mouseDir = aimChevron.transform.position - GetActivePuck().gameObject.transform.position;
		mouseDir.z = 0.0f;
		mouseDir = mouseDir.normalized;
		return mouseDir;
	}

	protected override void HandleIsShooting(){
		_clickForce += clickForceDelta;
	}

	protected override void MakeShot(){
		Vector3 shotDirection = ShotDirection();
		GetActivePuck().ShootPuck(shotDirection * _clickForce);
		
		_clickForce = 0;
		currentPhase = TurnPhase.POST_SHOT;
		aimChevron.Deactivate();
	}

}
