using UnityEngine;
using System.Collections;

public class SimpleMoveAnimation {

	Vector2 step;

	public SimpleMoveAnimation(int startX, int startY, int endX, int endY, int numFrames){
		step = new Vector2( (endX-startX)/numFrames, (endY-startY)/numFrames );
	}

	public void setUp(){

	}
	
	public void nextStep(){
		//subject.position += (Vector3) step;
	}

	public void cleanUp(){

	}
}
