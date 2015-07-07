using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnMetaData {

	public enum Answer{NO, YES, MAYBE};

	private Dictionary<Square, Answer> moving;
	private int activePlayer;

	public TurnMetaData(int ap){
		moving = new Dictionary<Square, Answer>();
		activePlayer = ap;
	}

	public void postMoving(Square s, Answer a){
		moving.Add(s, a);
	}

	public void updateMoving(Square s, bool b){
		if(moving.ContainsKey(s)){
			if(b){
				moving[s]=Answer.YES;
			}else{
				moving[s]=Answer.NO;
			}
		}else{
			Debug.Log("WARNING: updated empty meta data entry");
		}
	}
	
	public Answer getMoving(Square s){
		if(moving.ContainsKey(s))
			return moving[s];
		else
			return Answer.NO;
	}

	public void clearData(){
		moving.Clear();
	}

	public int getActivePlayer(){
		return activePlayer;
	}
}
