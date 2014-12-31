using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Selector : MonoBehaviour {

	private ManagerHub manager;
	public List <GameObject>[] selectedUnits = new List<GameObject>[3];//Important for server. probably not really now.

	void Destroy(){
		ManagerHub.onAnimationPlay-=resetSelection;
	}

	// Use this for initialization
	void Start () {

		manager = gameObject.GetComponent<ManagerHub>();

		for(int i = 0; i<selectedUnits.Length; i++){
			selectedUnits[i] = new List<GameObject>();
		}

		ManagerHub.onAnimationPlay+=resetSelection;
	}

	private void selectOrDeselect(GameObject subject){

		Debug.Log(selectedUnits.ToString());
		Debug.Log("turn is "+manager.turn);
		Debug.Log(selectedUnits[manager.turn]);

		if(selectedUnits[manager.turn].Contains(subject)){

			//Debug.Log("removed");
			selectedUnits[manager.turn].Remove(subject);
			subject.GetComponent<Movement>().deselect();

		}else{

			//Debug.Log("added");
			selectedUnits[manager.turn].Add(subject);
			subject.GetComponent<Movement>().select();

		}

		Debug.Log(selectedUnits[manager.turn].ToString()+", "+selectedUnits[manager.turn].ToArray().Length);

	}

	void resetSelection(int oldTurn){
		//oldTurn is not used
		for(int i = 0; i<selectedUnits.Length; i++){
			selectedUnits[i].Clear();
		}
	}

	// Update is called once per frame
	void Update () {
		//Selection must account for subsequent positions
		if(Input.GetMouseButtonDown(0)/* && ( Input.mousePosition())*/){
			
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			
			Square boardPos = manager.board.convertMouseClickToBoardCoords(mousePos);
			
			Debug.Log (boardPos.x+" "+ boardPos.y);

			GameObject contents = manager.board.grid[boardPos.x, boardPos.y];
			Debug.Log(contents);
			if(contents){
				selectOrDeselect(contents);
				//Debug.Log ("occupied. select contents");
			}
			else
				Debug.Log ("empty. select nothing.");
			//			mousePos.x = Mathf.Floor( mousePos.x ) + (Mathf.Abs(mousePos.x)%1.0f>0.5?1:0);
			//			mousePos.y = Mathf.Floor( mousePos.y ) + (Mathf.Abs(mousePos.y)%1.0f>0.5?1:0);
			//			
			//			//Only seems to connect with one. find out why.
			//			
			//			if((Mathf.Floor( transform.position.x ) + (transform.position.x%1.0f>0.5?1:0)) == mousePos.x  && (Mathf.Floor( transform.position.y ) + (transform.position.y%1.0f>0.5?1:0)) == mousePos.y  ){
			//				Debug.Log("found "+idNo );
			//			}else{
			//				Debug.Log("missed "+idNo+":"+(Mathf.Floor( transform.position.x ) + (transform.position.x%1.0f>0.5?1:0)) +":"+ mousePos.x +","+ (Mathf.Floor( transform.position.y ) + (transform.position.y%1.0f>0.5?1:0)) +":"+ mousePos.y);
			//			}
		}
	}
}
