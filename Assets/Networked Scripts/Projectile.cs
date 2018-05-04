using UnityEngine;

public class Projectile : MonoBehaviour {	

	public float lifeTime;
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag ("Target"))
		{
			Debug.Log("Ball will be Destroyed After Hitting target");			
			Destroy(gameObject);
		}
		Destroy(gameObject,lifeTime);												
	}	
	
}
