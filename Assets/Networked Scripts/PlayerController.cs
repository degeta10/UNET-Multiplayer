using UnityEngine;
using UnityEngine.Networking;

namespace UNET_Multiplayer.Assets.Networked_Scripts {
	public class PlayerController : MonoBehaviour {

		#region Variables

		public bool is_hit = false;
		public bool isGround = false;
		public bool isReady = false;
		public Rigidbody rb;
		public float JumpPower;
		public Transform shooter;
		private float Stuntime = 0.5f;
		private Vector3 StunHeight;
		private ObjectPooler objectPooler;
		private float stunHeight;

		#endregion

		#region Functions

		void Start () {
			objectPooler = ObjectPooler.Instance;
			stunHeight = 2f;
			StunHeight = new Vector3 (transform.position.x, stunHeight, transform.position.z);
		}

		void Update () {

		}

		private void Is_Ground () {
			isGround = true;
		}

		private void Is_NotGround () {
			isGround = false;
		}

		private void Is_Hit () {
			is_hit = true;
		}

		private void Is_NotHit () {
			is_hit = false;
		}

		void OnCollisionEnter (Collision other) {
			if (other.gameObject.CompareTag ("Ground")) {
				Is_Ground ();
				Is_NotHit ();
			}

			if (other.gameObject.CompareTag ("Projectile")) {
				Is_Hit ();
				Stun ();
				Is_NotGround ();
				Invoke ("UnStun", Stuntime);
			}
		}

		public void Stun () {
			transform.position = StunHeight;
			rb.isKinematic = true;
		}

		private void UnStun () {
			rb.isKinematic = false;
		}

		public void Jump () {
			if (isGround)
				rb.velocity = new Vector3 (0f, JumpPower, 0f);
			Is_NotGround ();
		}

		public void Fire (Vector3 obj, float Power) {
			var ball = objectPooler.SpawnFromPool ("Projectile", shooter.position, Quaternion.identity);
			ball.GetComponent<Rigidbody> ().velocity = (obj - shooter.position).normalized * Power;
		}

		#endregion

	}

}