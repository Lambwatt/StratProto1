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
			break;
		case NORTHEAST:
			return new Direction(1,1);
			break;
		case EAST:
			return new Direction(1,0);
			break;
		case SOUTHEAST:
			return new Direction(1,-1);
			break;
		case SOUTH:
			return new Direction(0,-1);
			break;
		case SOUTHWEST:
			return new Direction(-1,-1);
			break;
		case WEST:
			return new Direction(-1,0);
			break;
		case NORTHWEST:
			return new Direction(-1,1);
			break;
		case NORTH:
			return new Direction(0,1);
			break;
		default:

			return null;//FIXME figure out an exception to stick here.
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

