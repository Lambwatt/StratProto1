using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManageUIVisisbility : MonoBehaviour {

	ManagerHub manager;
	CanvasGroup group;
	
	void Start(){
		
		manager = GameObject.Find("manager").GetComponent<ManagerHub>();
		ManagerHub.onGoToGame+=showUI;
		ManagerHub.onGoToTransition+=hideUI;
		ManagerHub.onGoToEnd+=hideUI;

		group = GetComponent<CanvasGroup>();

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
	
	void Destroy(){
		ManagerHub.onGoToGame-=showUI;
		ManagerHub.onGoToTransition-=hideUI;
		ManagerHub.onGoToEnd-=hideUI;
	}
}
