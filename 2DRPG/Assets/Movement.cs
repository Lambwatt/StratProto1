using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public int numFrames;
	public int idNo;
	public ManagerHub manager;
	public GameObject selectionBox;
	public int player;
	public int totalHealth;
	public int aimedAttackDamage;
	public int readyAttackDamage;
	public int range;
	private SpriteMovement currentAnimation;
	private Transform healthbar;
	private int health;
	
	//FIXME Fix everything here to be designed around a single round. and clarify moves vs rounds vs turns teminology throughout
	

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
		showSelection();
	}

	public void deselect(){
		hideSelection();
	}

//	private void clearSelection(){
//		for(int i = 0; i<3; i++){
//			selection[i] = false;
//		}
//	}

	public void setPlayerNumber(int i){
		player = i;
	}

	public int getPlayerNumber(){
		return player;
	}

	void OnDestroy(){
		Debug.Log ("pulling out");
		ManagerHub.onAnimationPlay-=playNextAnimation;
	}

	void Destroy(){

//		ManagerHub.onAnimationPlay-=playNextAnimation;
	}

	void Start () {

		health = totalHealth;
		healthbar = transform.FindChild("healthFG");
		manager = GameObject.Find("manager").GetComponent<ManagerHub>();
		selectionBox = transform.FindChild("selection").gameObject;
		selectionBox.GetComponent<Renderer>().enabled = false;
		currentAnimation = new SpriteMovement("original idle", new LinearMoveCurve(null), transform.position, transform.position, 0);
		manager.board.register(this.gameObject, transform.position); //This should be unnecessary in future versions

		ManagerHub.onAnimationPlay+=playNextAnimation;
	}
	
	void Update () {

		if(manager.state=="animating"){
			if(currentAnimation.complete()){
				if(currentAnimation.hasNext()){
					currentAnimation = currentAnimation.getNext();
					transform.position = currentAnimation.getStep();
				}
			}else{
				transform.position = currentAnimation.getStep();
			}
		}

	}

	private void playNextAnimation(){
		if(currentAnimation.hasNext()){
			currentAnimation = currentAnimation.getNext();
		}
		hideSelection();
	}

	public void updateHealthDisplay(){
		int healthLost = totalHealth - health;


		healthbar.localScale = new Vector3(health>0 ? (float)health/(float)totalHealth : 0, healthbar.localScale.y, healthbar.localScale.z);
		healthbar.position = new Vector3(transform.position.x-((GetComponent<Renderer>().bounds.size.x / (float)totalHealth / 2.0f) * (float)healthLost), healthbar.position.y, healthbar.position.z);//need to get the constant out of here.

	}
	
	private void showSelection(){
		selectionBox.GetComponent<Renderer>().enabled = true;
	}

	private void hideSelection(){
		selectionBox.GetComponent<Renderer>().enabled = false;
	}
	
	public bool deductDamageFromHealth(int damage){
		health = health - damage;
		health = health>0 ? health : 0;
		updateHealthDisplay();//Maybe put this call outside the object when it needs to be distinguished from death
		return health == 0;
	}

	public int getAttackDamage(){
		return aimedAttackDamage;
	}

	public int getReadyDamage(){
		return readyAttackDamage;
	}

	public int getRange(){
		return range;
	}

	public void registerDeath(){
		Debug.Log ("Dying");
		ManagerHub.onAnimationPlay-=playNextAnimation;
		manager.registerDeath(player);
	}
}