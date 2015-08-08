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
		ManagerHub.onPlayerChange+=changePlayer;
	}

	private void selectOrDeselect(Square s){

//		#if UNITY_EDITOR
//		//Debug.Log(message, context);
//		#else
//		Application.ExternalCall("console.log", "in bounds?: "+manager.board.squareInBounds(s, Direction.getDirection(Direction.NONE)));
//		Application.ExternalCall("console.log", "occupied?: "+manager.board.isOccupied(s));
//		Application.ExternalCall("console.log", "belongs to player?: "+unitBelongsToPlayer(s));
//		Application.ExternalCall("console.log", "state is correct?: "+(manager.state=="planning"));
//		Application.ExternalCall("console.log", "all together?: "+(manager.board.squareInBounds(s, Direction.getDirection(Direction.NONE)) && manager.board.isOccupied(s) && unitBelongsToPlayer(s) && manager.state=="planning"));
//		#endif

		if(manager.board.squareInBounds(s, Direction.getDirection(Direction.NONE)) && manager.board.isOccupied(s) && unitBelongsToPlayer(s) && manager.state=="planning"){

//			#if UNITY_EDITOR
//			//Debug.Log(message, context);
//			#else
//			Application.ExternalCall("console.log", "dude selected?: "+selectedUnits.Contains(s));
//			#endif

			if(selectedUnits.Contains(s)){

//				#if UNITY_EDITOR
//				//Debug.Log(message, context);
//				#else
//				Application.ExternalCall("console.log", "deselecting");
//				#endif

				deselect(s);

			}else{
//
//				#if UNITY_EDITOR
//				//Debug.Log(message, context);
//				#else
//				Application.ExternalCall("console.log", "selecting");
//				#endif

				select(s);


			}
		}
//		}else{
//			if(manager.board.squareInBounds(s, Direction.getDirection(Direction.NONE))){
//				if(manager.board.hasBarrel(s)){
//					manager.board.kill(s);
//				}else{
//					manager.addBarrel(s);
//				}
//			}
//		}

	

	}

	private bool unitBelongsToPlayer(Square s){
		return manager.board.getPlayerNumber(s)==manager.activePlayer;
	}

	private void select(Square s){
	
		#if UNITY_EDITOR
		//Debug.Log(message, context);
		#else
		Application.ExternalCall("console.log", "started selection stuff");
		#endif


		manager.select();
		selectedUnits.Add(s);
		manager.board.selectSquareContents(s);

		#if UNITY_EDITOR
		//Debug.Log(message, context);
		#else
		Application.ExternalCall("console.log", "completed selection stuff");
		#endif

	}

	private void deselect(Square s){
		selectedUnits.Remove(s);
		manager.board.deselectSquareContents(s);
	}

	void changePlayer(){
		hideSelections();
		clearSelection();
		foreach(Square s in manager.order.getSquares()){
			selectedUnits.Add(s);
		}
		showSelections();
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

	void hideSelections(){
		for(int i = 0; i<selectedUnits.Count; i++){
			manager.board.deselectSquareContents(selectedUnits[i]);
		}
	}

	void showSelections(){

		for(int i = 0; i<selectedUnits.Count; i++){
			manager.board.selectSquareContents(selectedUnits[i]);
		}
	}



	// Update is called once per frame
	public void Update () {
		//Selection must account for subsequent positions
		if(Input.GetMouseButtonDown(0)/* && ( Input.mousePosition())*/){

			#if UNITY_EDITOR
			//Debug.Log(message, context);
			#else
			Application.ExternalCall("console.log", "Click recieved");
			#endif
			
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			
			Square boardPos = manager.board.convertMouseClickToBoardCoords(mousePos);

			#if UNITY_EDITOR
			//Debug.Log(message, context);
			#else
			Application.ExternalCall("console.log", "click at ["+boardPos.x+", "+boardPos.y+"]");
			#endif

			selectOrDeselect(boardPos);

		}
	}
}
