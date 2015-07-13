using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManageDoneButton : MonoBehaviour {

	ManagerHub manager;
	Button doneButton;
	
	void Start(){
		
		manager = GameObject.Find("manager").GetComponent<ManagerHub>();
				
		doneButton = GameObject.FindWithTag("DoneButton").GetComponent<Button>();
		doneButton.onClick.AddListener(()=>{manager.conductor.done();});
	}
	
	void Destroy(){

	}
}
