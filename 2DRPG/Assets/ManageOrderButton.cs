using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ManageOrderButton : MonoBehaviour {

	Dictionary <string, Toggle> buttons;
	ManagerHub manager;
	string buttonSelected;
	CanvasGroup group;
	//int selected;
	
	void Start(){
		
		manager = GameObject.Find("manager").GetComponent<ManagerHub>();
		ManagerHub.onAnimationPlay+=resetButtons;
		ManagerHub.onAnimationPlay+=hideUI;
		ManagerHub.onPlayerChange+=updateDisplay;
		ManagerHub.onPlayerChange+=hideUI;
		ManagerHub.onSelect+=showUI;
		buttonSelected = "none";
		group = GetComponent<CanvasGroup>();

		buttons = new Dictionary<string, Toggle>();

		//Order needs to be the same as direction hash
		buttons.Add ("none",GameObject.FindWithTag("NoneButton").GetComponent<Toggle>());
		buttons.Add ("attack",GameObject.FindWithTag("AttackButton").GetComponent<Toggle>());
		buttons.Add ("move",GameObject.FindWithTag("MoveButton").GetComponent<Toggle>());
		buttons.Add ("ready",GameObject.FindWithTag("ReadyButton").GetComponent<Toggle>());
		
		buttons["none"].onValueChanged.AddListener((value)=>{processButtonClick(value, "none"); });
		buttons["attack"].onValueChanged.AddListener((value)=>{processButtonClick(value, "attack"); });
		buttons["move"].onValueChanged.AddListener((value)=>{processButtonClick(value, "move"); });
		buttons["ready"].onValueChanged.AddListener((value)=>{processButtonClick(value, "ready"); });


		
	}
	
	private void processButtonClick(bool val, string key){
		Debug.Log ("Proccessing click: val = "+val+", key = "+key);
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
		if(key!="none")
			manager.selectOrder(key);
	}
	
	private void clearOtherButtons(string key){
		foreach(KeyValuePair<string, Toggle> button in buttons){
			if(button.Key!=key)
				button.Value.isOn = false;
		}
	}
	
	private void resetButtons(){
		buttons["none"].isOn = true;
		foreach(KeyValuePair<string, Toggle> button in buttons){
			//button.Value.enabled = false;
		}
	}

	private void updateDisplay(){
		buttons[manager.order.getKey()].isOn = true;
	}

	private void showUI(){
		group.alpha = 1;
		group.interactable = true;
		group.blocksRaycasts = true;
	}
	
	private void hideUI(int holder = 0){
		group.alpha = 0;
		group.interactable = false;
		group.blocksRaycasts = false;
	}
	
	void Destroy(){
		ManagerHub.onPlayerChange-=updateDisplay;
		ManagerHub.onAnimationPlay-=hideUI;
		ManagerHub.onAnimationPlay-=resetButtons;
		ManagerHub.onSelect-=showUI;
		ManagerHub.onPlayerChange-=hideUI;
	}
}
