using UnityEngine;

public class ArrowDirection : MonoBehaviour
{
   [SerializeField] Transform arrowKeeper; // Arrow visual direction

   [HideInInspector] public float vectorLengthScale;
   [HideInInspector] public Vector3 arrowDirection;

   [HideInInspector] public float vectorLength;
   [HideInInspector] public Vector3 ballDirection;

   private void Update()
   {
      MouseInputs();
      ArrowRotate();
   }

   private void MouseInputs()
   {
      if (Input.GetMouseButtonDown(0))
      {
         arrowKeeper.gameObject.SetActive(true);
      }
      if (Input.GetMouseButton(0))
      {
         ArrowRay(); // Visual arrow direction
      }
      if (Input.GetMouseButtonUp(0))
      { 
         BallRay(); // The direction and force with which the ball rolls
         GameManager.Instance.currentAttempts--; // Taking away a try after a hit
         GameManager.Instance.attemptsText.GetComponent<Animator>().SetTrigger("Loss"); // Animate text after shrinking
         arrowKeeper.gameObject.SetActive(false); 
      }
   }

   private void ArrowRotate()
   {
      arrowKeeper.rotation = Quaternion.LookRotation(new Vector3(-arrowDirection.x, 0, -arrowDirection.z), Vector3.up);
   }

   private void BallRay()
   {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

      if (Physics.Raycast(ray, out RaycastHit hit))
      {
         ballDirection = arrowKeeper.position - hit.point;
         vectorLength = Mathf.Clamp(ballDirection.magnitude, 0, 2);
      }
   }

   private void ArrowRay()
   {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

      if (Physics.Raycast(ray, out RaycastHit hit))
      {
         arrowDirection = arrowKeeper.position - hit.point;
         vectorLengthScale = arrowDirection.magnitude;
         arrowKeeper.localScale = new Vector3(arrowKeeper.localScale.x, arrowKeeper.localScale.y, -vectorLengthScale);
      }
   }
}
