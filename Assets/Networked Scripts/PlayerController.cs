using UnityEngine;
using UnityEngine.Networking;
public class PlayerController : MonoBehaviour {

#region Variables

	public GameObject Ball;
 	public bool is_hit=false;  
    public bool isGround = false;
	public bool isReady = false;   
	public Rigidbody rb;
	public float JumpPower;
	public Transform shooter;
	public float Power = 10f;

	private float Stuntime=0.5f;
    private Vector3 StunHeight; 
	private float stunHeight;

    #endregion

    #region Functions

    void Start()
    {
        stunHeight = 2f;
        StunHeight = new Vector3(transform.position.x, stunHeight, transform.position.z);
    }

	private void Is_Ground()
	{
		isGround = true;
	}

	private void Is_NotGround() 
	{
		isGround = false;
	}
	
    private void Is_Hit()
	{
		is_hit = true;
	}

	private void Is_NotHit() 
	{
		is_hit = false;
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Is_Ground();
            Is_NotHit ();          
        }

        if (other.gameObject.CompareTag ("Projectile")) 
		{
			Is_Hit ();
			Stun();
            Invoke("UnStun",Stuntime);  
		}       
    }

    public void Stun()
	{
		transform.position = StunHeight;        
		rb.isKinematic=true;            
	}

    private void UnStun()
    {       
        rb.isKinematic=false;
    }

	public void Jump()
    {
        if (isGround)
            rb.velocity = new Vector3(0f, JumpPower, 0f);
        Is_NotGround();		
    }

	public void Fire(Vector3 obj)
	{
		GameObject ball = Instantiate(Ball,shooter.position,Quaternion.identity) as GameObject;
		ball.GetComponent<Rigidbody>().velocity = (obj -shooter.position).normalized * Power;		
	}

#endregion

}
