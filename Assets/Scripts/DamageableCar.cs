using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LD47
{
   [RequireComponent(typeof(UpgradableCar))]
   [RequireComponent(typeof(FollowPath))]
   public class DamageableCar : MonoBehaviour
   {
      UpgradableCar carStats;
      FollowPath driver;
      float currentHP, currentMaxHP;
      public ParticleSystem brokenSmoke;
      private int timesRepaired = 0;
      public Button RepairButton;
      public int repairCostMultiplier;

      public int RepairCost
      {
         get
         {
            return 200 * timesRepaired * repairCostMultiplier;
         }
      }


      // Start is called before the first frame update
      void Start()
      {
         carStats = this.gameObject.GetComponent<UpgradableCar>();
         driver = this.gameObject.GetComponent<FollowPath>();
         currentHP = currentMaxHP = carStats.MaxHP;
         UpdateRepairButtonText();
         RepairButton.interactable = false;
      }

      public void RepairCar()
      {
         ++timesRepaired;
         currentHP = carStats.MaxHP;
         driver.Restart();
         brokenSmoke.Stop();
         brokenSmoke.gameObject.GetComponent<AudioSource>().Stop();
         this.gameObject.GetComponent<AudioSource>().Play();
         RepairButton.interactable = false;
         UpdateRepairButtonText();
      }

      public void TakeDamage(float ammount)
      {
         currentHP -= ammount;
         if (currentHP <= 0)
         {
            driver.Broken();
            brokenSmoke.Play();
            brokenSmoke.gameObject.GetComponent<AudioSource>().Play();
            this.gameObject.GetComponent<AudioSource>().Stop();
            RepairButton.interactable = true;
         }
      }

      private void UpdateRepairButtonText()
      {
         RepairButton.GetComponentInChildren<Text>().text = string.Format("Repair: {0}", RepairCost);
      }

      public void HealthUpgraded()
      {
         float missingHP = currentMaxHP - currentHP;
         currentMaxHP = carStats.MaxHP;
         currentHP = currentMaxHP - missingHP;
      }
   }
}