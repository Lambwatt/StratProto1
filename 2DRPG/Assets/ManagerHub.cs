using UnityEngine;
using System.Collections;

public class ManagerHub : MonoBehaviour {

	public Board board;

	// Use this for initialization
	void Awake () {
		board = GetComponent<Board>();

	}

	public void test(){
		board.sayHi();
	}
	// Update is called once per frame
	void Update () {
	
	}
}
