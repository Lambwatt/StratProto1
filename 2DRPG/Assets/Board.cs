using UnityEngine;
using System.Collections;

public struct Square{
	public int x;
	public int y;
}

public class Board : MonoBehaviour {

	public int height;
	public int width;
	public GameObject[,] grid;//manage this better later.
		
	public Square convertMouseClickToBoardCoords(Vector3 click){//find or creat coordinate type.
		Square result;
		result.x = Mathf.FloorToInt(click.x) + (width/2);
		result.y = Mathf.FloorToInt(click.y) + (height/2);
		//click.z = 0;
		//Debug.Log(reuslt);
		return result;
	}

	public void sayHi(){
		//Debug.Log("Hello World");
	}
	// Use this for initialization
	void Awake() {
		grid = new GameObject[width, height];
	}

	public void move(Vector3 start, Vector3 end){
		Square cStart = convertMouseClickToBoardCoords(start);
		Square cEnd = convertMouseClickToBoardCoords(end);

		grid[cEnd.x, cEnd.y] = grid[cStart.x, cStart.y];
		grid[cStart.x, cStart.y] = null;
	}

	public void move(Square start, Square end){
		//start = convertMouseClickToBoardCoords(start);
		//end = convertMouseClickToBoardCoords(end);
		
		grid[end.x, end.y] = grid[start.x, start.y];
		grid[start.x, start.y] = null;
	}

	public void register(GameObject unit, Vector3 pos){
		Square cPos = convertMouseClickToBoardCoords(pos);
		grid[cPos.x, cPos.y] = unit;
	}
	
	// Update is called once per frame
	void Update () {


	}
}
