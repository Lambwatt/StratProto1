using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public int numFrames;
	public int idNo;
	public ManagerHub manager;
	public GameObject selectionBox;
	public GameObject highlight;
	public int player;
	public int totalHealth;
	public int aimedAttackDamage;
	public int readyAttackDamage;
	public int range;
	private SpriteMovement currentAnimation;
	private Transform healthbar;
	private int health;
	private Sprite[] sprites;
	
	//FIXME Fix everything here to be designed around a single round. and clarify moves vs rounds vs turns teminology throughout

	public void setSpriteList(Sprite[] s){
		sprites = s;
	}

	public void printMovement(){
		Debug.Log ("Movement:["+currentAnimation.printMoves()+"]");
	}

	public void setNextAnimation(SpriteMovement n){
		currentAnimation.setNext(n);
	}

	public void replaceAnimation(SpriteMovement n){
		currentAnimation = n;
		setSprite(currentAnimation.getSpriteName());
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

	public void setPlayerNumber(int i){
		player = i;
		//hightlightOn();
	}

	public int getPlayerNumber(){
		return player;
	}

	void OnDestroy(){
		ManagerHub.onAnimationPlay-=playNextAnimation;
		ManagerHub.onAnimationPlay-=hightlightOn;
		ManagerHub.onPlayerChange-=hightlightOn;
		ManagerHub.onGoToGame-=hightlightOn;
	}

	void hightlightOn(){
		//Debug.Log("Active player = "+manager.activePlayer+" vs "+player);
		if(manager.activePlayer==player)
			highlight.GetComponent<Renderer>().enabled = true;
		else
			highlight.GetComponent<Renderer>().enabled = false;
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
		highlight = transform.FindChild("Highlight").gameObject;
		highlight.GetComponent<Renderer>().enabled = false;
		currentAnimation = new SpriteMovement("original idle", new LinearMoveCurve(null), transform.position, transform.position, 0);
		manager.board.register(this.gameObject, transform.position); //This should be unnecessary in future versions
		hightlightOn();

		ManagerHub.onAnimationPlay+=playNextAnimation;
		ManagerHub.onAnimationPlay+=hightlightOn;
		ManagerHub.onPlayerChange+=hightlightOn;
		ManagerHub.onGoToGame+=hightlightOn;


	}
	
	void Update () {

		if(manager.state=="animating"){
			if(currentAnimation.complete()){
				if(currentAnimation.hasNext()){
					//Debug.Log ("switching animations");
					currentAnimation = currentAnimation.getNext();
					setSprite(currentAnimation.getSpriteName());
					transform.position = currentAnimation.getStep();
				}
			}else{
				transform.position = currentAnimation.getStep();
			}
		}

	}

	public void setSprite(string key){
		switch(key){
		case "idle":
			//Debug.Log ("switched to idle");
			GetComponent<SpriteRenderer>().sprite=sprites[0];
			break;
		case "shootAimed":
			//Debug.Log ("switched to aimed shot");
			GetComponent<SpriteRenderer>().sprite=sprites[1];
			break;
		case "shootReadied":
			//Debug.Log ("switched to ready shot");
			GetComponent<SpriteRenderer>().sprite=sprites[2];
			break;
		case "ready":
			//Debug.Log ("switched to ready");
			GetComponent<SpriteRenderer>().sprite=sprites[3];
			break;
		case "hit":
			//Debug.Log ("switched to hit");
			GetComponent<SpriteRenderer>().sprite=sprites[4];
			break;
		case "die":
			//Debug.Log ("switched to dead");
			GetComponent<SpriteRenderer>().sprite=sprites[5];
			break;
		default:
			break;
		}
	}

	private void playNextAnimation(){
		if(currentAnimation.hasNext()){
			currentAnimation = currentAnimation.getNext();
			setSprite(currentAnimation.getSpriteName());
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
		highlight.GetComponent<Renderer>().enabled = false;
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
		ManagerHub.onAnimationPlay-=playNextAnimation;
		manager.registerDeath(player);
	}
}