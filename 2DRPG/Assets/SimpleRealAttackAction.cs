﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleRealAttackAction : Action {

	Square square;
	Direction dir;

	public SimpleRealAttackAction(Square s, Direction d)//Direction was converted in command
	{
		square = s;
		dir = d;
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

		board.setAnimation(square, new SpriteMovement("shootAimed", 
		                                              new LinearMoveCurve(null), 
		                                              board.convertBoardSquaresToWorldCoords(square), 
		                                              board.convertBoardSquaresToWorldCoords(square),
		                                              10));
		board.setAnimation(square, new SpriteMovement("idle", 
		                                              new LinearMoveCurve(null), 
		                                              board.convertBoardSquaresToWorldCoords(square), 
		                                              board.convertBoardSquaresToWorldCoords(square),
		                                              0));

		if(dir.equals(Direction.getDirection(Direction.NONE)))//you can't shoot yourself
			return 11;

		//Debug.Log ();

		Square result = checkSquare(board, data, 1, new Square(square.x+dir.getX(), square.y+dir.getY()));
			
		if(result.Equals(Square.getNullSquare())){
			return 11;//return length of shoot
		}else{

			GameObject smoke = GameObject.Instantiate(Resources.Load<GameObject>("GunFire"), board.convertBoardSquaresToWorldCoords(square), Quaternion.identity) as GameObject;

			GameObject bullet = GameObject.Instantiate(Resources.Load<GameObject>("Bullet"), 
			                                           board.convertBoardSquaresToWorldCoords(square), 
			                                           Quaternion.Euler(new Vector3(0,0,(Mathf.Rad2Deg*Mathf.Atan2(result.y-square.y, result.x-square.x))-45))
			                             )as GameObject;

			bullet.GetComponent<Bullet>().setCourse(new SpriteMovement(null, 
			                                                       new LinearMoveCurve(null), 
			                                                       board.convertBoardSquaresToWorldCoords(square), 
			                                                       board.convertBoardSquaresToWorldCoords(result),
			                                                           3), 3, board.convertBoardSquaresToWorldCoords(result), board.hasBarrel(result));
				
			

			bool dead = board.applyAttackDamage(square, result);
			if(dead){
				board.replaceAnimation(result, new SpriteMovement("die", 
			                                              new LinearMoveCurve(null), 
			                                              board.convertBoardSquaresToWorldCoords(result), 
			                                              board.convertBoardSquaresToWorldCoords(result),
			                                              20));
				board.kill(result);
				return 11;//return get shot + die time
			}else{
				board.replaceAnimation(result, new SpriteMovement("hit", 
				                                              new LinearMoveCurve(null), 
				                                              board.convertBoardSquaresToWorldCoords(result), 
				                                              board.convertBoardSquaresToWorldCoords(result),
				                                              10));
				board.setAnimation(result, new SpriteMovement("idle", 
				                                              new LinearMoveCurve(null), 
				                                              board.convertBoardSquaresToWorldCoords(result), 
				                                              board.convertBoardSquaresToWorldCoords(result),
				                                              0));
				return 11;//return get shot + get hit time
			}
		}
	}
	
	public void checkForConsequences(Board board, TurnMetaData data){
		//Do nothing, no consequences can be tripped
	}
	
	public int applyConsequences(Board board, TurnMetaData data){
		//do nothing, no consequences can be tripped
		return 10;
	}

	public Square checkSquare(Board b, TurnMetaData d, int count, Square s){

		if(b.isOccupied(s)){//square occupied

			if(b.getPlayerNumber(s)==d.getActivePlayer()){//unit is friend

				if(false){//friendly fire on
					return Square.getNullSquare();
				}else{ //friendly fire off
					return s;
				}
			}else//unit is foe
				return s;
		}else{//square empty

			if(count>=b.getRange(square)){//range exhausted

				return Square.getNullSquare();//missed
			}else{//range not reached

				return checkSquare(b, d, count+1, new Square(s.x+dir.getX(), s.y+dir.getY()));//keep going
			}
		}
	}
}
