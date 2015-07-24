using UnityEngine;
using System.Collections;

public class Bullet: MonoBehaviour{
	

	SpriteMovement course;
	int delay = -1;
	Vector3 destination;
	bool barrel;

	public void setCourse(SpriteMovement c, int d, Vector3 dest, bool b){
		course = c;
		delay = d;
		destination = dest;
		barrel = b;
	}

	void Update () {
		if(delay>-1){
			if(delay == 0){
				if(course.complete()){
					//Debug.Log ("Destroying");
					if(barrel){
						GameObject.Instantiate(Resources.Load<GameObject>("BarrelHit"), transform.position, Quaternion.identity);//blood effect
					}else{
						GameObject.Instantiate(Resources.Load<GameObject>("Blood"), transform.position, Quaternion.identity);//miss poof
					}

					GameObject.Destroy(this.gameObject);
				}else{
					transform.position = course.getStep();
				}
			}else{
				delay--;
			}
		}

	}

	public void setPosition(Vector3 dest){
		transform.position = dest;
	}

}
