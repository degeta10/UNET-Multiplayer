using UnityEngine;

namespace UNET_Multiplayer.Assets.Networked_Scripts {
	public class Projectile : MonoBehaviour {
		public float lifeTime;

		private ScoreManager scoreManager;

		public void Start () {
			scoreManager = GameObject.Find ("Local Player").GetComponent<ScoreManager> ();
		}

		void OnCollisionEnter (Collision other) {
			if (other.gameObject.CompareTag ("Target")) {
				scoreManager.Cmd_Calculate_Score (1, 0);
				scoreManager.Cmd_Player_Hit ();
				Destroy ();
			}

			if (other.gameObject.CompareTag ("Player")) {
				scoreManager.Cmd_Calculate_Score (0, 1);
				scoreManager.Cmd_Player_Hit ();
				Destroy ();
			}

			if (other.gameObject.CompareTag ("Border")) {
				Destroy ();
			}

			if (other.gameObject.CompareTag ("Projectile")) {
				Destroy ();
			}
		}

		void Destroy () {
			gameObject.SetActive (false);
		}

	}
}