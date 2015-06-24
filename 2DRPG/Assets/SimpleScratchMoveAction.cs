using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleScratchMoveAction : Action {

	Square square;
	Direction dir;
	
	public SimpleScratchMoveAction(Square s, Direction d){//Direction was converted in command

		square = s;
		dir = d;

	}

	private TurnMetaData.Answer canMove(Board board){
		return board.moveAllowed(square, dir);
	}
	
	private TurnMetaData.Answer willMove(Board board){
		return board.moveAllowed(square, dir);
	}

	public void checkIfExecutable(Board board, TurnMetaData dat){

		//Do nothing for now
	}
	
	public int execute(Board board, TurnMetaData dat){
		
		List<Response> res = new List<Response>();

//		if(board.moveAllowed(square, dir)){
//
//			board.move(square, dir);
//
//		}

		return 0;
	}
	
	public void checkForConsequences(Board board){

		// do nothing
	}
	
	public void applyConsequences(Board board){

		// do nothing
	}
}
