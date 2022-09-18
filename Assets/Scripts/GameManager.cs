using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; } // Singleton pattern

   [SerializeField] Text coinsText;
   [SerializeField] private GameObject[] coinsToWin;
   [SerializeField] private int coinValue;
   private int currentCoins;

   [SerializeField] private int maxAttempts;
   public Text attemptsText;
   public int currentAttempts;

   [SerializeField] Text speedText;
   [SerializeField] Text powerText;
   private ArrowDirection arrowDirection;

   [SerializeField] GameObject victoryPanel;
   [SerializeField] GameObject lossPanel;

   private void Awake()
   {
      Instance = this;

      arrowDirection = FindObjectOfType<ArrowDirection>();
      currentAttempts = maxAttempts;
   }

   private void Update()
   {
      TextUpdate();
      Checks();
   }

   private void TextUpdate()
   {
      float speed = Mathf.Round(arrowDirection.vectorLength * arrowDirection.ballDirection.magnitude * 10);
      float power = Mathf.Round(arrowDirection.arrowDirection.magnitude * 10);

      powerText.text = power.ToString();
      speedText.text = speed.ToString();
      attemptsText.text = currentAttempts.ToString();
      coinsText.text = currentCoins.ToString();
   }

   private void Checks()
   {
      if (currentCoins >= (coinsToWin.Length * coinValue))
         victoryPanel.SetActive(true);

      if (currentAttempts <= 0)
         lossPanel.SetActive(true);
   }

   public IEnumerator AddCoins()
   {
      coinsText.GetComponent<Animator>().enabled = true;
      for (int i = 0; i < coinValue; i++)
      {
         currentCoins++;
         yield return new WaitForSeconds(0.01f);
      }

      coinsText.GetComponent<Animator>().enabled = false;
   }
}
