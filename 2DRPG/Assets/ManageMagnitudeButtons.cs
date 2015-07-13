using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManageMagnitudeButtons : MonoBehaviour {

	Button up;
	Button down;
	Text field;
	ManagerHub manager;

	//int selected;

	void Start(){
		
		manager = GameObject.Find("manager").GetComponent<ManagerHub>();
		ManagerHub.onAnimationPlay+=resetButtons;
		ManagerHub.onPlayerChange+=updateDisplay;

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

	void Destroy(){
		ManagerHub.onAnimationPlay-=resetButtons;
		ManagerHub.onPlayerChange-=updateDisplay;
	}
}
