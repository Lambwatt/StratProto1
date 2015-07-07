using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Holds and manages tiles containing units

public struct Square{
	public int x { get; private set; }
	public int y { get; private set; }

	public Square(int x,int y) : this(){ this.x = x;
		this.y = y;}

//	public override int GetHashCode()
//	{
//		int hash = 17;
//		hash = hash * 31 + X;
//		hash = hash * 31 + Y;
//		return hash;
//	}
	
	public override bool Equals(object o)
	{
		return o is Square ? Equals((Square)o) : false;
	}
	
	public bool Equals(Square o)
	{
		return x == o.x &&
			y == o.y;
	}	
}


public class GridSlot{

	public GameObject unit;
	public GameObject pendingUnit;


	public void setPosition(Vector3 pos){

		//deselect();//Not sure if this will hold up. this may need to live somewhere else.
		unit.GetComponent<Movement>().setPosition(pos);
	}

	public void setAnimation(SpriteMovement a){
		unit.GetComponent<Movement>().setNextAnimation(a);
	}

	public void select(){

		unit.GetComponent<Movement>().select();
	}

	public void deselect(){

		unit.GetComponent<Movement>().deselect();
	}

	public int getPlayerNumber(){
		return unit.GetComponent<Movement>().getPlayerNumber();
	}

	public bool hasUnit(){
		return unit!=null;
	}

	public void addUnit(GameObject u){
		if(hasUnit())
			pendingUnit = u;
		else
			unit = u;
	}

	public void removeUnit(){
		unit = pendingUnit;
		pendingUnit = null;
	}
}


public class Board : MonoBehaviour{//Make this not a game object.

	public int height;
	public int width;
	public GridSlot[,] grid;//manage this better later. Yeah, this will eventually wind up being a dictionary
		
	public Square convertMouseClickToBoardCoords(Vector3 click){//find or creat coordinate type.
		return new Square(Mathf.FloorToInt(click.x/32) + (width/2),Mathf.FloorToInt(click.y/32) + (height/2));
	}

	public Vector3 convertBoardSquaresToWorldCoords(Square s){//find or creat coordinate type.
		return new Vector3((s.x - (width/2)+0.5f)*32, (s.y - (height/2)+0.5f)*32, 0);
	}
	
	// Use this for initialization
	void Awake() {
		grid = new GridSlot[width, height];//
		for(int i = 0; i< width; i++){
			for(int j = 0; j<height; j++ ){
				grid[i,j]=new GridSlot();
			}
		}
	}

	public List<Square> getActiveSquares(){

		List<Square> result = new List<Square>();
//
//		for(int i = 0; i<width; i++){
//			for(int j = 0; j<height; j++){
//				if(grid[i,j]!=null){
//
//					result.Add(new Square(i,j));
//				}
//			}
//		}

		return result;
	}

	public bool squareInBounds(Square s, Direction d){//add default so this can be used to select positions
		return s.x+d.getX() >= 0 && s.x+d.getX() < width && s.y+d.getY()>=0 && s.y+d.getY() < height;

	}

	//COPY THIS ONE
	public TurnMetaData.Answer moveAllowed(Square s, Direction d){
		if(squareInBounds(s,d) && !(s.x+d.getX()==s.x && s.y+d.getY() == s.y)){
			if(grid[s.x+d.getX(), s.y+d.getY()].unit==null)
				return TurnMetaData.Answer.YES;
			else
				return TurnMetaData.Answer.MAYBE;
		}else
			return TurnMetaData.Answer.NO;
	}

	public int getPlayerNumber(Square s){
		return grid[s.x,s.y].getPlayerNumber();
	}

	public bool isOccupied(Square s){
		return grid[s.x,s.y].hasUnit();
	}

	//COPY THIS ONE
	public void move(Square start, Direction dir){

		//grid[start.x, start.y].setPosition(convertBoardSquaresToWorldCoords(new Square(start.x+dir.getX(),start.y+dir.getY())));

		Square end = new Square(start.x+dir.getX(), start.y+dir.getY());
		grid[end.x, end.y].addUnit(grid[start.x, start.y].unit);
		grid[start.x, start.y].removeUnit();

		//Debug.Log("["+end.x+":"+start.x+"]|["+end.y+":"+start.y+"]");
//		if(end.x!=start.x || end.y!=start.y)
//			grid[start.x, start.y].unit = null;
	}

	//hopefully this one can be removed
	public void move(Vector3 start, Vector3 end){

		Square cStart = convertMouseClickToBoardCoords(start);
		Square cEnd = convertMouseClickToBoardCoords(end);

		grid[cEnd.x, cEnd.y].addUnit(grid[cStart.x, cStart.y].unit);
		grid[cStart.x, cStart.y].removeUnit();
//		if(end.x!=start.x || end.y!=start.y)
//			grid[cStart.x, cStart.y].unit = null;
	}

	//Moves contents of one tile to another and clears the previous tile if it is now empty
	public void move(Square start, Square end){
		
		grid[end.x, end.y].addUnit(grid[start.x, start.y].unit);
		grid[start.x, start.y].removeUnit();
		//Debug.Log("["+end.x+":"+start.x+"]|["+end.y+":"+start.y+"]");
//		if(end.x!=start.x || end.y!=start.y)
//			grid[start.x, start.y].unit = null;
	}

	public void resetPosition(Square s){
			grid[s.x, s.y].setPosition(convertBoardSquaresToWorldCoords(s));
	}

	//Adds a unit to the board at a location
	public void register(GameObject unit, Vector3 pos){
		//FIXME needs to return information about occupancy

		Square cPos = convertMouseClickToBoardCoords(pos);
		if(grid[cPos.x, cPos.y].unit==null)
			grid[cPos.x, cPos.y].unit = unit;
	}

	public bool register(GameObject unit, Square s){

		if(!grid[s.x, s.y].hasUnit()){
			grid[s.x, s.y].addUnit(unit);
			return true;
		}else
			return false;

	}

	public Square getFreeSquare(){
		Square s;
		//int countdown = ;
		do{
			s = new Square((int)Mathf.Floor(Random.value*width),(int)Mathf.Floor(Random.value*height));
		}while(grid[s.x, s.y].hasUnit() /*&& countdown>0*/);
		return s;
	}

	public void selectSquareContents(Square s){
		grid[s.x, s.y].select();
	}
	
	public void deselectSquareContents(Square s){
		grid[s.x, s.y].deselect();
	}

	public void setAnimation(Square s, SpriteMovement a){
		grid[s.x, s.y].setAnimation(a);
	}
	
//	// Update is called once per frame. Eventually, this should not be a behaviour
	void Update () {

		//in response to p key, print the board
		if(Input.GetKeyDown(KeyCode.P)){
			string result = "";
			for(int j = height-1; j>=0; j--){
				for(int i = 0; i<width; i++){
					result+= grid[i,j].hasUnit()? "O":"X";
				}
				result+="\n";
			}
			Debug.Log(result);
		}

	}

	public void reset(){

	}
}
