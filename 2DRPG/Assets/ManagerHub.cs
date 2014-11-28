using UnityEngine;
using System.Collections;

public class ManagerHub : MonoBehaviour {

	public Board board;
	public Selector selector;

	// Use this for initialization
	void Awake () {
		board = GetComponent<Board>();
		selector = GetComponent<Selector>();
	}

	public void test(){
		board.sayHi();
	}
	// Update is called once per frame
	void Update () {
	
	}
}
