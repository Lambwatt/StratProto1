using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Direction{

	public const int NONE=0;
	public const int NORTHEAST=1;
	public const int EAST=2;
	public const int SOUTHEAST=3;
	public const int SOUTH=4;
	public const int SOUTHWEST=5;
	public const int WEST=6;
	public const int NORTHWEST=7;
	public const int NORTH=8;

	//private static int[,] comboDirections = {,,,,,,,};

	private static int[] diagonals = {SOUTHWEST, NORTHWEST, SOUTHEAST, NORTHEAST};
	
	private int x;
	private int y;
	
	public Direction(int x, int y){
		
		this.x = x;
		this.y = y;
	}

	public int getX(){
		return x;
	}
	
	public int getY(){
		return y;
	}

	public static Direction getDirection(int d){
		switch(d){
		case NONE:
			return new Direction(0,0);
		case NORTHEAST:
			return new Direction(1,1);
		case EAST:
			return new Direction(1,0);
		case SOUTHEAST:
			return new Direction(1,-1);
		case SOUTH:
			return new Direction(0,-1);
		case SOUTHWEST:
			return new Direction(-1,-1);
		case WEST:
			return new Direction(-1,0);
		case NORTHWEST:
			return new Direction(-1,1);
		case NORTH:
			return new Direction(0,1);
		default:
			//Debug.Log ("undefined direction detected!");
			return null;//FIXME figure out an exception to stick here.
		}
	}

	public static string getDirectionString(int d){
		switch(d){
		case NONE:
			return "NONE";
		case NORTHEAST:
			return "NORTHEAST";
		case EAST:
			return "EAST";
		case SOUTHEAST:
			return "SOUTHEAST";
		case SOUTH:
			return "SOUTH";
		case SOUTHWEST:
			return "SOUTHWEST";
		case WEST:
			return "WEST";
		case NORTHWEST:
			return "NORTHWEST";
		case NORTH:
			return "NORTH";
		default:
			//Debug.Log ("undefined direction detected!");
			return "ERROR: NOT A VALID DIRECTION";//FIXME figure out an exception to stick here.
		}
	}

	private static int[] getComboDirection(int ind){
		switch(ind){
		case 0:
			return new int[2]{SOUTH, SOUTHWEST};
		case 1:
			return new int[2]{NORTH, NORTHWEST};
		case 2:
			return new int[2]{SOUTH, SOUTHEAST};
		case 3:
			return new int[2]{NORTH, NORTHEAST};
		case 4:
			return new int[2]{WEST, SOUTHWEST};
		case 5:
			return new int[2]{WEST, NORTHWEST};
		case 6:
			return new int[2]{EAST, SOUTHEAST};
		case 7:
			return new int[2]{EAST, NORTHEAST};
		default:
			return new int[0];
		}
	}

	public int invertDirection(int direction){
		if(direction==0)
			return 0;
		else
			return (((direction - 1)+4)%8)+1;  

	}

	public Direction invertDirection(){
		return new Direction(-x, -y);
	}

	public static int[] getRelativeDirection(Square s, Square t){//gets directions from s to t
		int xDiff = Mathf.Abs(s.x - t.x);
		int yDiff = Mathf.Abs(s.y - t.y);

		if(xDiff == 0){
			if(t.y>s.y){
				return new int[1]{NORTH};
			}else{
				return new int[1]{SOUTH};
			}
		}else if(yDiff == 0){
			if(t.x>s.x){
				return new int[1]{EAST};
			}else{
				return new int[1]{WEST};
			}
		}else if(xDiff == yDiff){
			int ind = (boolToInt(s.x<t.x))*2 + (boolToInt(s.y<t.y))*1;
			return new int[1] {diagonals[ind]};
		}else{
			int ind = (boolToInt(xDiff>yDiff))*4 + (boolToInt(s.x<t.x))*2 + (boolToInt(s.y<t.y))*1;
			return getComboDirection(ind);
		}
	}

	private static int boolToInt(bool b){
		return b ? 1 : 0;
	}

	public static void testDirections(){
		Square shooter = new Square(3,3);

		for(int x = 0; x<7; x++){
			for(int y = 0; y<7; y++){
				string res = "["+x+","+y+"]:";
				foreach(int i in getRelativeDirection(shooter, new Square(x,y))){
					res+= Direction.getDirectionString(i)+" ";
				}
				Debug.Log (res);
			}
		}
	}
}

