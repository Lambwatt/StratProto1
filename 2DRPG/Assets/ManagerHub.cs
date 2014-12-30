using UnityEngine;
using System.Collections;

public class ManagerHub : MonoBehaviour {

	public Board board;
	public Selector selector;
	public Conductor conductor;
	public int turn = 0;

	public const int maxTurns = 3;

	public delegate void TurnChangeAction(int oldTurn);
	public static event TurnChangeAction onTurnChange;

	// Use this for initialization
	void Awake () {
		board = GetComponent<Board>();
		selector = GetComponent<Selector>();
		conductor = GetComponent<Conductor>();
	}

	public void test(){
		board.sayHi();
	}
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.RightBracket)){
			//Debug.Log ("righted "+(turn+1));
			//assignMoveOrder();//Change this order then move
			Debug.Log ("trun up");
			changeTurn(turn+1);
			
		}
		
		else if(Input.GetKeyDown(KeyCode.LeftBracket)){
			//Debug.Log ("left "+(turn-1));
			//assignMoveOrder();//change this order then move
			Debug.Log ("trun down");
			changeTurn(turn-1);
			
		}

	}

	public void changeTurn(int t){

		//Debug.Log (t);
		if(t < 0 || t >= maxTurns){
			Debug.Log("rejected in manager");
			return;
		}
		
		int oldTurn = turn;	
		turn = t;

		if(onTurnChange!=null){
			onTurnChange(oldTurn);
		}

		Debug.Log("new turn = "+t);
	}
}
