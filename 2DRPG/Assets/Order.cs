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
	}

	public int getMagnitude(){
		return magnitude;
	}

	public void setMagnitude(int i){
		magnitude = i;
	}

	public void setSquares(List<Square> l){
		squares = l;
	}

	public void setOrderKey(string s){
		orderKey = s;
	}

	public void setDirection(int d){
		direction = d;
	}

	public List<Command> getCommands(){

		List<Command> res = new List<Command>();

		//great opportunity for currying if c# allows it and you want to show off.
		foreach(Square s in squares){
			res.Add(commandFactory.getCommand(orderKey, s, direction)); 
		}

		return res;

	}
}
