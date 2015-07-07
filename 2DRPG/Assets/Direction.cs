using UnityEngine;
using System.Collections;

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

	public int invertDirection(int direction){
		if(direction==0)
			return 0;
		else
			return (((direction - 1)+4)%8)+1;  

	}

	public Direction invertDirection(){
		return new Direction(-x, -y);
	}
	
}

