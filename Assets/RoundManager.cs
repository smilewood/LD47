using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LD47
{
   public class RoundManager : MonoBehaviour
   {
      public List<GameObject> rounds;
      public int currentRound;
      public float roundTimer;
      public float currentRoundTimer;
      public Text roundAnnounceText;

      public GameObject winMenu;
      public MenuEvents menuEvents;

      // Start is called before the first frame update
      void Start()
      {
         foreach (GameObject round in rounds)
         {
            round.SetActive(false);
         }
         rounds[currentRound].SetActive(true);

         currentRoundTimer = roundTimer;
         StartCoroutine(showRoundText(0));
      }
      bool won = false;
      // Update is called once per frame
      void Update()
      {
         if (won)
            return;
         currentRoundTimer -= Time.deltaTime;
         if(currentRoundTimer <= 0)
         {
            //next round
            rounds[currentRound].SetActive(false);
            ++currentRound;
            if(currentRound == rounds.Count)
            {
               //win, yay
               won = true;
               winMenu.SetActive(true);
               menuEvents.PauseGame();
               return;
            }
            StartCoroutine(showRoundText(currentRound));
            rounds[currentRound].SetActive(true);
            currentRoundTimer = roundTimer;
         }
         //show round progress bar somewhere
      }

      IEnumerator showRoundText(int round)
      {
         roundAnnounceText.text = string.Format("Round {0}", round+1);
         roundAnnounceText.gameObject.SetActive(true);
         yield return new WaitForSeconds(2);
         roundAnnounceText.gameObject.SetActive(false);
      }
   }
}