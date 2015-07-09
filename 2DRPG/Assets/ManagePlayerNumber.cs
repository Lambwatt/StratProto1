using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManagePlayerNumber : MonoBehaviour {
	
	Text field;
	ManagerHub manager;
	
	void Start(){
		
		manager = GameObject.Find("manager").GetComponent<ManagerHub>();
		ManagerHub.onPlayerChange+=updateDisplay;
		ManagerHub.onNewTurn+=updateDisplay;
		
		field = GameObject.FindWithTag("ShowPlayerNumber").GetComponent<Text>();
		
	}
	
	private void updateDisplay(){
		Debug.Log ("Updated display to match player "+manager.activePlayer+".");
		field.text = ""+(manager.activePlayer+1);
	}
	
	void Destroy(){
		ManagerHub.onPlayerChange-=updateDisplay;
		ManagerHub.onNewTurn-=updateDisplay;
	}
}
