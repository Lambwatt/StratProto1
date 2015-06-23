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
	public string state = "planning";
	public Board board;
	public int frameCount = 0;

	public const int maxTurns = 3;

	public delegate void TurnChangeAction(int oldTurn);
	public static event TurnChangeAction onTurnChange;

	public delegate void PlayAnimationAction();
	public static event PlayAnimationAction onAnimationPlay;

	// Use this for initialization
	void Awake () {
		realBoard = GetComponent<Board>();
		//scratchBoard = new ScratchBoard(realBoard);
		board = realBoard;
		commandFactory = new SimpleCommandFactory();
		order = new Order(commandFactory);
		selector = GetComponent<Selector>();
		conductor = GetComponent<Conductor>();
		resolver = new Resolver();

	}

	public void resolve(){
		order.setSquares(selector.selectedUnits);
		frameCount = resolver.resolve(board, order);
		onAnimationPlay();
		state = "animating";
	}

	void Update () {

		if(state=="animating"){
			frameCount--;
			if(frameCount<0)
				state = "planning";
		}
	}
//
//	public void setFrameCount(int i){
//		frameCount = i;
//	}

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
