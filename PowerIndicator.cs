using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerIndicator : MonoBehaviour{

	private ParticleSystem _powerIndicator;

	void Awake(){
		_powerIndicator = GetComponent<ParticleSystem>();
		_powerIndicator.Stop();
	}

	public void Activate(Vector3 startingPos, Vector3 facingPos){
		
		// vector from this object towards the target location
		Vector3 vectorToTarget = facingPos - startingPos;
		// rotate that vector by 90 degrees around the Z axis
		Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;
		
		var noZStartingPos = new Vector3(startingPos.x, startingPos.y, 0);
		var noZFacingPoss = new Vector3(facingPos.x, facingPos.y, 0);
		
		transform.position = startingPos;
		transform.rotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
		_powerIndicator.Play();
	}

	public void Deactive(){
		_powerIndicator.Stop();
		_powerIndicator.Clear();
	}

}
