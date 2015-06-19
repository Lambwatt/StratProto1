using UnityEngine;
using System.Collections;

public class SimpleMoveAnimation {

	Vector2 step;
	Transform subject;//Maybe an animation. Figure out what this should be later when you have internetz.

	public SimpleMoveAnimation(int startX, int startY, int endX, int endY, int numFrames){
		step = new Vector2( (endX-startX)/numFrames, (endY-startY)/numFrames );
		this.subject = subject;
	}

	public void setUp(){

	}
	
	public void nextStep(){
		subject.position += (Vector3) step;
	}

	public void cleanUp(){

	}
}
