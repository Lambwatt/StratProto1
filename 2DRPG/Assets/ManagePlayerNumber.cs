using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManagePlayerNumber : MonoBehaviour {
	
	Text field;
	ManagerHub manager;
	
	void Start(){
		
		manager = GameObject.Find("manager").GetComponent<ManagerHub>();
		//Sureley one of these is at least partially obselete.
		ManagerHub.onPlayerChange+=updateDisplay;
		ManagerHub.onNewTurn+=updateDisplay;
		ManagerHub.onGoToGame+=updateDisplay;
		
		field = GameObject.FindWithTag("ShowPlayerNumber").GetComponent<Text>();
		
	}
	
	private void updateDisplay(){
		field.text = ""+(manager.activePlayer+1);
	}
	
	void Destroy(){
		ManagerHub.onPlayerChange-=updateDisplay;
		ManagerHub.onNewTurn-=updateDisplay;
		ManagerHub.onGoToGame-=updateDisplay;
	}
}
