using UnityEngine;
using System.Collections;

public enum Direction {
	NONE=0,
	NORTHEAST=1,
	EAST=2,
	SOUTHEAST=3,
	SOUTH=4,
	SOUTHWEST=5,
	WEST=6,
	NORTHWEST=7,
	NORTH=8
}

public class MoveOrder{

	public Vector2 direction;
	public string label;
	
	public MoveOrder(Direction direction){

		switch(direction){
		case Direction.NONE:
			label = "NONE";
			this.direction = new Vector2(0,0);
			break;
		case Direction.NORTHEAST:
			label = "NORTHEAST";
			this.direction = -1*Vector2.right + Vector2.up; 
			break;
		case Direction.EAST:
			label = "EAST";
			this.direction = Vector2.right;
			break;
		case Direction.SOUTHEAST:
			label = "SOUTHEAST";
			this.direction = Vector2.right + (-1*Vector2.up);
			break;
		case Direction.SOUTH:
			label = "SOUTH";
			this.direction = -1*Vector2.up;
			break;
		case Direction.SOUTHWEST:
			label = "SOUTHWEST";
			this.direction = -1*Vector2.right + (-1*Vector2.up);
			break;
		case Direction.WEST:
			label = "WEST";
			this.direction = -1*Vector2.right;
			break;
		case Direction.NORTHWEST:
			label = "NORTHWEST";
			this.direction = Vector2.right + Vector2.up;
			break;
		case Direction.NORTH:
			label = "NORTH";
			this.direction = Vector2.up;
			break;
		default:
			break;
		}

		//this.direction = direction;
	}
	
	public void apply(Transform subject) {

		subject.position += (Vector3) direction;

	}
	
	public void undo(Transform subject) {

		subject.position -= (Vector3) direction;

	}

	public string toString(){

		return "Move "+label;
	}
}

public class Conductor : MonoBehaviour {

	//These will be needed later
	//private Direction direction;
	private int magnitude;
	private ManagerHub manager;
	private Selector selector;
	private Board board;

	public MoveOrder[] moves = new MoveOrder[3];


	// Use this for initialization
	void Start () {
		manager = GameObject.Find("manager").GetComponent<ManagerHub>();
		selector = manager.selector;
		board = manager.board;
		resetMoves();
	}
	
	// Update is called once per frame
	void Update () {
//		
		
		/*else*/ if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
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

	public void resetMoves(){
		for(int i = 0; i<3; i++){
			moves[i] = new MoveOrder(Direction.NONE);
		}
	}

	void assignMoveOrder(Direction direction){

		//Pretty sure only the last line of this does anything
//		if(index < 0 || index > moves.Length)
//			return;
//		else
			moves[manager.turn] = new MoveOrder(direction);
		printMoves();
			
	}

	void printMoves(){

		string result = "moves = [_";
		for(int i = 0; i<moves.Length; i++){
			result+=moves[i].toString()+"_";
		}
		result+="]";
		Debug.Log (result);
	}
	
}
