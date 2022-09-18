using System.Collections;
using UnityEngine;

public class BallRotation : MonoBehaviour
{
   [SerializeField] [Range(0, 1f)] float lerpTime;
   [SerializeField] Color collisionColor;

   private ArrowDirection arrowDirection;
   private MeshRenderer meshRenderer;

   private void Awake()
   {
      arrowDirection = FindObjectOfType<ArrowDirection>();
      meshRenderer = GetComponent<MeshRenderer>();
   }

   private void Update()
   {
      BallRotationX();
   }

   private void BallRotationX()
   {
      float x = arrowDirection.ballDirection.x;
      float z = arrowDirection.ballDirection.z;

      // Ball rotation based on speed
      transform.Rotate(((Mathf.Abs(x) + Mathf.Abs(z)) / 2f) * arrowDirection.vectorLength, 0, 0); 
   }

   public void CollisionColorChange()
   {
      StartCoroutine(ColorChange());
   }

   private IEnumerator ColorChange() // Flashing when hitting a wall
   {
      meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, collisionColor, lerpTime);
      yield return new WaitForSeconds(0.14f);
      meshRenderer.material.color = Color.white;
   }
}
