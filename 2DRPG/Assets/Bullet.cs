using UnityEngine;
using System.Collections;

public class Bullet: MonoBehaviour{
	

	SpriteMovement course;

	public void setCourse(SpriteMovement c, float angle){
		Debug.Log ("Set the bloody course!");
		course = c;
		transform.Rotate(Vector3.forward,Mathf.Rad2Deg*angle-45);
		Debug.Log("rotation in deg:"+transform.rotation.z );
	}

	void Update () {

		if(course.complete()){
			//Debug.Log ("Destroying");
			GameObject.Destroy(this.gameObject);
		}else{
			transform.position = course.getStep();
		}

	}

	public void setPosition(Vector3 dest){
		transform.position = dest;
	}

}
