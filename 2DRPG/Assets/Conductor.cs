using UnityEngine;
using System.Collections;

//Also manages orders. Combine with movement and resolver in replacement structure. Also holds a lot of information that belongs in a seperate table.
//This class will become the UIDriver

public class Conductor : MonoBehaviour {

	private ManagerHub manager;
	
	// Use this for initialization
	void Start () {
		manager = GameObject.Find("manager").GetComponent<ManagerHub>();
	}
	
	// Update is called once per frame
	void Update () {
//		
//		if(manager.state == "animating"){
//			;//Do nothing
//		}else{
//
////			if(Input.GetKeyDown(KeyCode.G)) {
////				//Soon to be in a button response 
////				manager.resolve();
////
////			}
////
////			else if(Input.GetKeyDown(KeyCode.Tab)) {
////				manager.changePlayer();
////			}
//		}
	}

	public void done(){
		if(manager.state == "animating"){
			;//Do nothing
		}else if(manager.state == "planning"){
			
			if(manager.allPlayersVisited()){
				manager.resolve();
			}else{
				manager.changePlayer();
			}
		}
	}

	public int raiseMagnitude(){
		if(manager.state=="planning" && manager.order.getMagnitude()<manager.maxMagnitude){
			//Debug.Log("before: "+manager.order.getMagnitude());
			manager.order.setMagnitude(manager.order.getMagnitude()+1);
			//Debug.Log("now: "+manager.order.getMagnitude());
		}
		return manager.order.getMagnitude();
	}

	public int lowerMagnitude(){
		if(manager.state=="planning" && manager.order.getMagnitude()>manager.minMagnitude)
			manager.order.setMagnitude(manager.order.getMagnitude()-1);
		return manager.order.getMagnitude();
	}

	public void setDirection(int d){
		if(manager.state=="planning")
			manager.order.setDirection(d);
	}

	public void setOrderKey(string key){
		if(manager.state=="planning"){
			manager.order.setOrderKey(key);
		}
	}

	void assignMoveOrder(int d){
		manager.order.setOrderKey("move");
		manager.order.setDirection(d);
	}

}
