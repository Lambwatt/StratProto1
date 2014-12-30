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
		if(end.x!=start.x || end.y!=start.y)
			grid[cStart.x, cStart.y] = null;
	}

	public void move(Square start, Square end){
		Debug.Log("HELLO!?");
		//start = convertMouseClickToBoardCoords(start);
		//end = convertMouseClickToBoardCoords(end);
		
		grid[end.x, end.y] = grid[start.x, start.y];

		//Debug.Log("["+end.x+":"+start.x+"]|["+end.y+":"+start.y+"]");
		if(end.x!=start.x || end.y!=start.y)
			grid[start.x, start.y] = null;
	}

	public void register(GameObject unit, Vector3 pos){
		Square cPos = convertMouseClickToBoardCoords(pos);
		grid[cPos.x, cPos.y] = unit;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.P)){
			string result = "";
			for(int j = height-1; j>=0; j--){
				for(int i = 0; i<width; i++){
					result+= grid[i,j]==null? "O":"X";
				}
				result+="\n";
			}
			Debug.Log(result);
		}

	}
}
