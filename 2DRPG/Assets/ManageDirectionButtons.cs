using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManageDirectionButtons : MonoBehaviour {

	Toggle[] buttons;
	ManagerHub manager;
	int buttonSelected;
	CanvasGroup group;
	//int selected;

	void Start(){

		manager = GameObject.Find("manager").GetComponent<ManagerHub>();
		ManagerHub.onAnimationPlay+=resetButtons;
		ManagerHub.onAnimationPlay+=hideUI;
		ManagerHub.onPlayerChange+=updateDisplay;
		ManagerHub.onPlayerChange+=hideUI;
		ManagerHub.onOrderSelect+=showUI;
		buttonSelected = 0;
		group = GetComponent<CanvasGroup>();

		//Order needs to be the same as direction hash
		buttons = new Toggle[]{
			GameObject.FindWithTag("CentreButton").GetComponent<Toggle>(),
			GameObject.FindWithTag("NorthEastButton").GetComponent<Toggle>(),
			GameObject.FindWithTag("EastButton").GetComponent<Toggle>(),
			GameObject.FindWithTag("SouthEastButton").GetComponent<Toggle>(),
			GameObject.FindWithTag("SouthButton").GetComponent<Toggle>(),
			GameObject.FindWithTag("SouthWestButton").GetComponent<Toggle>(),
			GameObject.FindWithTag("WestButton").GetComponent<Toggle>(),
			GameObject.FindWithTag("NorthWestButton").GetComponent<Toggle>(),
			GameObject.FindWithTag("NorthButton").GetComponent<Toggle>()

		};

		for(int i = 0; i<9; i++)
			setupListener(i);

	}

	private void setupListener(int i){
		buttons[i].onValueChanged.AddListener((value)=>{
				processButtonClick(value, i);
			//Debug.Log ("called for "+Direction.getDirectionString(i));
		});
	}

	private void processButtonClick(bool val, int d){
		if(val){
			if(d==buttonSelected)//re-selected active button
				;
			else{//selected new direction
				setDirection(d);
				clearOtherButtons(d);
			}
		}else{
			if(d==buttonSelected){//re-clicked active button
				buttons[d].isOn = true;//set to true and re-select
			}else//button deactivated
				;//do nothing
		}
	}

	private void setDirection(int d){
		buttonSelected = d;
		manager.conductor.setDirection(d);
	}

	private void clearOtherButtons(int d){
		for(int i = 0; i<9; i++){
			if(i!=d)
				buttons[i].isOn = false;
		}
	}

	private void resetButtons(){
		buttons[0].isOn = true;
	}

	private void updateDisplay(){
		buttons[manager.order.getDirection()].isOn = true;
	}

	private void showUI(){
		group.alpha = 1;
		group.interactable = true;
		group.blocksRaycasts = true;
	}
	
	private void hideUI(){
		Debug.Log("hid UI?");
		group.alpha = 0;
		group.interactable = false;
		group.blocksRaycasts = false;
	}

	void OnDestroy(){
		ManagerHub.onPlayerChange-=updateDisplay;
		ManagerHub.onAnimationPlay-=resetButtons;
		ManagerHub.onAnimationPlay+=hideUI;
		ManagerHub.onPlayerChange-=hideUI;
		ManagerHub.onOrderSelect-=showUI;
	}
}
