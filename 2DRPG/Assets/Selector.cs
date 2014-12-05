using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Selector : MonoBehaviour {

	private ManagerHub manager;
	public List <GameObject> selectedUnits = new List<GameObject>();

	// Use this for initialization
	void Start () {
		manager = gameObject.GetComponent<ManagerHub>();
	}

	private void selectOrDeselect(GameObject subject){

		Debug.Log(selectedUnits.ToString());
		if(selectedUnits.Contains(subject)){
			Debug.Log("removed");
			Debug.Log(selectedUnits.Remove(subject));
			subject.GetComponent<Movement>().hideSelection();
		}else{
			Debug.Log("added");
			selectedUnits.Add(subject);
			subject.GetComponent<Movement>().showSelection();
		}

		Debug.Log(selectedUnits.ToString()+", "+selectedUnits.ToArray().Length);

	}

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)/* && ( Input.mousePosition())*/){
			
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			
			Square boardPos = manager.board.convertMouseClickToBoardCoords(mousePos);
			
			Debug.Log (boardPos.x+" "+ boardPos.y);

			GameObject contents = manager.board.grid[boardPos.x, boardPos.y];
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
