﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleRealMoveAction : Action{

	Square square;
	Square oldSquare;
	Direction dir;
//	bool isFirst;
	bool isLast;

	public SimpleRealMoveAction(Square s, Direction d, bool l)//Direction was converted in command
	{
		square = s;
		dir = d;
//		isFirst = f;
		isLast = l;
	}

	private bool postCanMove(Board board, TurnMetaData data){

		if(board.isOccupied(square) && board.getPlayerNumber(square)==data.getActivePlayer()){
			TurnMetaData.Answer ans = board.moveAllowed(square, dir);
			data.postMoving(square,ans);
			return ans != TurnMetaData.Answer.NO;
		}else return false;
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

	public List<Action> checkIfExecutable(Board board, TurnMetaData data){
		if(postCanMove(board, data))
			return null;//success state.
		else{
			if(board.isOccupied(square)){
				List<Action> res =  new List<Action>();
				res.Add(new SimpleRealMoveAction(square, Direction.getDirection(Direction.NONE), true));
				return res;
			}else{
				List<Action> res =  new List<Action>();
				res.Add(new EmptyAction());
				return res;
			}
		}

	}

	public int execute(Board board, TurnMetaData data){

		if(willMove(square, data)){

			data.updateMoving(square, true);
			board.setAnimation(square, new SpriteMovement("idle", 
							              			new LinearMoveCurve(null), 
							              			board.convertBoardSquaresToWorldCoords(square), 
							              			board.convertBoardSquaresToWorldCoords(new Square(square.x+dir.getX(),square.y+dir.getY())),
							                        10));
			if(isLast){
				board.setAnimation(square, new SpriteMovement("idle", 
			                                         	new LinearMoveCurve(null), 
			                                        	 board.convertBoardSquaresToWorldCoords(new Square(square.x+dir.getX(),square.y+dir.getY())), 
			                                        	 board.convertBoardSquaresToWorldCoords(new Square(square.x+dir.getX(),square.y+dir.getY())),
			                                         	 0));
			}
			board.move(square, dir);
			oldSquare = new Square(square.x, square.y);
			square = new Square(square.x+dir.getX(), square.y+dir.getY());
			
		}else{
			data.updateMoving(square, false);//Simplifies future data queries.
		}

		return isLast ? 11 : 10;
	}

	public void checkForConsequences(Board board, TurnMetaData data){
		List<Square> shooters = data.getAllShooters();
		//Debug.Log ("num shooters: "+shooters.Count);
		foreach(Square s in shooters){
			//Debug.Log ("Testing : "+Mathf.Abs(s.x-square.x)+"<"+board.getRange(s)+" && "+Mathf.Abs(s.y-square.y)+"<"+board.getRange(s)+" = "+(Mathf.Abs(s.x-square.x)<board.getRange(s) && Mathf.Abs(s.y-square.y)<board.getRange(s)));
			if(Mathf.Abs(s.x-square.x)<board.getRange(s) && Mathf.Abs(s.y-square.y)<board.getRange(s)){
				//Debug.Log ("Tripped");
				data.trip(s,square);
				//Debug.Log ("Tripped "+s+". ["+Mathf.Abs(s.x-square.x)+"<"+board.getRange(s)+"||"+Mathf.Abs(s.y-square.y)+"<"+board.getRange(s)+":"+(Mathf.Abs(s.x-square.x)<board.getRange(s) && Mathf.Abs(s.y-square.y)<board.getRange(s))+"]");
			}
		}

	}
	
	public int applyConsequences(Board board, TurnMetaData data){

		int res = 0;
		if(data.isTarget(square)){
			//Debug.Log ("\twas target");
			List<Square> shooters = data.getMyShooters(square);
			foreach(Square s in shooters){
				//Debug.Log ("\tchecking against ["+s.x+","+s.y+"]");
				if(isTarget(data.getTargets(s))){
					//Debug.Log ("\twas targeted");
					data.cancelReadiness(s);
					int newRes = getShotBy(board, s);
					if(newRes>res){
						//Debug.Log ("\tdied");
						res = newRes;
					}
				}
			}
		}
		return res;
	}

	private bool isTarget(List<Square> targets){
		Square closest = targets[0];
		foreach(Square s in targets){
			if(Mathf.Abs(s.x-square.x)<=Mathf.Abs(s.x-closest.x) && Mathf.Abs(s.y-square.y)<=Mathf.Abs(s.y-closest.y)){
				closest = s;
			}
		}
		return closest.Equals(square);
	}

	private int getShotBy(Board board, Square shooter){
		board.setAnimation(shooter, new SpriteMovement("shootReadied", 
		                                              new LinearMoveCurve(null), 
		                                              board.convertBoardSquaresToWorldCoords(shooter), 
		                                              board.convertBoardSquaresToWorldCoords(shooter),
		                                              10));
		board.setAnimation(shooter, new SpriteMovement("idle", 
		                                              new LinearMoveCurve(null), 
		                                              board.convertBoardSquaresToWorldCoords(shooter), 
		                                              board.convertBoardSquaresToWorldCoords(shooter),
		                                              0));

		GameObject smoke = GameObject.Instantiate(Resources.Load<GameObject>("GunFire"), board.convertBoardSquaresToWorldCoords(shooter), Quaternion.identity) as GameObject;
		
		GameObject bullet = GameObject.Instantiate(Resources.Load<GameObject>("Bullet"), 
		                                           board.convertBoardSquaresToWorldCoords(square), 
		                                           Quaternion.Euler(new Vector3(0,0,(Mathf.Rad2Deg*Mathf.Atan2(shooter.y-square.y, shooter.x-square.x))-45))
		                                           )as GameObject;
		
		bullet.GetComponent<Bullet>().setCourse(new SpriteMovement(null, 
		                                                           new LinearMoveCurve(null), 
		                                                           board.convertBoardSquaresToWorldCoords(shooter), 
		                                                           board.convertBoardSquaresToWorldCoords(square),
		                                                           3), 3, board.convertBoardSquaresToWorldCoords(square), false);

		bool dead = board.applyReadyDamage(shooter, square);
		if(dead){
			board.replaceAnimation(square, new SpriteMovement("die", 
			                                              new LinearMoveCurve(null), 
			                                              board.convertBoardSquaresToWorldCoords(square), 
			                                              board.convertBoardSquaresToWorldCoords(square),
			                                              21));
			board.kill(square);
			return 11;//return get shot + die time
		}else{
			board.replaceAnimation(square, new SpriteMovement("hit", 
			                                              new LinearMoveCurve(null), 
			                                              board.convertBoardSquaresToWorldCoords(oldSquare), 
			                                              board.convertBoardSquaresToWorldCoords(square),
			                                              10));
			return 11;//return get shot + get hit time
		}
	}
}


