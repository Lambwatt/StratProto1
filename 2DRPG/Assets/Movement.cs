using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public int numFrames;
	public int idNo;
	public ManagerHub manager;
	public GameObject selectionBox;
	//private int turn = 0;
//	private Vector2 pos;
	private bool moving = false;
	private SpriteMovement currentAnimation;
	
	//FIXME Fix everything here to be designed around a single round. and clarify moves vs rounds vs turns teminology throughout

	//Moves turn forward or back

	public void printMovement(){
		Debug.Log ("Movement:["+currentAnimation.printMoves()+"]");
	}

	public void setNextAnimation(SpriteMovement n){
		currentAnimation.setNext(n);
	}

	public void setPosition(Vector3 dest){
		transform.position = dest;
	}
	
	public void select(){
		//selection[manager.turn] = true;
		showSelection();
	}

	public void deselect(){
		//selection[manager.turn] = false;
		hideSelection();
	}

	private void clearSelection(){
//		for(int i = 0; i<3; i++){
//			selection[i] = false;
//		}
	}

	void Destroy(){
		//ManagerHub.onTurnChange-=changeTurn;
		ManagerHub.onAnimationPlay-=playNextAnimation;
	}

	void Start () {

		manager = GameObject.Find("manager").GetComponent<ManagerHub>();
		selectionBox = transform.FindChild("selection").gameObject;
		selectionBox.GetComponent<Renderer>().enabled = false;
		currentAnimation = new SpriteMovement("original idle", new LinearMoveCurve(null), transform.position, transform.position, 0);
		manager.board.register(this.gameObject, transform.position); //This should be unnecessary in future versions

		ManagerHub.onAnimationPlay+=playNextAnimation;
	}
	
	void Update () {//This is long. should shorten it or move it.

		if(manager.state=="animating"){
			//Debug.Log("animation is "+currentAnimation.complete());
			if(currentAnimation.complete()){
				if(currentAnimation.hasNext()){
					currentAnimation = currentAnimation.getNext();
					transform.position = currentAnimation.getStep();
					Debug.Log ("Moved to next animation with tag: "+currentAnimation.getSpriteName());
				}
			}else{
				transform.position = currentAnimation.getStep();
			}
		}

	}

	private void playNextAnimation(){
		printMovement();
		if(currentAnimation.hasNext()){
			currentAnimation = currentAnimation.getNext();
		}
		hideSelection();
	}
	
	private void showSelection(){
		selectionBox.GetComponent<Renderer>().enabled = true;
	}

	private void hideSelection(){
		selectionBox.GetComponent<Renderer>().enabled = false;
	}
	

}