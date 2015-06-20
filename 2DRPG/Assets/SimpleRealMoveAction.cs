using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleRealMoveAction : Action{

	Square square;
	Direction dir;
//	bool isFirst;
//	bool isLast;

	public SimpleRealMoveAction(Square s, Direction d)//Direction was converted in command
	{
		square = s;
		dir = d;
//		isFirst = f;
//		isLast = l;
	}

	private bool postCanMove(Board board, TurnMetaData data){

		TurnMetaData.Answer ans = board.moveAllowed(square, dir);
		data.postMoving(square,ans);
		return ans != TurnMetaData.Answer.NO || dir == Direction.getDirection(Direction.NONE);//May want to handle this differently, but for now moving nowhere ends in failure to avoid an infinite loop.
	}

	public bool willMove(Square square, TurnMetaData data){
		switch( data.getMoving(square)){
		case TurnMetaData.Answer.NO:
			return false;
		case TurnMetaData.Answer.YES:
			return true;
		case TurnMetaData.Answer.MAYBE:
			return willMove(new Square(square.x+dir.getX(), square.y+dir.getY()), data);
				default:
				Debug.Log("WARNING: recieved non-answer.");
			return false;
		}
	}

	public void checkIfExecutable(Board board, TurnMetaData data){
		postCanMove(board, data);
	}

	public List<Response> execute(Board board, TurnMetaData data){
		
		List<Response> res = new List<Response>();
		
		if(willMove(square, data)){

			data.updateMoving(square, true);
			board.move(square, dir);
			
		}else{
			data.updateMoving(square, false);//Simplifies future data queries.
		}

		return null;
	}

	public void checkForConsequences(Board board){
		
	}
	
	public void applyConsequences(Board board){
		
	}



}
