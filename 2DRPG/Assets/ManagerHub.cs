﻿using UnityEngine;
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
	public int activePlayer;
	public Order order;
	public string state = "planning";
	public Board board;
	public int frameCount = 0;
	public int minMagnitude = 1;
	public int maxMagnitude = 3;
	public int unitsPerPlayer = 0;
	public const int maxTurns = 3;

//	public delegate void TurnChangeAction(int oldTurn);
//	public static event TurnChangeAction onTurnChange;

	public delegate void PlayAnimationAction();
	public static event PlayAnimationAction onAnimationPlay;

	public delegate void PlayerChangeAction();
	public static event PlayerChangeAction onPlayerChange;

	private int firstPlayer = 0;
	private int orderCount = 2;
	private int ordersRun = 0;
	private int resolvingPlayer = 0;

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

		selector = GetComponent<Selector>();
		conductor = GetComponent<Conductor>();
		resolver = new Resolver();

	}

	void Start(){

		unitsPerPlayer = board.width*board.height/2<unitsPerPlayer ? board.width*board.height/2 : unitsPerPlayer;

		for(int i = 0; i<players.Length; i++){
			for(int j = 0; j<unitsPerPlayer; j++){
				addUnit(i,board.getFreeSquare());
			}
		}

		activePlayer = 0;
		order = players[0].getOrder();
//		onPlayerChange();

	}

	public void changePlayer(){
		order.setSquares(selector.selectedUnits);
		activePlayer = (activePlayer+1)%2;
		order = players[activePlayer].getOrder();
		//selector.Reselect(order.getSquares);//Need to figure this part out.
		onPlayerChange();
	}

	private void addUnit(int p, Square s){

		GameObject unit = initializeUnit(p);

		if(board.register(unit, s))
			unit.GetComponent<Movement>().setPosition(board.convertBoardSquaresToWorldCoords(s));

		else
			Debug.Log ("Error: could not place unit for player "+p+" at ["+s.x+","+s.y+"] because space was occuied");

	}

	private GameObject initializeUnit(int player){
		GameObject unit = Instantiate<GameObject>(Resources.Load<GameObject>("PlayerPrefab")) as GameObject;
		unit.GetComponent<SpriteRenderer>().sprite = players[player].getSprite();
		unit.GetComponent<Movement>().setPlayerNumber(player);
		return unit;
	}
	
	private Order initializeOrders(CommandFactory commandFactory){
		return new Order(commandFactory);
	}
	

	public void resolve(){
		order.setSquares(selector.selectedUnits);
		frameCount = resolver.resolve(board, order, resolvingPlayer);
		players[resolvingPlayer].setOrder(initializeOrders(commandFactory));
		resolvingPlayer = (resolvingPlayer+1)%2;
		order = players[resolvingPlayer].getOrder();
		ordersRun++;
		//players[1].setOrder(initializeOrders(commandFactory));
		//Old order has been used. Not longer needs to be preserved
		onAnimationPlay();
		state = "animating";
	}

	void Update () {

		if(state=="animating"){
			frameCount--;
			if(frameCount<0){
				if(ordersRun == orderCount){
					ordersRun = 0;
					firstPlayer = (firstPlayer+1)%2;
					resolvingPlayer = firstPlayer;
					state = "planning";
				}
				else{
					resolve();
				}
			}
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
