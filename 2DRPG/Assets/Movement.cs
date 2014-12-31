using UnityEngine;
using System.Collections;




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
	public GameObject selectionBox;


	 

	//private int turn = 0;
//	private Vector2 pos;
	private bool moving = false;

	//private MoveOrder[] moves;
	//private Vector2 change;
	//private bool selected = true;

	private moveAnimation anim;
	private int frames = 0;
	private int moveNumber = 0;
	private bool moveComplete = false;
	private Vector3 oldPosition;

	private bool[] selection = new bool[] {false, false, false};


	//Moves turn forward or back

	private void resetPosition(int oldTurn){
		for(int i = oldTurn; i > 0; i--){

			if(selection[i-1]){
				
				Vector3 oldPos = transform.position;
				manager.conductor.moves[i-1].undo(transform);
				manager.board.move(oldPos, transform.position);
				
			}
		}
	}

	private void changeTurn(int t){

		//Debug.Log (t);
		if(t < 0 || t > manager.conductor.moves.Length){
			Debug.Log("rejected "+t);
			return;
		}

		//FIX THINGS HERE!!!!!
		//Debug.Log ("t is " + t+" while turn is "+turn);
		if(manager.turn < t){//count down
			//Debug.Log("took lesser");
			for(int i = t; i > manager.turn ; i--){
				if(selection[i-1]){

					Vector3 oldPos = transform.position;
					manager.conductor.moves[i-1].undo(transform);
					manager.board.move(oldPos, transform.position);

				}
			}

		}else if(manager.turn > t){
			//Debug.Log("took greater");
			for(int i = t; i < manager.turn; i++){
				if(selection[i]){

					Vector3 oldPos = transform.position;

					manager.conductor.moves[i].apply(transform);
					//Debug.Log("going from "+oldPos+" to "+transform.position);
					manager.board.move(oldPos, transform.position);
					//Debug.Log("Moved?");
				}
			}

		}else{
			//Debug.Log("took equal");
			//do nothing
		}

		//All suspect
//		Debug.Log("should catch "+manager.conductor.moves.Length);
//		if(turn==manager.conductor.moves.Length){Debug.Log("quit turn number "+turn); return;}//Take this out when the prototype ends
//
//		Debug.Log("Testing turn number "+turn);

		if(selection[manager.turn])
			select();
		else
			deselect();
	}

	public void select(){
		selection[manager.turn] = true;
		showSelection();
	}

	public void deselect(){
		selection[manager.turn] = false;
		hideSelection();
	}

	private void clearSelection(){
		for(int i = 0; i<3; i++){
			selection[i] = false;
		}
	}

	void Destroy(){
		ManagerHub.onTurnChange-=changeTurn;
		ManagerHub.onAnimationPlay-=animate;
	}

	void Start () {

		manager = GameObject.Find("manager").GetComponent<ManagerHub>();
		selectionBox = transform.FindChild("selection").gameObject;
		selectionBox.renderer.enabled = false;
		Debug.Log("box was "+selectionBox);
//		Debug.Log ("onStart "+managerObject);
//			 = managerObject
//		Debug.Log("manager is "+manager);

		manager.test();
		manager.board.register(this.gameObject, transform.position);

		ManagerHub.onTurnChange+=changeTurn;
		ManagerHub.onAnimationPlay+=animate;

//		moves = manager.conductor.moves;
		// First store our current position when the
		// script is initialized.

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
					selectMoveAnimation();

				}else{
					moving = false;
//					resetMoves(); Where should resets happen?
					manager.board.move(oldPosition, transform.position);
					manager.conductor.resetMoves();
					resetSelection();
				}
			}
		}else{
			//CheckInput();
		}

	}

	private void resetSelection(){

		for(int i = 0; i<selection.Length; i++){
			selection[i] = false;
		}
		hideSelection();
	}

	private void selectMoveAnimation(){
		Debug.Log("\trunning for "+moveNumber);
		if(selection[moveNumber]){
			Debug.Log("\tDetected real movement");
			anim = new moveAnimation(this.transform, manager.conductor.moves[moveNumber], numFrames);
			moveComplete = false;
			frames = 0;
		}else{
			Debug.Log("\tNot selected");
			//It would be better if you could just not do anything, but for now a go nowhere will do
			anim = new moveAnimation(this.transform, new MoveOrder(Direction.NONE), numFrames);
			moveComplete = false;
			frames = 0;
		}
	}

	private void showSelection(){
		selectionBox.renderer.enabled = true;
	}

	private void hideSelection(){
		selectionBox.renderer.enabled = false;
	}
	
	private void animate(int oldTurn) {



		//Execute turn
//		if(Input.GetKeyDown(KeyCode.Return)) {
			resetPosition(oldTurn);
			moveNumber = 0;
			frames = 0;
			moveComplete = false;
			//anim = new moveAnimation(this.transform, manager.conductor.moves[moveNumber], numFrames);
			selectMoveAnimation();
			moving = true;
			oldPosition = transform.position;
//		}

		//}
	}
}