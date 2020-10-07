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
		Vector3 vectorToTarget = facingPos - startingPos;
		Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;
		
		transform.position = new Vector3(startingPos.x, startingPos.y, -10f);
		transform.rotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
		_powerIndicator.Play();
	}

	public void Deactive(){
		_powerIndicator.Stop();
		_powerIndicator.Clear();
	}

}
