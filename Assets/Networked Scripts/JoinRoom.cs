namespace UNET_Multiplayer.Assets.Networked_Scripts
{
    using UnityEngine;
    using UnityEngine.Networking.Match;
    using UnityEngine.UI;

    public class JoinRoom : MonoBehaviour
    {
        private Text text;

        private void Awale()
        {
            text = GetComponentInChildren<Text>();            
        }

        public void Initialize(MatchInfoSnapshot match, Transform panelTransform)
        {
            text.text=match.name;
            transform.SetParent(panelTransform);
            transform.localScale=Vector3.one;
            transform.localRotation=Quaternion.identity;
            transform.localPosition=Vector3.zero;                     

        }
         
    }
}