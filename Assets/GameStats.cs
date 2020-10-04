using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace LD47
{
   public class GameStats : MonoBehaviour
   {
      public UpgradableCar Blue, Green, Pink;
      public GameObject GameOverScreen;
      public MenuEvents menuEvents;
      public static GameStats GameMaster
      {
         get
         {
            if(GM is null)
            {
               throw new Exception("There should always be exactly one GameStats object. Where is it?");
            }
            return GM;
         }
      }
      private static GameStats GM;

      public int money = 0;
      public Text currentMoneyText;

      // Start is called before the first frame update
      void Start()
      {
         if(GM is null)
         {
            GM = this;
         }
         else
         {
            throw new Exception("There should always be exactly one GameStats object. Why are we trying to make a new one?");
         }
         WriteMoneyText();
      }
      private void OnDestroy()
      {
         if(this == GM)
         {
            GM = null;
         }
      }
      bool gameOver = false;
      // Update is called once per frame
      void Update()
      {
         if (!gameOver && Green.gameObject.GetComponent<FollowPath>().broken && Blue.gameObject.GetComponent<FollowPath>().broken && Pink.gameObject.GetComponent<FollowPath>().broken && totalRepairCost > money)
         {
            //gameOver
            gameOver = true;
            menuEvents.PauseGame();
            GameOverScreen.SetActive(true);
            GameOverScreen.transform.Find("ProgressText").GetComponent<Text>().text = string.Format("You made it to Round {0}, completing {1} laps!", GameObject.Find("Sidelines").GetComponent<RoundManager>().currentRound, laps);
         }
      }

      private int totalRepairCost
      {
         get
         {
            int cost = Green.gameObject.GetComponent<FollowPath>().broken ? Green.gameObject.GetComponent<DamageableCar>().RepairCost : 0;
            cost += Blue.gameObject.GetComponent<FollowPath>().broken ? Blue.gameObject.GetComponent<DamageableCar>().RepairCost : 0;
            return cost + (Pink.gameObject.GetComponent<FollowPath>().broken ? Pink.gameObject.GetComponent<DamageableCar>().RepairCost : 0);
         }
      }

      public bool TrySpendMoney(int ammount)
      {
         if (ammount > money)
         {
            return false;
         }
         money -= ammount;
         WriteMoneyText();
         return true;
      }
      private void WriteMoneyText()
      {
         currentMoneyText.text = string.Format("Money: {0}", money);
      }
      int laps = 0;
      public void ReachedCheckpoint(int moneyGain)
      {
         ++laps;
         money += moneyGain;
         WriteMoneyText();
      }
   }
}