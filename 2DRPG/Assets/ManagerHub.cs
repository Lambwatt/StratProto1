using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Initializes everything and provides a commo reference point. also holds any global variables necessary.

public class ManagerHub : MonoBehaviour {

	public Board realBoard;
	public ScratchBoard scratchBoard;
	public Selector selector;
	public Conductor conductor;
	public Resolver resolver;
	public CommandFactory commandFactory;
	public int turn = 0;
	public Order order;
	//public string state = "planning";
	public Board board;

	public const int maxTurns = 3;

	public delegate void TurnChangeAction(int oldTurn);
	public static event TurnChangeAction onTurnChange;

	public delegate void PlayAnimationAction();
	public static event PlayAnimationAction onAnimationPlay;

	// Use this for initialization
	void Awake () {
		realBoard = GetComponent<Board>();
		scratchBoard = new ScratchBoard(realBoard);
		board = realBoard;
		commandFactory = new SimpleCommandFactory();
		order = new Order(commandFactory);
		selector = GetComponent<Selector>();
		conductor = GetComponent<Conductor>();
		resolver = new Resolver();;

//		Dictionary<Square, string> test = new Dictionary<Square, string>();
//
//		test.Add(new Square(0,0),"This square started [0,0].");
//		test.Add(new Square(0,1),"This square started [0,1].");
//
//		Debug.Log(test[new Square(0,0)]);
//		Debug.Log(test[new Square(0,1)]);
//
//		test.Add(new Square(1,0),test[new Square(0,1)]);
//		test.Remove(new Square(0,1));
//
//		Debug.Log(test[new Square(1,0)]);
//		Debug.Log(test[new Square(0,1)]);
		//test.Add(Square(0,0),"This square started [0,0].");

	}

	public void resolve(){
		order.setSquares(selector.selectedUnits);
		resolver.resolve(board, order);
		onAnimationPlay();
	}


//	public void test(){
////		realBoard.sayHi();
//	}
	// Update is called once per frame
	void Update () {

//		if(Input.GetKeyDown(KeyCode.RightBracket)){
//			//Debug.Log ("righted "+(turn+1));
//			//assignMoveOrder();//Change this order then move
//			..Debug.Log ("trun up");
//			//changeTurn(turn+1);
//			
//		}
//		
//		else if(Input.GetKeyDown(KeyCode.LeftBracket)){
//			//Debug.Log ("left "+(turn-1));
//			//assignMoveOrder();//change this order then move
//			Debug.Log ("trun down");
//			//changeTurn(turn-1);
//			
//		}else 
//		if(Input.GetKeyDown(KeyCode.Return)) {
//
////			if(onAnimationPlay!=null){
////				onAnimationPlay();
////			}
//			resolver()
//			//state = "resolving";
//			//TRIGGIR MOVEMENT
//
//			//turn = 0;
//		}

	}

//	public void changeTurn(int t){
//
//		//Debug.Log (t);
//		if(t < 0 || t >= maxTurns){
//			Debug.Log("rejected in manager");
//			return;
//		}
//		
//		int oldTurn = turn;	
//		turn = t;
//
//		if(onTurnChange!=null){
//			onTurnChange(oldTurn);
//		}
//
//		Debug.Log("new turn = "+t);
//	}
}
