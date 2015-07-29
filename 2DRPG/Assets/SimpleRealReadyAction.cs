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
		                                              10*data.getRemainingTurn()));
		board.setAnimation(square, new SpriteMovement("idle", 
		                                              new LinearMoveCurve(null), 
		                                              board.convertBoardSquaresToWorldCoords(square), 
		                                              board.convertBoardSquaresToWorldCoords(square),
		                                              0));
		return 11;
	}
	
	public void checkForConsequences(Board board, TurnMetaData data){

		int range = board.getRange(square);
		for(int i = 0; i<(2*range)+1; i++){
			for(int j = 0; j<(2*range)+1; j++){
				Square testSquare = new Square(square.x-range+i, square.y-range+j);
				if(board.squareInBounds(testSquare, Direction.getDirection(Direction.NONE))){
					if(board.isOccupied(testSquare) && board.getPlayerNumber(testSquare)!=data.getActivePlayer()){
						//Debug.Log ("["+square.x+","+square.y+"] to ["+testSquare.x+","+testSquare.y+"]");
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
			//Debug.Log ("Targets: "+targets.Count);
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
					return 10;

				foreach(Square t in targets){
					//Debug.Log ("for ["+t.x+", "+t.y+"] !board.hasBarrel(target) = "+!board.hasBarrel(t));
					if(Mathf.Abs(t.x-square.x)<=Mathf.Abs(target.x-square.x) && Mathf.Abs(t.y-square.y)<=Mathf.Abs(target.y-square.y) && !board.hasBarrel(t)){
						target = t;
						//Debug.Log ("switched to targeting ["+t.x+", "+t.y+"] because !board.hasBarrel(target) = "+!board.hasBarrel(target));
					}
				}
			}

			if(board.unitHasCover(square, target)){
				Debug.Log ("Detected cover.");

				Square result = board.getLastBarrel();

				board.setAnimation(square, new SpriteMovement("shootReadied", 
				                                              new LinearMoveCurve(null), 
				                                              board.convertBoardSquaresToWorldCoords(square), 
				                                              board.convertBoardSquaresToWorldCoords(square),
				                                              10));
				board.setAnimation(square, new SpriteMovement("idle", 
				                                              new LinearMoveCurve(null), 
				                                              board.convertBoardSquaresToWorldCoords(square), 
				                                              board.convertBoardSquaresToWorldCoords(square),
				                                              0));

				GameObject smoke = GameObject.Instantiate(Resources.Load<GameObject>("GunFire"), board.convertBoardSquaresToWorldCoords(square), Quaternion.identity) as GameObject;
				
				GameObject bullet = GameObject.Instantiate(Resources.Load<GameObject>("Bullet"), 
				                                           board.convertBoardSquaresToWorldCoords(square), 
				                                           Quaternion.Euler(new Vector3(0,0,(Mathf.Rad2Deg*Mathf.Atan2(result.y-square.y, result.x-square.x))-45))
				                                           )as GameObject;
				
				bullet.GetComponent<Bullet>().setCourse(new SpriteMovement(null, 
				                                                           new LinearMoveCurve(null), 
				                                                           board.convertBoardSquaresToWorldCoords(square), 
				                                                           board.convertBoardSquaresToWorldCoords(result),
				                                                           3), 3, board.convertBoardSquaresToWorldCoords(result), false);

				return 11;
			}else{

				GameObject smoke = GameObject.Instantiate(Resources.Load<GameObject>("GunFire"), board.convertBoardSquaresToWorldCoords(square), Quaternion.identity) as GameObject;
				
				GameObject bullet = GameObject.Instantiate(Resources.Load<GameObject>("Bullet"), 
				                                           board.convertBoardSquaresToWorldCoords(target), 
				                                           Quaternion.Euler(new Vector3(0,0,(Mathf.Rad2Deg*Mathf.Atan2(target.y-square.y, target.x-square.x))-45))
				                                           )as GameObject;
				
				bullet.GetComponent<Bullet>().setCourse(new SpriteMovement(null, 
				                                                           new LinearMoveCurve(null), 
				                                                           board.convertBoardSquaresToWorldCoords(square), 
				                                                           board.convertBoardSquaresToWorldCoords(target),
				                                                           3), 3, board.convertBoardSquaresToWorldCoords(target), false);

				board.setAnimation(square, new SpriteMovement("shootReadied", 
				                                              new LinearMoveCurve(null), 
				                                              board.convertBoardSquaresToWorldCoords(square), 
				                                              board.convertBoardSquaresToWorldCoords(square),
				                                              10));
				board.setAnimation(square, new SpriteMovement("idle", 
				                                              new LinearMoveCurve(null), 
				                                              board.convertBoardSquaresToWorldCoords(square), 
				                                              board.convertBoardSquaresToWorldCoords(square),
				                                              0));

				bool dead = board.applyReadyDamage(square, target);
				if(dead){
					board.replaceAnimation(target, new SpriteMovement("die", 
					                                              new LinearMoveCurve(null), 
					                                              board.convertBoardSquaresToWorldCoords(target), 
					                                              board.convertBoardSquaresToWorldCoords(target),
					                                             21));
					board.kill(target);
					return 11;//return get shot + die time
				}else{
					board.replaceAnimation(target, new SpriteMovement("hit", 
					                                              new LinearMoveCurve(null), 
					                                              board.convertBoardSquaresToWorldCoords(target), 
					                                              board.convertBoardSquaresToWorldCoords(target),
					                                              10));
					board.setAnimation(target, new SpriteMovement("idle", 
					                                              new LinearMoveCurve(null), 
					                                              board.convertBoardSquaresToWorldCoords(target), 
					                                              board.convertBoardSquaresToWorldCoords(target),
					                                              0));
					return 11;//return get shot + get hit time
				}
			}

		}else
			return 10;
	}
}
