using System;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

namespace UNET_Multiplayer.Assets.Networked_Scripts
{
    public class CustomNetworkManager : NetworkManager
    {
        
        // public void OnClickStartHost()
        // {
        //     StartMatchMaker();
        //     matchMaker.CreateMatch("new Match",4,true,"","","",0,0,OnMatchCreated);
            
        // }

        // private void OnMatchCreated(bool success, string extendedInfo, MatchInfo responseData)
        // {
        //     base.StartHost(responseData);
        // }

        // private void OnEnable()
        // {
        //     RefreshList();
        // }

        // private void RefreshList()
        // {
        //     if (matchMaker==null)
        //     {
        //         StartMatchMaker();
        //     }

        //     matchMaker.ListMatches(0,10," ",true,0,0,HandleListMatchesComplete);
        // }

        // private void HandleListMatchesComplete(bool success, string extendedInfo, List<MatchInfoSnapshot> responseData)
        // {
        //     AvailableMatchesList.HandleNewMatchList(responseData);
        // }
    }
}