namespace UNET_Multiplayer.Assets.Networked_Scripts
{
  	using UnityEngine;
	using UnityEngine.Networking;
	using UnityEngine.UI;

	public class NetworkPlayerManager : NetworkBehaviour {

	#region Variables

	public PlayerController activePlayer;
	public GameObject Ball;
	public float Power = 10f;
	public PlayerController player1,player2,player3;
	public Renderer player1color,player2color,player3color;
	public Transform PlayerFormation;
	public Button b1,b2,b3;
	public Canvas myCanvas;
	public Camera myCamera;

	private int playerNumber;
	private Transform obj;
	private string targetTag="Target";
		
	#endregion

	#region Built-in Functions

		void Start ()
		{	
			if (!isLocalPlayer)
			{
				AssignTags();	
				transform.name="Not Local Player";					
				return;
			}		
			
			if (!isServer)
			{
				CmdChangeColor();
			}

			CmdTurn();			
		}

		void Update () 
		{
			if (!isLocalPlayer)		
				return;	

			// if (Input.GetMouseButtonDown(0)&&activePlayer.isGround)
			// {			
			// 	Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			// 	RaycastHit hit = new RaycastHit();                  
			// 	if (Physics.Raycast(ray, out hit))
			// 	{
			// 		if (hit.transform.tag==targetTag&&!hit.transform.GetComponent<PlayerController>().is_hit&&hit.transform.GetComponent<PlayerController>().isGround)
			// 		{	
			// 			obj=hit.transform;				
			// 			CmdFire(obj.position,Power);									
			// 			CmdPlayer();    
			// 		}				
			// 	}		
			// }

			if (Input.touchCount == 1&&Input.GetTouch(0).phase==TouchPhase.Began&&activePlayer.isGround)
			{			
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit = new RaycastHit();                  
				if (Physics.Raycast(ray, out hit))
				{
					if (hit.transform.tag==targetTag&&!hit.transform.GetComponent<PlayerController>().is_hit&&hit.transform.GetComponent<PlayerController>().isGround)
					{	
						obj=hit.transform;				
						CmdFire(obj.position,Power);									
						CmdPlayer();    
					}				
				}		
			}
			
		}
		
	#endregion

	#region Override

		public override void OnStartLocalPlayer()
		{
			ActivateEssentials();			
			AssignButtons();		
			SetName("Local Player");
		}

	#endregion

	#region Commands
		
		[Command]
		public void CmdFire(Vector3 objPosition,float Power)
		{  
			activePlayer.Fire(objPosition,Power);	
			RpcFire(objPosition,Power);
		}

		[Command]
		public void CmdChangeColor()
		{
			ChangeColorRed();
			RpcChangeColor();
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

	#endregion

	#region RPCs

		[ClientRpc]
		public void RpcFire(Vector3 objPosition,float Power)
		{
			if (!isServer)
			{
				activePlayer.Fire(objPosition,Power);		
			}			
		}

		[ClientRpc]
		public void RpcChangeColor()
		{		
			ChangeColorRed();
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

	#endregion


	#region Other Functions

		public void SetName(string name)
		{
			transform.name=name;
		}

		public void AssignTags()
		{
			transform.tag=targetTag;
			player1.tag=targetTag;
			player2.tag=targetTag;
			player3.tag=targetTag;
		}

		public void AssignButtons()
		{
			b1.onClick.AddListener(player1.Jump);
			b2.onClick.AddListener(player2.Jump);
			b3.onClick.AddListener(player3.Jump);
		}

		public void ActivateEssentials()
		{
			playerNumber = 1;		
			myCamera.gameObject.SetActive(true);
			myCanvas.gameObject.SetActive(true);
		}

		public void ChangeColorRed()
		{
			player1color.material.color = Color.red;
			player2color.material.color = Color.red;
			player3color.material.color = Color.red;		
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
				case 1:
						player1.isReady=false;                      
						player2.isReady = true;
						player3.isReady = false;
						activePlayer=player2;										          
						break;

				case 2:
						player1.isReady=false;                      
						player2.isReady = false;
						player3.isReady = true;
						activePlayer=player3; 					                        
						break;

				case 3:
						player1.isReady=true;                      
						player2.isReady = false;
						player3.isReady = false;
						activePlayer=player1;					                
						break;
			}
		} 	
		
	#endregion	

	}
}
