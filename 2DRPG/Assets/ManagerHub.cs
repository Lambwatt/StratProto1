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
	public Player[] players;
//	public Player player1;
//	public Player player2;
	public Order order;
	public string state = "planning";
	public Board board;
	public int frameCount = 0;
	public int minMagnitude = 1;
	public int maxMagnitude = 3;

	public const int maxTurns = 3;

//	public delegate void TurnChangeAction(int oldTurn);
//	public static event TurnChangeAction onTurnChange;

	public delegate void PlayAnimationAction();
	public static event PlayAnimationAction onAnimationPlay;

	// Use this for initialization
	void Awake () {
		realBoard = GetComponent<Board>();
		//scratchBoard = new ScratchBoard(realBoard);
		board = realBoard;
		commandFactory = new SimpleCommandFactory();

		players = new Player[]{	new Player(0, Resources.Load<Sprite>("player1")),
								new Player(1, Resources.Load<Sprite>("player2"))};

		players[0].setOrder(initializeOrders(commandFactory));
		players[1].setOrder(initializeOrders(commandFactory));



		order = players[0].getOrder();
		selector = GetComponent<Selector>();
		conductor = GetComponent<Conductor>();
		resolver = new Resolver();

	}

	void Start(){
		addUnit(0,0,0);
		addUnit(1,1,1);

	}

	private void addUnit(int p, int x, int y){
		GameObject unit = initializeUnit(p);
		if(board.register(unit, x, y)){
			unit.GetComponent<Movement>().setPosition(board.convertBoardSquaresToWorldCoords(new Square(x,y)));
		}else{
			Debug.Log ("Error: could not place unit for player "+p+" at ["+x+","+y+"] because space was occuied");
		}
	}

	private GameObject initializeUnit(int player){
		GameObject unit = Instantiate<GameObject>(Resources.Load<GameObject>("PlayerPrefab")) as GameObject;
		unit.GetComponent<SpriteRenderer>().sprite = players[player].getSprite();
		return unit;
	}
	
	private Order initializeOrders(CommandFactory commandFactory){
		return new Order(commandFactory);
	}
	

	public void resolve(){
		order.setSquares(selector.selectedUnits);
		frameCount = resolver.resolve(board, order);
		players[0].setOrder(initializeOrders(commandFactory));
		players[0].setOrder(initializeOrders(commandFactory));
		order = players[0].getOrder();//Old order has been used. Not longer needs to be preserved
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
