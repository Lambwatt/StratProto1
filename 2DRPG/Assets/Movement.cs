using UnityEngine;
using System.Collections;

public class MoveOrder{

	private Vector2 direction;

	public MoveOrder(Vector2 direction){
		this.direction = direction;
	}

	public void apply(Transform subject) {

		subject.position +=(Vector3) direction;
	}

	public void undo(Transform subject) {
		subject.position-= (Vector3) direction;
	}
}

public class Movement : MonoBehaviour {

	private MoveOrder[] moves = new MoveOrder[3]; 

	private int turn = 0;
	private Vector2 pos;
	private bool moving = false;
	private Vector2 change;


	void assignMoveOrder(int index, Vector2 direction){

		if(index < 0 || index > moves.Length)
			return;
		else
			moves[turn] = new MoveOrder(direction);
	}

	//Moves turn forward or back
	private void changeTurn(int t){

		Debug.Log (t);
		if(t < 0 || t > moves.Length){
			Debug.Log("rejected");
			return;
		}

		//Debug.Log ("proceding with " + t);
		if(t < turn){//count down
			//Debug.Log("took lesser");
			for(; turn > t ; turn--){
				moves[turn-1].undo(transform);
			}

		}else if(t > turn){
			//Debug.Log("took greater");
			for(; turn < t; turn++)
				moves[turn].apply(transform);

			
		}else{
			Debug.Log("took equal");
			//do nothing
		}
	}

	void Start () {
		// First store our current position when the
		// script is initialized.
		for(int i = 0; i<3; i++){
			moves[i] = new MoveOrder(new Vector2(0,0));
		}
		pos = transform.position;//Does this do anything?
	}
	
	void Update () {
		
		CheckInput();

	}
	
	private void CheckInput() {

		if(Input.GetKeyDown(KeyCode.RightBracket)){
			Debug.Log ("righted "+(turn+1));
			changeTurn(turn+1);

		}

		else if(Input.GetKeyDown(KeyCode.LeftBracket)){
			Debug.Log ("left "+(turn-1));
			changeTurn(turn-1);

		}
	
		else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
			assignMoveOrder(turn, Vector2.right);

		}
		
		// For left, we have to subtract the direction
		else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
			assignMoveOrder(turn, -1*Vector2.right);
		}

		else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
			assignMoveOrder(turn, Vector2.up);
		}
		
		// Same as for the left, subtraction for down
		else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
			assignMoveOrder(turn, -1*Vector2.up);
		}

		//Diagonals 
		else if (Input.GetKeyDown(KeyCode.E)) {
			assignMoveOrder(turn, Vector2.right + Vector2.up);
			
		}
		
		// For left, we have to subtract the direction
		else if (Input.GetKeyDown(KeyCode.Q)) {
			assignMoveOrder(turn, -1*Vector2.right + Vector2.up);
		}
		
		else if (Input.GetKeyDown(KeyCode.C)) {
			assignMoveOrder(turn, Vector2.right + (-1*Vector2.up));
		}
		
		// Same as for the left, subtraction for down
		else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.DownArrow)) {
			assignMoveOrder(turn, -1*Vector2.right + (-1*Vector2.up));
		}

	}
}