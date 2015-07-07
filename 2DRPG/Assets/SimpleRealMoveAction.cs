﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleRealMoveAction : Action{

	Square square;
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
			board.setAnimation(square, new SpriteMovement("move", 
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
			
		}else{
			data.updateMoving(square, false);//Simplifies future data queries.
		}

		return isLast ? 11 : 10;
	}

	public void checkForConsequences(Board board){
		
	}
	
	public void applyConsequences(Board board){
		
	}



}
