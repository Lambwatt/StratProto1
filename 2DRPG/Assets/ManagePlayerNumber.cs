using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManagePlayerNumber : MonoBehaviour {
	
	Text field;
	ManagerHub manager;
	
	void Start(){
		
		manager = GameObject.Find("manager").GetComponent<ManagerHub>();
		ManagerHub.onPlayerChange+=updateDisplay;
		
		field = GameObject.FindWithTag("ShowPlayerNumber").GetComponent<Text>();
		
	}
	
	private void updateDisplay(){
		field.text = ""+(manager.activePlayer+1);
	}
	
	void Destroy(){
		ManagerHub.onPlayerChange-=updateDisplay;
	}
}
