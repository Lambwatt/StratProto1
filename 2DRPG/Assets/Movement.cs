using UnityEngine;
using System.Collections;

public class MoveOrder{

	public Vector2 direction;

	public MoveOrder(Vector2 direction){
		this.direction = direction;
	}

	public void apply(Transform subject) {

		subject.position += (Vector3) direction;
	}

	public void undo(Transform subject) {
		subject.position -= (Vector3) direction;
	}
}


public class moveAnimation{

	private Transform subject;
	private Vector2 step;

	//private int numSteps;
	//private int currentStep;
	//private pointList; For later when objects need to turn in a movement path

	public moveAnimation(Transform subject, MoveOrder order, int numFrames){//Num frames should be moved so it can be a global constant.
		step = order.direction / numFrames;
		this.subject = subject;
	}

	public void nextStep(){
		subject.position += (Vector3) step;
	}

}

public class Movement : MonoBehaviour {

	public int numFrames;
	public int idNo;
	public ManagerHub manager;


	private MoveOrder[] moves = new MoveOrder[3]; 

	private int turn = 0;
//	private Vector2 pos;
	private bool moving = false;
	//private Vector2 change;
	private bool selected = true;

	private moveAnimation anim;
	private int frames = 0;
	private int moveNumber = 0;
	private bool moveComplete = false;
	private Vector3 oldPosition;


	void assignMoveOrder(int index, Vector2 direction){

		if(index < 0 || index > moves.Length)
			return;
		else
			moves[turn] = new MoveOrder(direction);
	}

	//Moves turn forward or back
	private void changeTurn(int t){

		//Debug.Log (t);
		if(t < 0 || t > moves.Length){
			//Debug.Log("rejected");
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
			//Debug.Log("took equal");
			//do nothing
		}
	}

	void resetMoves(){
		for(int i = 0; i<3; i++){
			moves[i] = new MoveOrder(new Vector2(0,0));
		}
	}

	void Start () {

		manager.test();
		manager.board.register(this.gameObject, transform.position);

		// First store our current position when the
		// script is initialized.
		resetMoves();
//		pos = transform.position;//Does this do anything?
	}
	
	void Update () {//This is long. should shorten it or move it.
		if(moving){
			anim.nextStep();
			frames++;
			moveComplete = frames>=numFrames;

			if(moveComplete){
				moveNumber++;
				if(moveNumber < 3){

					anim = new moveAnimation(this.transform, moves[moveNumber], numFrames);
					moveComplete = false;
					frames = 0;

				}else{
					moving = false;
					resetMoves();
					manager.board.move(oldPosition, transform.position);
				}
			}
		}else{
			CheckInput();
		}

	}
	
	private void CheckInput() {


		if(selected){

			if(Input.GetKeyDown(KeyCode.RightBracket)){
				//Debug.Log ("righted "+(turn+1));
				changeTurn(turn+1);

			}

			else if(Input.GetKeyDown(KeyCode.LeftBracket)){
				//Debug.Log ("left "+(turn-1));
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
			else if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.DownArrow)) {
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
			else if (Input.GetKeyDown(KeyCode.Z)) {
				assignMoveOrder(turn, -1*Vector2.right + (-1*Vector2.up));
			}

			// No direction
			else if (Input.GetKeyDown(KeyCode.S)) {
				assignMoveOrder(turn, new Vector2(0,0));
			}

			//Execute turn
			else if (Input.GetKeyDown(KeyCode.Return)) {
				changeTurn(0);
				moveNumber = 0;
				frames = 0;
				moveComplete = false;
				anim = new moveAnimation(this.transform, moves[moveNumber], numFrames);
				moving = true;
				oldPosition = transform.position;
			}

		}
	}
}