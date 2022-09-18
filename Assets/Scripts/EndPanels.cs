using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPanels : MonoBehaviour
{
   public void ReloadScene()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }
}
