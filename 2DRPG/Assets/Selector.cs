using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//selects and deselcts units on the board based on mouse clicks.

public class Selector : MonoBehaviour {

	private ManagerHub manager;
	public List <Square> selectedUnits = new List<Square>();

	void Destroy(){
		ManagerHub.onAnimationPlay-=resetSelection;
	}

	// Use this for initialization
	void Start () {

		manager = gameObject.GetComponent<ManagerHub>();

//		for(int i = 0; i<selectedUnits.Length; i++){
//			selectedUnits[i] = new List<GameObject>();//change to squares
//		}

		ManagerHub.onAnimationPlay+=clearSelection;
	}

	private void selectOrDeselect(Square s){

		if(manager.board.squareInBounds(s, Direction.getDirection(Direction.NONE)) && manager.board.isOccupied(s)){

			if(selectedUnits.Contains(s)){

				deselect(s);

			}else{

				select(s);

			}
		}

		//Debug.Log(selectedUnits[manager.turn].ToString()+", "+selectedUnits[manager.turn].ToArray().Length);

	}

	private void select(Square s){
		selectedUnits.Add(s);
		manager.board.selectSquareContents(s);
	}

	private void deselect(Square s){
		selectedUnits.Remove(s);
		manager.board.deselectSquareContents(s);
	}

	void clearSelection(){
		selectedUnits.Clear();//Should be unnecessary.
	}

	void resetSelection(){
		//oldTurn is not used
		for(int i = 0; i<selectedUnits.Count; i++){
			deselect(selectedUnits[i]);
		}
	}

	// Update is called once per frame
	void Update () {
		//Selection must account for subsequent positions
		if(Input.GetMouseButtonDown(0)/* && ( Input.mousePosition())*/){
			
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			
			Square boardPos = manager.board.convertMouseClickToBoardCoords(mousePos);
			
			Debug.Log (boardPos.x+" "+ boardPos.y);

			selectOrDeselect(boardPos);

		}
	}
}
