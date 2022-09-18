using UnityEngine;

public class Rebound : MonoBehaviour
{
   [SerializeField] float forceValue;
   [SerializeField] ParticleSystem wallCollisionParticle;
   [SerializeField] private ParticleSystem coinParticle;

   private ArrowDirection arrowDirection;
   private SpherePhysics spherePhysics;
   private Rotator rotator;
   private BallRotation ballRotation;

   private void Awake()
   {
      arrowDirection = FindObjectOfType<ArrowDirection>();
      spherePhysics = FindObjectOfType<SpherePhysics>();
      ballRotation = FindObjectOfType<BallRotation>();
      rotator = FindObjectOfType<Rotator>();
   }

   private void Update()
   {
      RayCollisionCheck(); // Additional ray collision check
      Movement();
   }

   private void RayCollisionCheck()
   {
      Vector3 direction = arrowDirection.ballDirection;
      Ray ray = new Ray(transform.position, transform.TransformDirection(direction * 5));

      if (Physics.Raycast(ray, out RaycastHit hit, 0.6f, 3, QueryTriggerInteraction.Ignore))
      {
         Instantiate(wallCollisionParticle, transform.position, hit.transform.rotation);

         arrowDirection.ballDirection = Vector3.Reflect(arrowDirection.ballDirection, hit.normal);
         AddVariousPhysics();
      }
   }

   private void OnCollisionEnter(Collision collision) // Basic collision check
   {
      Instantiate(wallCollisionParticle, transform.position, collision.transform.rotation); ;

      Vector3 wallNormal = collision.contacts[0].normal;
      arrowDirection.ballDirection = Vector3.Reflect(arrowDirection.ballDirection, wallNormal);

      AddVariousPhysics();
   }

   private void OnTriggerEnter(Collider other) // Coin picking
   {
      Coin coin = other.GetComponent<Coin>();
      if (coin)
      {
         Instantiate(coinParticle, transform.position, coinParticle.transform.rotation); ;
         StartCoroutine(GameManager.Instance.AddCoins());
         Destroy(other.gameObject);
      }
   }

   private void Movement()
   {
      // Speed of ball
      if (arrowDirection.vectorLength > 0)
         arrowDirection.vectorLength -= Time.deltaTime / 4;
      else
         arrowDirection.vectorLength = 0;

      float x = arrowDirection.ballDirection.x * Time.deltaTime * 3 * (arrowDirection.vectorLength / 2);
      float z = arrowDirection.ballDirection.z * Time.deltaTime * 3 * (arrowDirection.vectorLength / 2);
      transform.Translate(x, 0, z);
   }

   private void AddVariousPhysics()
   {
      spherePhysics.AddForce(forceValue);
      ballRotation.CollisionColorChange();
      rotator.CollisionScaleChange();
   }
}
