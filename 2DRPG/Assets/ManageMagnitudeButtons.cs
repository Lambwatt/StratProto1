using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManageMagnitudeButtons : MonoBehaviour {

	Button up;
	Button down;
	Text field;
	ManagerHub manager;
	CanvasGroup group;
	//int selected;

	void Start(){
		
		manager = GameObject.Find("manager").GetComponent<ManagerHub>();
		ManagerHub.onAnimationPlay+=resetButtons;
		ManagerHub.onPlayerChange+=updateDisplay;
		ManagerHub.onPlayerChange+=hideUI;
		ManagerHub.onMoveOrderSelect+=showUI;
		ManagerHub.onNonMoveOrderSelect+=hideUI;
		group = GetComponent<CanvasGroup>();

		field = GameObject.FindWithTag("ShowMagnitude").GetComponent<Text>();

		up = GameObject.FindWithTag("RaiseMagnitude").GetComponent<Button>();
		up.onClick.AddListener(()=>{field.text = manager.conductor.raiseMagnitude().ToString(); });

		down = GameObject.FindWithTag("LowerMagnitude").GetComponent<Button>();
		down.onClick.AddListener(()=>{field.text = manager.conductor.lowerMagnitude().ToString(); });
		
	}

	private void resetButtons(){
		field.text = "1";
	}

	private void updateDisplay(){
		field.text = ""+manager.order.getMagnitude();
	}

	private void showUI(){
		group.alpha = 1;
		group.interactable = true;
		group.blocksRaycasts = true;
	}
	
	private void hideUI(){
		group.alpha = 0;
		group.interactable = false;
		group.blocksRaycasts = false;
	}

	void OnDestroy(){
		ManagerHub.onAnimationPlay-=resetButtons;
		ManagerHub.onPlayerChange-=updateDisplay;
		ManagerHub.onPlayerChange-=hideUI;
		ManagerHub.onMoveOrderSelect-=showUI;
		ManagerHub.onNonMoveOrderSelect-=hideUI;
	}
}
