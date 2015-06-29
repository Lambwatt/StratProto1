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

		field = GameObject.FindWithTag("ShowMagnitude").GetComponent<Text>();

		up = GameObject.FindWithTag("RaiseMagnitude").GetComponent<Button>();
		up.onClick.AddListener(()=>{field.text = manager.conductor.raiseMagnitude().ToString(); Debug.Log ("raised magnitude = "+manager.order.getMagnitude().ToString());});

		down = GameObject.FindWithTag("LowerMagnitude").GetComponent<Button>();
		down.onClick.AddListener(()=>{field.text = manager.conductor.lowerMagnitude().ToString(); Debug.Log ("lowered magnitude = "+manager.order.getMagnitude().ToString());});
		
	}

	private void resetButtons(){
		field.text = "1";
	}

	void Destroy(){
		ManagerHub.onAnimationPlay-=resetButtons;
	}
}
