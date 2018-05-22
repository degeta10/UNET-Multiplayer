using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace UNET_Multiplayer.Assets.Networked_Scripts {
    public class ScoreManager : NetworkBehaviour {
        
        #region Variables

        public int PlayerScore;
        public int OpponentScore;
        public Text PlayerScoreText, OpponentScoreText;
        public bool Player_Is_Hit = false;

        #endregion

        #region Event Functions

        void Start () {
            if (!isLocalPlayer)
                return;

            PlayerScore = 0;
            OpponentScore = 0;
            CmdDisplayScore (PlayerScore, OpponentScore);
        }

        void Update () {
            if (!isLocalPlayer)
                return;

            if (Player_Is_Hit) {              
                CmdDisplayScore (PlayerScore, OpponentScore);
                Cmd_Player_Not_Hit();
            }
        }

        #endregion

        #region My Functions 

        void DisplayScore (int pscore, int oscore) {
            PlayerScoreText.text = pscore.ToString ();
            OpponentScoreText.text = oscore.ToString ();
        }

        void Calculate_Score (int pscore, int oscore) {
            PlayerScore += pscore;
            OpponentScore += oscore;
        }

        #endregion

        #region Commands

        [Command]
        public void CmdDisplayScore (int pscore, int oscore) {
            DisplayScore (pscore, oscore);
            RpcDisplayScore (pscore, oscore);
        }

        [Command]
        public void Cmd_Calculate_Score (int pscore, int oscore) {
            Calculate_Score (pscore, oscore);
            Rpc_Calculate_Score (pscore, oscore);
        }

        [Command]
        public void Cmd_Player_Not_Hit () {
            Player_Is_Hit = false;
            Rpc_Player_Not_Hit ();
        }

        [Command]
        public void Cmd_Player_Hit () {
            Player_Is_Hit = true;
            Rpc_Player_Hit ();
        }

        #endregion

        #region RPCs          

        [ClientRpc]
        public void RpcDisplayScore (int pscore, int oscore) {
            if (!isServer) {
                DisplayScore (pscore, oscore);
            }
        }

        [ClientRpc]
        public void Rpc_Calculate_Score (int pscore, int oscore) {
            if (!isServer) {
                Calculate_Score (pscore, oscore);
            }
        }

        [ClientRpc]
        public void Rpc_Player_Not_Hit () {
            if (!isServer) {
                Player_Is_Hit = false;
            }
        }

        [ClientRpc]
        public void Rpc_Player_Hit () {
            if (!isServer) {
                Player_Is_Hit = true;
            }
        }

        #endregion

    }
}