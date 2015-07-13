using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManageEndScreen : MonoBehaviour {

	ManagerHub manager;
	Button replayButton;
	Button returnButton;
	CanvasGroup group;
	
	void Start(){
		
		manager = GameObject.Find("manager").GetComponent<ManagerHub>();
		ManagerHub.onGoToEnd+=showScreen;
		ManagerHub.onGoToStart+=hideScreen;
		ManagerHub.onGoToGame+=hideScreen;

		replayButton = GameObject.FindWithTag("ReplayButton").GetComponent<Button>();
		replayButton.onClick.AddListener(()=>{manager.startGame();});

		returnButton = GameObject.FindWithTag("ReturnButton").GetComponent<Button>();
		returnButton.onClick.AddListener(()=>{manager.reset();});

		group = GetComponent<CanvasGroup>();
		hideScreen();
	}
	
	private void showScreen(){
		gameObject.SetActive(true);
	}
	
	private void hideScreen(){
		group.alpha = 0;
		group.interactable = false;
		group.blocksRaycasts = false;
	}
	
	void Destroy(){
		ManagerHub.onGoToEnd-=showScreen;
		ManagerHub.onGoToStart-=hideScreen;
		ManagerHub.onGoToGame-=hideScreen;
	}
}
