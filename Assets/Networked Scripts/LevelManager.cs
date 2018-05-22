using UnityEngine;
using UnityEngine.SceneManagement;

namespace UNET_Multiplayer.Assets.Networked_Scripts
{
    public class LevelManager : MonoBehaviour
    {
        public void OnClickLeaveGame()
        {
            SceneManager.LoadScene("Menu");            
        }
    }
}