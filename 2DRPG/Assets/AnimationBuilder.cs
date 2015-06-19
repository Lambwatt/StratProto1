using UnityEngine;
using System.Collections;

public class AnimationBuilder {//Just build for an infantry move

	Square start;
	Square end;
	//Eventually, you'll have to choose the sprite too.

	AnimationBuilder(Square s){

	}

	public SimpleMoveAnimation build(){
		return new SimpleMoveAnimation(start.x, start.y, end.x, end.y, 0);
	}
}
