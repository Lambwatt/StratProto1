using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Order  {

	string orderKey;
	int direction;
	int magnitude;
	List<Square> squares;
	CommandFactory commandFactory;

	public Order(CommandFactory cf){
		commandFactory = cf;
		orderKey = "none";
		direction = 0;
		magnitude = 1;
		squares = new List<Square>();
	}

	public int getMagnitude(){
		return magnitude;
	}

	public void setMagnitude(int i){
		magnitude = i;
	}

	public void setSquares(List<Square> l){
		squares.Clear();
		foreach(Square s in l){
			squares.Add(s);
		}
	}

	public List<Square> getSquares(){
		return squares;
	}

	public void setOrderKey(string s){
		orderKey = s;
		Debug.Log ("set order key to "+orderKey);
	}

	public string getKey(){
		return orderKey;
	}

	public void setDirection(int d){
		direction = d;
	}

	public int getDirection(){
		return direction;
	}

	public List<Command> getCommands(){

		List<Command> res = new List<Command>();

		//great opportunity for currying if c# allows it and you want to show off.
		foreach(Square s in squares){
			res.Add(commandFactory.getCommand(orderKey, s, direction, magnitude)); 
		}

		return res;

	}

	public void print(){
		string result = "{";

		result+="Key: "+orderKey;

		result+=" Squares: [ ";
		foreach(Square s in squares){
			result+="("+s.x+", "+s.y+")";
		}
		result+="]";

		result +=" Direction: "+Direction.getDirectionString(direction);

		result+= " Magnitude: "+magnitude+"}";

		Debug.Log(result);

	}
}
