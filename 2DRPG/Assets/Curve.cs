using UnityEngine;
using System.Collections;

public delegate float CurveFunction (float frac);

public interface Curve{
		
	float getProgress(float time);
}
