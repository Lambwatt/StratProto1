using UnityEngine;
using System.Collections;

public class Bullet: MonoBehaviour{
	

	SpriteMovement course;
	int delay = -1;

	public void setCourse(SpriteMovement c, int d){
		course = c;
		delay = d;
	}

	void Update () {
		if(delay>-1){
			if(delay == 0){
				if(course.complete()){
					//Debug.Log ("Destroying");
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
