using UnityEngine;
using System.Collections;

public class Player {

	Sprite sprite;
	Order order;
	int playerNumber;

	public Player(int i, Sprite s){
		playerNumber  = i;
		sprite = s;
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


}