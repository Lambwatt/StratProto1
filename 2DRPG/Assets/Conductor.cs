using UnityEngine;
using System.Collections;

//Also manages orders. Combine with movement and resolver in replacement structure. Also holds a lot of information that belongs in a seperate table.
//This class will become the UIDriver

//public class DamageOrder{
//	
////	public MoveOrder movement;
//	public Vector3 direction;
//	public int damage;
	
//	public DamageOrder(MoveOrder movement, int damage){
//		this.movement = movement; 
//		this.damage = damage;
//		this.direction = movement.direction;
//	}
	
//	public string toString(){
//		
//		return "Move "+label;
//	}
//}

//public class MoveOrder{
//
//	public Vector2 direction;
//	public string label;
//	
//	public MoveOrder(Direction direction){
//
//		switch(direction){
//		case Direction.NONE:
//			label = "NONE";
//			this.direction = new Vector2(0,0);
//			break;
//		case Direction.NORTHEAST:
//			label = "NORTHEAST";
//			this.direction = Vector2.right + Vector2.up; 
//			break;
//		case Direction.EAST:
//			label = "EAST";
//			this.direction = Vector2.right;
//			break;
//		case Direction.SOUTHEAST:
//			label = "SOUTHEAST";
//			this.direction = Vector2.right + (-1*Vector2.up);
//			break;
//		case Direction.SOUTH:
//			label = "SOUTH";
//			this.direction = -1*Vector2.up;
//			break;
//		case Direction.SOUTHWEST:
//			label = "SOUTHWEST";
//			this.direction = -1*Vector2.right + (-1*Vector2.up);
//			break;
//		case Direction.WEST:
//			label = "WEST";
//			this.direction = -1*Vector2.right;
//			break;
//		case Direction.NORTHWEST:
//			label = "NORTHWEST";
//			this.direction = -1*Vector2.right + Vector2.up;
//			break;
//		case Direction.NORTH:
//			label = "NORTH";
//			this.direction = Vector2.up;
//			break;
//		default:
//			break;
//		}
//
//		//this.direction = direction;
//	}
//	
//	public void apply(Transform subject) {
//
//		subject.position += (Vector3) direction;
//
//	}
//	
//	public void undo(Transform subject) {
//
//		subject.position -= (Vector3) direction;
//
//	}
//
//	public string toString(){
//
//		return "Move "+label;
//	}
//}

public class Conductor : MonoBehaviour {

	//These will be needed later
	//private Direction direction;
	private int magnitude;
	private ManagerHub manager;
	private Selector selector;
	private Board board;

//	public MoveOrder move;//make array in the future
	
	// Use this for initialization
	void Start () {
		manager = GameObject.Find("manager").GetComponent<ManagerHub>();
		selector = manager.selector;//both of these should be unnecessary
		board = manager.scratchBoard;//
//		resetMoves();
	}
	
	// Update is called once per frame
	void Update () {
		
//		if(manager.state == "resolving"){
//			//Do nothing
//		}else{
			if(Input.GetKeyDown(KeyCode.LeftBracket)){
				board.reset();//Shouldn't need a board reference.
			}

			else if(Input.GetKeyDown(KeyCode.RightBracket)){
				manager.resolve();
			}

			else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
				Debug.Log ("EAST");
				assignMoveOrder(Direction.EAST);
				
			}
			
			// For left, we have to subtract the direction
			else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
				Debug.Log ("WEST");
				assignMoveOrder(Direction.WEST);
			}
			
			else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
				Debug.Log ("NORTH");
				assignMoveOrder(Direction.NORTH);
			}
			
			// Same as for the left, subtraction for down
			else if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.DownArrow)) {
				Debug.Log ("SOUTH");
				assignMoveOrder( Direction.SOUTH );
			}
			
			//Diagonals 
			else if (Input.GetKeyDown(KeyCode.E)) {
				Debug.Log ("NORTHEAST");
				assignMoveOrder( Direction.NORTHEAST );
				
			}
			
			// For left, we have to subtract the direction
			else if (Input.GetKeyDown(KeyCode.Q)) {
				Debug.Log ("NORTHWEST");
				assignMoveOrder( Direction.NORTHWEST );
			}
			
			else if (Input.GetKeyDown(KeyCode.C)) {
				Debug.Log ("SOUTHEAST");
				assignMoveOrder( Direction.SOUTHEAST );
			}
			
			// Same as for the left, subtraction for down
			else if (Input.GetKeyDown(KeyCode.Z)) {
				Debug.Log ("SOUTHWEST");
				assignMoveOrder( Direction.SOUTHWEST );
			}
			
			// No direction
			else if (Input.GetKeyDown(KeyCode.S)) {
				Debug.Log ("NONE");
				assignMoveOrder( Direction.NONE );
			}

			if(Input.GetKeyDown(KeyCode.Return)) {
				manager.resolve();
			}
		//}
	}

//	private void changeTurn(int t){
//		
//		//Debug.Log (t);
//		if(t < 0 || t > moves.Length){
//			//Debug.Log("rejected");
//			return;
//		}
//		
//		turn = t;
//	}

//	public void resetMoves(){
//		move = new MoveOrder(Direction.NONE);
//	}

	void assignMoveOrder(int d){


		manager.order.setOrderKey("move");
		manager.order.setDirection(d);
		printMoves();
			
	}

	void printMoves(){

//		string result = "moves = [_";
//		for(int i = 0; i<move.Length; i++){
//			result+=move[i].toString()+"_";
//		}
//		result+="]";
//		Debug.Log ("move = "+move.toString());
	}
	
}
