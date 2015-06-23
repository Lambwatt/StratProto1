using UnityEngine;
using System.Collections;

public class SpriteMovement {
	
	string spriteName;
	Curve curve;
	Vector3 start;
	Vector3 end;
	int totalFrames;

	int frame = 1;
	SpriteMovement next;

	public SpriteMovement(string name, Curve c, Vector3 s, Vector3 e, int tot){
		spriteName = name;
		curve = c;
		start = s;
		end = e;
		totalFrames = tot;
	}

	public Vector3 getStep(){
		if(totalFrames==0)
			return end;
		else{
			float step = curve.getProgress(((float)frame)/(float)totalFrames);
			frame++;
			return new Vector3(start.x+step*(end.x-start.x), start.y+step*(end.y-start.y));
		}
	}

	public bool hasNext(){
		return next!=null;
	}

	public bool complete(){
		if(totalFrames>0){
			//Debug.Log(frame+"<"+totalFrames+":"+(frame < totalFrames));
			return frame > totalFrames;
		}else {
			return false;
		}
	}

	public string getSpriteName(){
		return spriteName;
	}


	public SpriteMovement getNext(){
		return next;
	}

	public void setNext(SpriteMovement a){

		if(next == null){
			//garbage collect next?
			next = a;
		}else{
			next.setNext(a);
		}
	}
}
