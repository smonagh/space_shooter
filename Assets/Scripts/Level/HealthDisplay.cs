using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
  public Text healthText;
  public GameSession gameSession;
  // Start is called before the first frame update
  void Start()
  {
      healthText = GetComponent<Text>();
      gameSession = FindObjectOfType<GameSession>();
  }

  // Update is called once per frame
  void Update()
  {
      string newHealth = gameSession.GetHealth().ToString();
      string writeHealth = string.Format("Health: {0}", newHealth);
      healthText.text = writeHealth;
  }

}
