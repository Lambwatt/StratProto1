using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//tracks changes in the board between turns and responds like the real board
public class ScratchBoard : Board  {

	void Awake() {}
	void Start(){}
	void Update(){}

	Board board;
	Dictionary<Square, Square> activeSquares;

	public ScratchBoard(Board b){

		board = b;
		populateActiveSquares(board.getActiveSquares());

	}

	public bool isOccupied(Square s){
		//if(activeSquares.ContainsKey(s)){
			return false;//This will be much more complicated when it starts accounting for movement and archers and stuff
		//}else{
			//return board.squareInBounds(s, );
			//board.moveAllowed(activeSquares);
		//}
	}

	//checks if a square is occupied using active squares for units and the board for the boundary check
	public bool moveAllowed(Square s, Direction d){
//		Square k = new Square(s.x+d.getX(), s.y+d.getY());
//		if(activeSquares.ContainsKey(k)){
			return false;//This will be much more complicated when it starts accounting for movement and archers and stuff
		//}else{
			//return board.squareInBounds();
			//board.moveAllowed(activeSquares);
		//}
	}

	//moves square's contents to another square in the activeSquares dictionary
	public void move(Square start, Direction dir){

		Square end = new Square(start.x+dir.getX(), start.y+dir.getY());
		activeSquares.Add(end, activeSquares[start]);

		if(end.x!=start.x || end.y!=start.y)
			activeSquares.Remove(start);

	}

	//Moves all squares back to their initial positions
	public void reset(){
		List<Square> vals = new List<Square>();

		foreach(Square key in activeSquares.Keys){
			vals.Add(activeSquares[key]);
			activeSquares.Remove(key);
		}

		populateActiveSquares(vals);

		foreach(Square key in activeSquares.Keys){
			board.resetPosition(key);
		}
	}

	public void selectSquareContents(Square s){
		board.selectSquareContents(s);
	}

	public void deselectSquareContents(Square s){
		board.deselectSquareContents(s);
	}

	//places a list of squares in the dictionary
	private void populateActiveSquares(List<Square> squares){
		foreach(Square s in squares)
			activeSquares.Add(s,s);
	}

}
