using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour{

	public Text Player1Score;
	public Text Player2Score;

	public UI_PuckStatus[] Player1Pucks;
	public UI_PuckStatus[] Player2Pucks;

	public UI_PuckAvatar Player1Avatar;
	public UI_PuckAvatar Player2Avatar;

	private PuckMasterGameManager _pmgm;

	private PuckMasterPlayer _player1;
	private PuckMasterPlayer _player2;
	
	// Use this for initialization
	void Start (){
		_pmgm = FindObjectOfType<PuckMasterGameManager>();
		_player1 = _pmgm.GetPlayerWithTag("Player1");
		_player2 = _pmgm.GetPlayerWithTag("Player2");
		
		RegisterPucksWithUI(_player1, Player1Pucks);
		RegisterPucksWithUI(_player2, Player2Pucks);
		Player1Avatar.RegisterPlayer(_player1);
		Player2Avatar.RegisterPlayer(_player2);
	}

	private void RegisterPucksWithUI(PuckMasterPlayer player, UI_PuckStatus[] pucksText){
		var pucks = player.GetPucks();
		for (var i = 0; i < pucks.Length; i++){
			pucksText[i].RegisterPuck(pucks[i]);
		}
	}
	
	// Update is called once per frame
	void Update (){
		Player1Score.text = _player1.GetScore().ToString();
		Player2Score.text = _player2.GetScore().ToString();
	}
}
