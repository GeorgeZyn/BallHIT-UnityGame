using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   [SerializeField] private float smoothSpeed;
   [SerializeField] private Vector3 offset;

   private Transform player;


   private void Awake()
   {
      player = FindObjectOfType<Rebound>().GetComponent<Transform>();
   }

   private void LateUpdate()
   {
      transform.position = Vector3.Lerp(transform.position, player.position + offset, smoothSpeed * Time.deltaTime);
   }
}
