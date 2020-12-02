using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterSelecter : MonoBehaviour{
  private InputController _ic;
  private AimChevron _ac;
  private Camera _cam;
  private SelectionDisplayHandler _selectionDisplayHandler;
  
  private List<CharacterName> _characters = new List<CharacterName>();

  private int _maxCharacters = 3;

  // Start is called before the first frame update
  void Start(){
    _ic = GetComponent<InputController>();
    _ac = GetComponentInChildren<AimChevron>();
    _selectionDisplayHandler = GetComponentInChildren<SelectionDisplayHandler>();
    _ac.Activate();

    _cam = Camera.main;
  }

  public CharacterName[] GetCharacters(){
    return _characters.ToArray();
  }

  void printCharacters(){
    var s = "";
    foreach (var characterName in _characters){
      s += characterName.ToString() + " : ";
    }

    Debug.Log(s);
  }

  private void SelectCharacter(){
    var chevToViewport = _cam.WorldToViewportPoint(_ac.gameObject.transform.position);
    Ray ray = _cam.ViewportPointToRay(chevToViewport);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit)){
      var characterSelection = hit.transform.gameObject.GetComponent<CharacterSelectionHandler>();
      if (characterSelection != null){
        _characters.Add(characterSelection.selectionCharacter);
      }

      _selectionDisplayHandler.UpdateSelections(GetCharacters());
    }
  }

  private void RemoveLastCharacter(){
    _characters.RemoveAt(_characters.Count - 1);
    _selectionDisplayHandler.UpdateSelections(GetCharacters());
  }
  
  // Update is called once per frame
  void Update(){
    if (_ic.GetSubmit()){
      if (_characters.Count < _maxCharacters){
        SelectCharacter();
      }
    }

    if (_ic.GetCancel()){
      RemoveLastCharacter();
    }
  }
}