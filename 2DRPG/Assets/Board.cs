using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Holds and manages tiles containing units

public struct Square{

	public static Square getNullSquare(){
		return new Square(-1,-1);
	}

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
	public List<GameObject> deadUnits;

	public GridSlot(){
		deadUnits = new List<GameObject>();
		ManagerHub.onNewTurn+=clearDeadUnits;
	}

	public void setPosition(Vector3 pos){
		//deselect();//Not sure if this will hold up. this may need to live somewhere else.
		if(!hasBarrel())
			unit.GetComponent<Movement>().setPosition(pos);
	}

	public void setAnimation(SpriteMovement a){
		if(!hasBarrel())
			unit.GetComponent<Movement>().setNextAnimation(a);
	}

	public void select(){
		if(!hasBarrel())
			unit.GetComponent<Movement>().select();
	}

	public void deselect(){
		if(!hasBarrel())
			unit.GetComponent<Movement>().deselect();
	}

	public int getPlayerNumber(){
		if(hasBarrel())
			return -1;
		else
			return unit.GetComponent<Movement>().getPlayerNumber();
	}

	public bool hasUnit(){
		return unit!=null;
	}

	public bool hasBarrel(){
		if(unit != null){
			//Debug.Log (""+unit.CompareTag("Barrel")+" because tag = "+unit.tag);
			return unit.CompareTag("Barrel");
		}
		return false;
	}

	public void addUnit(GameObject u){
		if(hasUnit()){
			pendingUnit = u;
			if(hasBarrel()) 
				Debug.Log ("WARNING: Trying to move into a barrel");
		}else
			unit = u;

	}

	public void removeUnit(){
		unit = pendingUnit;
		pendingUnit = null;
	}

	public int getAttackDamage(){
		if(hasBarrel())
			return 0;
		else
			return unit.GetComponent<Movement>().getAttackDamage();
	}

	public int getReadyDamage(){
		if(hasBarrel())
			return 0;
		else
			return unit.GetComponent<Movement>().getReadyDamage();
	}

	public bool applyDamage(int damage){
		if(hasBarrel())
			return false;
		else
			return unit.GetComponent<Movement>().deductDamageFromHealth(damage);
	}

	public void kill(){
		deadUnits.Add(unit);
		unit = null;
	}

	private void clearDeadUnits(){
		foreach(GameObject u in deadUnits){
			u.GetComponent<Movement>().registerDeath();
			GameObject.Destroy(u);

		}
		deadUnits.Clear();
	}

	public void clearEvents(){
		ManagerHub.onNewTurn-=clearDeadUnits;
	}

	public void clearTile(){
		if(unit!=null){GameObject.Destroy(unit); unit = null;}
		if(pendingUnit!=null){GameObject.Destroy(pendingUnit); pendingUnit = null;}
	}

	public int getRange(){
		if(hasBarrel())
			return 0;
		else
			return unit.GetComponent<Movement>().getRange();
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

	}

//	public Square checkForTargetsInRange(Square attacker){
//		
//	}
//
//	public Square checkIfInRange(Square target){
//
//	}

	public bool applyAttackDamage(Square attacker, Square target){
		int damage = grid[attacker.x,attacker.y].getAttackDamage();
		return grid[target.x, target.y].applyDamage(damage);
	}

	public bool applyReadyDamage(Square attacker, Square target){
		int damage = grid[attacker.x,attacker.y].getReadyDamage();
		return grid[target.x, target.y].applyDamage(damage);
	}

	public int getRange(Square s){
		//Debug.Log ("checking range for ["+s.x+","+s.y+"]");
		return grid[s.x, s.y].getRange();
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

	public void kill(Square s){
		grid[s.x, s.y].kill();
	}

	public bool hasBarrel(Square s){
		return grid[s.x, s.y].hasBarrel();
	}

	public bool unitHasCover(Square shooter, Square target){
		int[] dirs = Direction.getRelativeDirection(shooter, target);
		if(dirs.Length==2){//dirs can only be 1 or 2 slots long
//			int subDirection = Direction.getDirectionDifference(dirs[0], dirs[1]); Well that was apparently a waste of time. :(
			return pathBlocked(shooter, 0, Direction.getStepsRequired(shooter, target, dirs[0]), Direction.getDirection(dirs[0]), 0, Direction.getStepsRequired(shooter, target, dirs[1]), Direction.getDirection(dirs[1]));
		}else{
			return pathBlocked (shooter, 0, Direction.getStepsRequired(shooter, target, dirs[0]), Direction.getDirection(dirs[0]));
		}
	}

	//Write the 2 directional version of this. also test;
	private bool pathBlocked(Square s, int stepsTaken, int maxSteps, Direction dir){

		if(stepsTaken>=maxSteps){
			return false;
		}else if(grid[s.x, s.y].hasBarrel()){
			return true;
		}else{
			return pathBlocked(new Square(s.x+dir.getX(), s.y+dir.getY()), stepsTaken+1, maxSteps, dir);
		}
	}

	private bool pathBlocked(Square s, int perpStepsTaken, int maxPerpSteps, Direction perpDirection, int diagStepsTaken, int maxDiagSteps, Direction diagDirection){
		if(perpStepsTaken == maxPerpSteps && diagStepsTaken == maxDiagSteps)
			return false;
		else if(grid[s.x, s.y].hasBarrel()){
				return true;
		}else{
			if(diagStepsTaken < diagStepsTaken){
				if(pathBlocked(new Square(s.x+diagDirection.getX(), s.y+diagDirection.getY()), perpStepsTaken, maxPerpSteps, perpDirection, diagStepsTaken + 1, maxDiagSteps, diagDirection)){
					if(perpStepsTaken < maxPerpSteps){
						return pathBlocked(new Square(s.x+perpDirection.getX(), s.y+perpDirection.getY()), perpStepsTaken+1, maxPerpSteps, perpDirection, diagStepsTaken, maxDiagSteps, diagDirection);
					}
				}
				return true;
			}else if(perpStepsTaken < maxPerpSteps){
				return pathBlocked(new Square(s.x+perpDirection.getX(), s.y+perpDirection.getY()), perpStepsTaken+1, maxPerpSteps, perpDirection, diagStepsTaken, maxDiagSteps, diagDirection);
			}else{
				return true;
			}
		}
	}

//	public 
	
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

	public void clearTiles(){
		for(int j = height-1; j>=0; j--){
			for(int i = 0; i<width; i++){
				grid[i,j].clearTile();
			}
		}
	}

	void Destroy(){
		for(int i = 0; i< width; i++){
			for(int j = 0; j<height; j++ ){
				grid[i,j].clearEvents();
			}
		}
	}
}
