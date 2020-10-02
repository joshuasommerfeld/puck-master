using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PuckStatus : MonoBehaviour{

	private Text _puckStatus;
	private Image _highlight;
	
	private PointAndShoot _puck;
	private String _cachedPhase;
	private bool _cachedHighlight;

	public Color HighlightColor = new Color(255, 255, 255, 0.5f);
	
	// Update is called once per frame
	void Start (){
		_puckStatus = GetComponent<Text>();
		_highlight = GetComponentInChildren<Image>();
	}
	
	public void RegisterPuck (PointAndShoot puck){
		_puck = puck;
		_cachedPhase = _puck.GetPuckPhaseAsString();
		_cachedHighlight = _puck.IsHighlighted();
		UpdateCache();
	}

	private bool CacheHasChange(){
		return
			_cachedPhase != _puck.GetPuckPhaseAsString() ||
			_cachedHighlight != _puck.IsHighlighted();
	}

	private void UpdateCache(){
		_cachedPhase = _puck.GetPuckPhaseAsString();
		_cachedHighlight =  _puck.IsHighlighted();
		_puckStatus.text = _cachedPhase;

		if (_puck.IsHighlighted()){
			_highlight.color = HighlightColor;
		} else{
			_highlight.color = Color.clear;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (CacheHasChange()){
			UpdateCache();
		}
	}
}
