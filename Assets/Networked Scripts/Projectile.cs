using UnityEngine;

namespace UNET_Multiplayer.Assets.Networked_Scripts
{
	public class Projectile : MonoBehaviour 
	{	
		public float lifeTime;
		void OnCollisionEnter(Collision other)
		{
			if (other.gameObject.CompareTag ("Target"))
			{
				Destroy();
			}

			if (other.gameObject.CompareTag ("Border"))
			{
				Destroy();
			}

			if (other.gameObject.CompareTag ("Projectile"))
			{
				Destroy();
			}

			// Destroy(gameObject,lifeTime);															
		}

		void Destroy()
		{
			gameObject.SetActive(false);	
		}	
	
	}
}


