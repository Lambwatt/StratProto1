using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnMetaData {

	public enum Answer{NO, YES, MAYBE};

	private Dictionary<Square, Answer> moving;
	private List<Square> ready;
	private Dictionary<Square, List<Square>> tripped;
	private int activePlayer;
	private int remainingTurns;

	public TurnMetaData(int ap, int rt){
		moving = new Dictionary<Square, Answer>();
		ready = new List<Square>();
		tripped = new Dictionary<Square, List<Square>>();
		activePlayer = ap;
		remainingTurns = rt;
		//Debug.Log ("remaining turns = "+remainingTurns);
	}

	public int getRemainingTurn(){
		return remainingTurns;
	}

	public void postReady(Square s){
		Debug.Log ("Posted Square:["+s.x+","+s.y+"]");
		ready.Add(s);
		Debug.Log("ready now has "+ready.Count+" entries.");
	}

	public List<Square> getAllShooters(){
		Debug.Log("returning ready with "+ready.Count+" entries.");
		return ready;
	}

	public void trip(Square shooter, Square target){
		if(!tripped.ContainsKey(shooter))
			tripped.Add(shooter, new List<Square>());
		tripped[shooter].Add(target);
	}

	public bool hasTarget(Square s){
		return tripped.ContainsKey (s);
	}

	public bool isTarget(Square s){
		foreach(Square t in tripped.Keys){
			if(tripped[t].Contains(s))
				return true;
		}
		return false;
	}

	public List<Square> getMyShooters(Square s){
		List<Square> res = new List<Square>();
		foreach(Square t in tripped.Keys){
			if(tripped[t].Contains(s))
				res.Add(t);
		}
		return res;
	}

	public List<Square> getTargets(Square s){
		return tripped[s];
	}

	public void cancelReadiness(Square s){
		Debug.Log("Remove ["+s.x+","+s.y+"]");
		ready.Remove(s);
		Debug.Log("After removal, ready has "+ready.Count+"entries");
		if(hasTarget(s))
			tripped.Remove(s);
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
		//tripped.Clear(); might be ncessary. requires testing
	}

	public int getActivePlayer(){
		return activePlayer;
	}

	public static void copyData(TurnMetaData source, TurnMetaData dest){
		List<Square> r = source.getAllShooters();
		Debug.Log ("copying "+r.Count+" squares");
		foreach(Square s in r){
			dest.postReady(s);
			Debug.Log ("Square:["+s.x+","+s.y+"]");
		}
	}
}
