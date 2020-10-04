using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace LD47
{
   public class UpgradableCar : MonoBehaviour
   {
      public float Speed;
      public float MaxHP;
      public float UpgradeCostMultiplier;
      public bool unlockAtStart;


      public Text SpeedLevelText, ArmorLevelText;
      public Button ArmorButton, SpeedButton;

      int speedLevel = 1, healthLevel = 1;

      public int SpeedUpgradeCost
      {
         get
         {
            return (int)((float)speedLevel * ((float)speedLevel / 2f) * 100 * UpgradeCostMultiplier);
         }
      }

      public int ArmorUpgradeCost
      {
         get
         {
            return (int)((float)healthLevel * ((float)healthLevel / 2f) * 100 * UpgradeCostMultiplier);
         }
      }

      // Start is called before the first frame update
      void Start()
      {
         UpdateHealthLevelText();
         UpdateSpeedLevelText();
         if (unlockAtStart)
         {
            this.UnlockCar();
         }
      }

      // Update is called once per frame
      void Update()
      {

      }

      public void UpgradeSpeed()
      {
         ++this.speedLevel;
         this.Speed = speedLevel;

         UpdateSpeedLevelText();
      }

      public void UpgradeHealth()
      {
         ++this.healthLevel;
         this.MaxHP = healthLevel * 5;
         GetComponent<DamageableCar>().HealthUpgraded();
         UpdateHealthLevelText();
      }

      private void UpdateHealthLevelText()
      {
         ArmorLevelText.text = string.Format("{0}/5", healthLevel-1);

         ArmorButton.transform.GetComponentInChildren<Text>().text = string.Format("{0}", ArmorUpgradeCost);
         ArmorButton.interactable = healthLevel <= 5;
      }


      private void UpdateSpeedLevelText()
      {
         SpeedLevelText.text = string.Format("{0}/5", speedLevel-1);

         SpeedButton.transform.GetComponentInChildren<Text>().text = string.Format("{0}", SpeedUpgradeCost);
         SpeedButton.interactable = speedLevel <= 5;
      }

      public void UnlockCar()
      {
         transform.GetComponentInChildren<SpriteRenderer>().enabled = true;
         GetComponent<BoxCollider2D>().enabled = true;
         GetComponent<DamageableCar>().RepairCar();
      }

   }
}