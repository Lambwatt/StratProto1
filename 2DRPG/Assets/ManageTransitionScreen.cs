using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManageTransitionScreen : MonoBehaviour {

	ManagerHub manager;
	Button playerReadyButton;
	CanvasGroup group;
	
	void Start(){
		
		manager = GameObject.Find("manager").GetComponent<ManagerHub>();
		ManagerHub.onGoToGame+=hideScreen;
		ManagerHub.onGoToTransition+=showScreen;
//		ManagerHub.onGoToEnd+=hideScreen;

		playerReadyButton = GameObject.FindWithTag("PlayerReadyButton").GetComponent<Button>();
		playerReadyButton.onClick.AddListener(()=>{manager.finishTransition();});

		group = GetComponent<CanvasGroup>();
		hideScreen();
	}
	
	private void showScreen(){
		group.alpha = 1;
		group.interactable = true;
		group.blocksRaycasts = true;
	}
	
	private void hideScreen(){
		group.alpha = 0;
		group.interactable = false;
		group.blocksRaycasts = false;
	}
	
	void Destroy(){
		ManagerHub.onGoToGame-=hideScreen;
		ManagerHub.onGoToTransition-=showScreen;
//		ManagerHub.onGoToEnd-=hideScreen;
	}
}
