using UnityEngine;
using System.Collections;

public class Player {

	Sprite sprite;
	Order order;
	int playerNumber;
	int units;

	public Player(int i, Sprite s){
		playerNumber  = i;
		sprite = s;
		units = 0;
	}

	public Order getOrder(){
		return order;
	}

	public void setOrder(Order o){
		order = o;
	}

	public Sprite getSprite(){
		return sprite;
	}

	public void addUnit(){
		units++;
	}

	public void removeUnit(){
		units--;
	}

	public bool allUnitsLost(){
		return units==0;
	}

	public void clearUnits(){
		units = 0;
	}

}