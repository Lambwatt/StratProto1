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
						Debug.Log ("["+square.x+","+square.y+"] to ["+testSquare.x+","+testSquare.y+"]");
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
			Debug.Log ("Targets: "+targets.Count);
			Square target;
			if(targets.Count == 1)
				target = targets[0];
			else{

				//select a non barrel initial target. 
				int i = 0;
				do{
					target = targets[i];
					i++;
				}while(board.hasBarrel(target) && i<targets.Count);

				if(board.hasBarrel(target))//No the 
					return 0;

				foreach(Square t in targets){
					//Debug.Log ("for ["+t.x+", "+t.y+"] !board.hasBarrel(target) = "+!board.hasBarrel(t));
					if(Mathf.Abs(t.x-square.x)<=Mathf.Abs(target.x-square.x) && Mathf.Abs(t.y-square.y)<=Mathf.Abs(target.y-square.y) && !board.hasBarrel(t)){
						target = t;
						//Debug.Log ("switched to targeting ["+t.x+", "+t.y+"] because !board.hasBarrel(target) = "+!board.hasBarrel(target));
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
