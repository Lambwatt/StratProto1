using UnityEngine;
using System.Collections;

public class ManagerHub : MonoBehaviour {

	public Board board;

	// Use this for initialization
	void Start () {
		board = GetComponent<Board>();
		board.sayHi();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
