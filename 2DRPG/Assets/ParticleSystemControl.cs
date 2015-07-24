using UnityEngine;
using System.Collections;

public class ParticleSystemControl : MonoBehaviour 
{	
	public void Update() 
	{
		Destroy(gameObject, 10000.0f);
	}
}
