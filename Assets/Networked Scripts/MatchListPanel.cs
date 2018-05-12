namespace UNET_Multiplayer.Assets.Networked_Scripts
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Networking.Match;

    public class MatchListPanel : MonoBehaviour
    {
        [SerializeField]
        private GameObject ButtonPrefab;

        private void Awake()
        {
            AvailableMatchesList.OnAvailableMatchesChanged+=AvailableMatchesList_OnAvailableMatchesChanged;
        }

        private void AvailableMatchesList_OnAvailableMatchesChanged(List<MatchInfoSnapshot> matches)
        {
           ClearExistingButtons();
           CreateNewJoinGameButtons(matches);
        }

        private void CreateNewJoinGameButtons(List<MatchInfoSnapshot> matches)
        {
           foreach (var match in matches)
           {
                var button = Instantiate(ButtonPrefab);
                button.GetComponent<JoinRoom>().Initialize(match,transform);               
           }
        }

        private void ClearExistingButtons()
        {
            var buttons = GetComponentsInChildren<JoinRoom>();
            foreach (var button in buttons)
            {
                Destroy(button.gameObject);
            }
        }


    }
}