using UnityEngine;
using System.Collections;

//Also manages orders. Combine with movement and resolver in replacement structure. Also holds a lot of information that belongs in a seperate table.
//This class will become the UIDriver

public class Conductor : MonoBehaviour {

	private ManagerHub manager;
	//private Selector selector;
	private Board board;

//	public MoveOrder move;//make array in the future
	
	// Use this for initialization
	void Start () {
		manager = GameObject.Find("manager").GetComponent<ManagerHub>();
		//selector = manager.selector;//both of these should be unnecessary
		board = manager.scratchBoard;//
//		resetMoves();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(manager.state == "animating"){
			;//Do nothing
		}else{
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
		}
	}

	public int raiseMagnitude(){
		if(manager.order.getMagnitude()<manager.maxMagnitude)
			manager.order.setMagnitude(manager.order.getMagnitude()+1);
		return manager.order.getMagnitude();
	}

	public int lowerMagnitude(){
		if(manager.order.getMagnitude()>manager.minMagnitude)
			manager.order.setMagnitude(manager.order.getMagnitude()-1);
		return manager.order.getMagnitude();
	}

	public void setDirection(int d){
		manager.order.setDirection(d);
	}

	public void setOrderKey(string key){
		manager.order.setOrderKey(key);
	}

	void assignMoveOrder(int d){


		manager.order.setOrderKey("move");
		manager.order.setDirection(d);
		//printMoves();
			
	}

//	void printMoves(){

//		string result = "moves = [_";
//		for(int i = 0; i<move.Length; i++){
//			result+=move[i].toString()+"_";
//		}
//		result+="]";
//		Debug.Log ("move = "+move.toString());
//	}
	
}
