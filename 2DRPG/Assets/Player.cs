using UnityEngine;
using System.Collections;

public class Player {

	Sprite[] sprites;
	Order order;
	int playerNumber;
	int units;

	public Player(int i, Sprite idle, Sprite shootAimed, Sprite shootHit, Sprite ready, Sprite hit, Sprite dead){
		playerNumber  = i;
		sprites = new Sprite[6]{idle, shootAimed, shootHit, ready, hit, dead};
		units = 0;
	}

	public Order getOrder(){
		return order;
	}

	public void setOrder(Order o){
		order = o;
	}

	public Sprite getSprite(){
		return sprites[0];
	}

	public Sprite[] getSprites(){
		return sprites;
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