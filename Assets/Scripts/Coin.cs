using UnityEngine;

public class Coin : MonoBehaviour
{
   [SerializeField] float speedRotate;

   private void FixedUpdate()
   {
      transform.Rotate(0, speedRotate * Time.deltaTime, 0);   
   }
}
