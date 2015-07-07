using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ManageOrderButton : MonoBehaviour {

	Dictionary <string, Toggle> buttons;
	ManagerHub manager;
	string buttonSelected;
	//int selected;
	
	void Start(){
		
		manager = GameObject.Find("manager").GetComponent<ManagerHub>();
		ManagerHub.onAnimationPlay+=resetButtons;
		ManagerHub.onPlayerChange+=updateDisplay;
		buttonSelected = "none";

		buttons = new Dictionary<string, Toggle>();

		//Order needs to be the same as direction hash
		buttons.Add ("none",GameObject.FindWithTag("NoneButton").GetComponent<Toggle>());
		buttons.Add ("attack",GameObject.FindWithTag("AttackButton").GetComponent<Toggle>());
		buttons.Add ("move",GameObject.FindWithTag("MoveButton").GetComponent<Toggle>());
		buttons.Add ("ready",GameObject.FindWithTag("ReadyButton").GetComponent<Toggle>());
		
		buttons["none"].onValueChanged.AddListener((value)=>{processButtonClick(value, "none");});
		buttons["attack"].onValueChanged.AddListener((value)=>{processButtonClick(value, "attack");});
		buttons["move"].onValueChanged.AddListener((value)=>{processButtonClick(value, "move");});
		buttons["ready"].onValueChanged.AddListener((value)=>{processButtonClick(value, "ready");});
		
	}
	
	private void processButtonClick(bool val, string key){
		if(val){
			if(key==buttonSelected)//re-selected active button
				;//do nothing
			else{//selected new direction
				setOrderType(key);
				clearOtherButtons(key);
			}
		}else{
			if(key==buttonSelected){//re-clicked active button
				buttons[key].isOn = true;//set to true and re-select
			}else//button deactivated
				;//do nothing
		}
	}
	
	private void setOrderType(string key){
		buttonSelected = key;
		manager.conductor.setOrderKey(key);
	}
	
	private void clearOtherButtons(string key){
		foreach(KeyValuePair<string, Toggle> button in buttons){
			if(button.Key!=key)
				button.Value.isOn = false;
		}
	}
	
	private void resetButtons(){
		buttons["none"].isOn = true;
	}

	private void updateDisplay(){
		buttons[manager.order.getKey()].isOn = true;
	}
	
	void Destroy(){
		ManagerHub.onPlayerChange-=updateDisplay;
		ManagerHub.onAnimationPlay-=resetButtons;
	}
}
