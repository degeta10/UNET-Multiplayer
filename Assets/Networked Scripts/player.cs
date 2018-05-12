using UnityEngine;
using UnityEngine.Networking;

public class player : NetworkBehaviour {
	
	public GameObject bulletPrefab;
	public Transform shooter;
	public int activePlayer;

	private int playerNumber=1;

	void Start () {

		playerNumber=1;
		if (!isServer)
		{
			CmdSendName("Client");
						
		}
		CmdTurn();		
	}

	[Command]
	public void CmdSendName(string name)
	{
		RpcUpdateName(name);
	}

    [ClientRpc]
	public void RpcUpdateName(string name)
	{
		transform.name=name;
	}

	[Command]
	public void CmdTurn()
	{
		TurnDecider();
		RpcTurn();
	}

	[Command]
	public void CmdPlayer()
	{
		PlayerSelector();
		RpcPlayer();
	}

	[Command]
	public void CmdFire()
	{
		Fire();
		RpcFire();		
	}

	[ClientRpc]
	public void RpcFire()
	{
		if (!isServer)
		{
			Fire();
		}
	}

	[ClientRpc]
	public void RpcTurn()
	{
		if (!isServer)
		{
			TurnDecider();
		}
	}

	[ClientRpc]
	public void RpcPlayer()
	{
		if (!isServer)
		{
			PlayerSelector();
		}
	}

	void Update()
	{
		if (!isLocalPlayer)
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			CmdFire();
			CmdPlayer();
		}
	}

	public void Fire()
	{
		GameObject ball = Instantiate(bulletPrefab,shooter.position, rotation: Quaternion.identity) as GameObject;
		ball.GetComponent<Rigidbody>().velocity = shooter.transform.forward*20f;
	}

	public void PlayerSelector()
    {
        if (playerNumber == 3)        
            playerNumber = 1;       
        else       
            playerNumber++;
        
        TurnDecider();
    }

    public void TurnDecider()
    {
        switch (playerNumber)
        {
            case 1: activePlayer=2;										          
                	break;

            case 2: activePlayer=3; 					                        
               		break;

            case 3: activePlayer=1;					                
              		break;
        }
    } 	
	
}
