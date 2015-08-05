using UnityEngine;
using System.Collections;

public class ManageDirectionLabel : MonoBehaviour {
	
	ManagerHub manager;
	CanvasGroup group;
	
	void Start(){
		
		manager = GameObject.Find("manager").GetComponent<ManagerHub>();
		ManagerHub.onPlayerChange+=hideUI;
		ManagerHub.onOrderSelect+=showUI;
		group = GetComponent<CanvasGroup>();
		
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
	
	void OnDestroy(){
		ManagerHub.onPlayerChange-=hideUI;
		ManagerHub.onOrderSelect-=showUI;
	}
}
