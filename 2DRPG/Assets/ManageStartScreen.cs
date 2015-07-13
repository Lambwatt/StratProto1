using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManageStartScreen : MonoBehaviour {

	ManagerHub manager;
	Button startButton;
	CanvasGroup group;
	
	void Start(){
		
		manager = GameObject.Find("manager").GetComponent<ManagerHub>();
		ManagerHub.onGoToGame+=hideScreen;
		ManagerHub.onGoToStart+=showScreen;

		startButton = GameObject.FindWithTag("StartButton").GetComponent<Button>();
		startButton.onClick.AddListener(()=>{manager.startGame();});

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
		ManagerHub.onGoToGame-=hideScreen;
		ManagerHub.onGoToStart-=showScreen;
	}
}
