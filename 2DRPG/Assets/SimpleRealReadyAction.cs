using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleRealReadyAction : Action {

	Square square;
	
	public SimpleRealReadyAction(Square s)//Final version should add a direction back in.
	{
		square = s;
		//range = r;
	}
	
	//Executable if a unit belonging to the player is in the square
	public List<Action> checkIfExecutable(Board board, TurnMetaData data){
		
		if(board.isOccupied(square) && board.getPlayerNumber(square)==data.getActivePlayer()){
			return null;
		}else{
			List<Action> res =  new List<Action>();
			res.Add(new EmptyAction());
			return res;
		}
		
	}
	
	public int execute(Board board, TurnMetaData data){

		data.postReady(square); //Do something with the data
		
		board.setAnimation(square, new SpriteMovement("ready", 
		                                              new LinearMoveCurve(null), 
		                                              board.convertBoardSquaresToWorldCoords(square), 
		                                              board.convertBoardSquaresToWorldCoords(square),
		                                              10));
		return 10;
	}
	
	public void checkForConsequences(Board board, TurnMetaData data){

		int range = board.getRange(square);
		for(int i = 0; i<(2*range)+1; i++){
			for(int j = 0; j<(2*range)+1; j++){
				Square testSquare = new Square(square.x-range+i, square.y-range+j);
				if(board.squareInBounds(testSquare, Direction.getDirection(Direction.NONE))){
					if(board.isOccupied(testSquare) && board.getPlayerNumber(testSquare)!=data.getActivePlayer()){
						data.trip(square, testSquare);
					}
				}
			}
		}
	}
	
	public int applyConsequences(Board board, TurnMetaData data){

		if(data.hasTarget(square)){

			List<Square> targets = data.getTargets(square);

			data.cancelReadiness(square);

			Square target;
			if(targets.Count == 1)
				target = targets[0];
			else{
				target = targets[0];
				foreach(Square t in targets){
					if(Mathf.Abs(t.x-square.x)<=Mathf.Abs(target.x-square.x) && Mathf.Abs(t.y-square.y)<=Mathf.Abs(target.y-square.y) ){
						target = t;
					}
				}
			}

			board.setAnimation(square, new SpriteMovement("shootReadied", 
			                                              new LinearMoveCurve(null), 
			                                              board.convertBoardSquaresToWorldCoords(square), 
			                                              board.convertBoardSquaresToWorldCoords(square),
			                                              10));

			bool dead = board.applyReadyDamage(square, target);
			if(dead){
				board.setAnimation(target, new SpriteMovement("die", 
				                                              new LinearMoveCurve(null), 
				                                              board.convertBoardSquaresToWorldCoords(target), 
				                                              board.convertBoardSquaresToWorldCoords(target),
				                                             10));
				board.kill(target);
				return 10;//return get shot + die time
			}else{
				board.setAnimation(target, new SpriteMovement("hit", 
				                                              new LinearMoveCurve(null), 
				                                              board.convertBoardSquaresToWorldCoords(target), 
				                                              board.convertBoardSquaresToWorldCoords(target),
				                                              10));
				return 10;//return get shot + get hit time
			}
		}else
			return 0;
	}
}
