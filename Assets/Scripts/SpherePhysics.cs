using UnityEngine;

public class SpherePhysics : MonoBehaviour
{
   [SerializeField] private float mass;
   [SerializeField] private float gravity;
   [SerializeField] private float forceValue;

   private float impulse = 12;
   private float acceleration = 0.2f;

   private void FixedUpdate()
   {
      CalculatePhysics();
   }

   private void CalculatePhysics()
   {
      // If the ball is not on the ground, then gravity acts on it, otherwise it will bounce off the ground

      if (transform.position.y > 1 || acceleration < 0)
      {
         if (acceleration != 0.2f)
         {
            acceleration += 0.05f;
         }
         transform.position -= new Vector3(0, mass * gravity * Time.deltaTime, 0);
         gravity += acceleration;
      }
      else
      {
         transform.position = new Vector3(transform.position.x, 1, transform.position.z);

         // Gradual decrease in bounce after hitting the ground
         if (transform.position.y <= 1)
         {
            if (impulse >= 1)
            {
               impulse /= 3;
               Bouncy(acceleration / 4);
            }
         }
      }
   }

   private void Bouncy(float forceValue)
   {
      gravity = -gravity;
      gravity /= 2;
      acceleration = -forceValue;
   }

   public void AddForce(float forceValue)
   {
      gravity = forceValue * 90;
      acceleration = 0.2f;
      impulse = 12;
   }
}
