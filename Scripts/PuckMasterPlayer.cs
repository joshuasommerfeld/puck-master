using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using JetBrains.Annotations;
using UnityEngine;

public abstract class PuckMasterPlayer : MonoBehaviour{
  protected enum TurnPhase{
    INACTIVE,
    PRE_TURN,
    SELECT_PUCK,
    PRE_SHOT,
    IS_SHOOTING,
    HAS_SHOT,
    POST_SHOT
  }

  protected Color[] PlayerColors = {
    Color.green,
    Color.magenta,
  };

  protected PuckMasterGameManager pmgm;
  protected TurnPhase currentPhase = TurnPhase.INACTIVE;

  protected int playerNumber;
  protected int score;

  protected PointAndShoot[] pucks;
  protected int activePuck = 0;

  protected DeploymentArea deploymentArea;

  protected AimChevron aimChevron;

  /*
   * Lifecycle
   */
  
  protected void Awake(){
    pucks = GetComponentsInChildren<PointAndShoot>();
    aimChevron = GetComponentInChildren<AimChevron>();
    pmgm = FindObjectOfType<PuckMasterGameManager>();

    foreach (var da in FindObjectsOfType<DeploymentArea>()){
      if (this.tag.Equals(da.tag)){
        deploymentArea = da;
      }
    }
  }

  protected void Start(){
    foreach (var puck in pucks){
      puck.gameObject.GetComponent<Renderer>().material.SetColor("_Color", PlayerColors[playerNumber]);
      puck.gameObject.tag = gameObject.tag;
    }
  }
  
  protected void Update(){
    switch (currentPhase){
      case TurnPhase.INACTIVE:
        return;
      case TurnPhase.PRE_TURN:
        HandlePreTurn();
        break;
      case TurnPhase.SELECT_PUCK:
        HandleSelectPuck();
        break;
      case TurnPhase.PRE_SHOT:
        HandleShooting();
        break;
      case TurnPhase.IS_SHOOTING:
        TriggerShot();
        break;
    }
  }
  
    
  protected void FixedUpdate(){
    switch (currentPhase){
      case TurnPhase.INACTIVE:
        return;
      case TurnPhase.IS_SHOOTING:
        HandleIsShooting();
        break;
      case TurnPhase.HAS_SHOT:
        MakeShot();
        break;
      case TurnPhase.POST_SHOT:
        CheckForTurnEnd();
        break;
    }
  }
  
  /*
  * protected members
  */

  protected abstract void HandlePreTurn();
  protected abstract void HandleSelectPuck();
  protected abstract void HandleShooting();
  protected abstract void TriggerShot();

  protected abstract void HandleIsShooting();
  protected abstract void MakeShot();

  protected void CheckForTurnEnd(){
    var activePuck = GetActivePuck();

    if (activePuck == null){
      EndTurn();
      return;
    }

    if (pmgm.AllPucksAreStopped()){
      EndTurn();
    }
  }
  
  protected void EndTurn(){
    pmgm.EndTurn();
  }

  /*
  * Getters
  */
  
  public int GetPlayerNumber(){
    return playerNumber;
  }
  
  public PointAndShoot GetActivePuck(){
    var activePucks = GetPucksInPlay();
    return activePuck + 1 > activePucks.Length ? null : activePucks[activePuck];
  }

  public PointAndShoot[] GetPucks(){
    return pucks;
  }
  
  protected PointAndShoot[] GetPucksInPlay(){
    var activePucks = new List<PointAndShoot>();

    foreach (var puck in pucks){
      if (puck.IsInPlay()){
        activePucks.Add(puck);
      }
    }

    return activePucks.ToArray();
  }

  protected PointAndShoot[] GetPucksOutOfPlay(){
    var outOfPlayPucks = new List<PointAndShoot>();

    foreach (var puck in pucks){
      if (!puck.IsInPlay()){
        outOfPlayPucks.Add(puck);
      }
    }

    return outOfPlayPucks.ToArray();
  }

  public int GetScore(){
    return score;
  }
  
  /*
  * Setters
  */
  
  public void SetPlayerNumber(int number){
    playerNumber = number;
  }

  /*
  * Mutations
  */

  public void Activate(){
    currentPhase = TurnPhase.PRE_TURN;

    foreach (PointAndShoot puck in GetPucksOutOfPlay()){
      puck.DecrementCooldown();
      if (puck.GetCooldown() <= 0){
        puck.Ready();
      }
    }

    foreach (PointAndShoot puck in GetPucksInPlay()){
      puck.Activate();
    }
  }

  public void Deactivate(){
    Debug.Log("deactivating player " + playerNumber);
    currentPhase = TurnPhase.INACTIVE;
    foreach (PointAndShoot puck in GetPucksInPlay()){
      puck.Deactivate();
    }
  }  
  
  public int AddToScore(int scoreToAdd){
    score += scoreToAdd;
    return score;
  }

  public void PutPuckOnCooldown(GameObject puck){
    puck.GetComponent<PointAndShoot>().ScorePuckAndPutOnCooldown();

    puck.transform.position = pmgm.transform.position;
  }

}