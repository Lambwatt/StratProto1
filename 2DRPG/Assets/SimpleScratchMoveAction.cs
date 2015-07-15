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

	public List<Action> checkIfExecutable(Board board, TurnMetaData data){
		return null;
		//Do nothing for now
	}
	
	public int execute(Board board, TurnMetaData data){
		
		List<Response> res = new List<Response>();

//		if(board.moveAllowed(square, dir)){
//
//			board.move(square, dir);
//
//		}

		return 0;
	}
	
	public void checkForConsequences(Board board, TurnMetaData data){

		// do nothing
	}
	
	public int applyConsequences(Board board, TurnMetaData data){
		return 0;
		// do nothing
	}
}
