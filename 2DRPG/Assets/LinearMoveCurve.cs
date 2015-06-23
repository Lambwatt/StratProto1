using UnityEngine;
using System.Collections;

public class LinearMoveCurve : Curve {
	
	public LinearMoveCurve(CurveFunction func){
		;//Do nothing since the curve is linnear
	}

	public float getProgress(float t){
		if(t>=0 && t<=1)
			return t;
		else{
			Debug.Log ("WARNING: Out of bounds time index "+t+" requested.");
			return 1.0f;
		}
	}
}
