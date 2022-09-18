using System.Collections;
using UnityEngine;

public class Rotator : MonoBehaviour
{
   private ArrowDirection arrowDirection;

   private void Awake()
   {
      arrowDirection = FindObjectOfType<ArrowDirection>();
   }

   private void Update()
   {
      BallRotationY();
   }

   private void BallRotationY()
   {

      Quaternion rot = Quaternion.LookRotation(arrowDirection.ballDirection, Vector3.up);
      transform.rotation = Quaternion.Lerp(transform.rotation, rot, 10.5f * Time.deltaTime);
   }

   public void CollisionScaleChange()
   {
      StartCoroutine(ScaleChange());
   }

   private IEnumerator ScaleChange() // Animation of ball squeezing when hitting a wall
   {
      transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z - 0.2f);
      yield return new WaitForSeconds(0.02f);
      transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z - 0.2f);

      yield return new WaitForSeconds(0.02f);
      transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0.7f);
      yield return new WaitForSeconds(0.02f);
      transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0.85f);
      yield return new WaitForSeconds(0.02f);
      transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1f);
   }
}
