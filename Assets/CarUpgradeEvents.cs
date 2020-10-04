using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LD47
{
   public enum Car
   {
      Blue = 0, Green = 2, Pink = 1
   }
   public class CarUpgradeEvents : MonoBehaviour
   {
      public UpgradableCar Blue, Green, Pink;
      public GameStats MoneyTracker;
      public GameObject PinkUnlock, GreenUnlock;

      public void UnlockCar(int car)
      {
         switch ((Car)car)
         {
            case Car.Blue:
            {
               //unlocked at start
               break;
            }
            case Car.Green:
            {
               if (MoneyTracker.TrySpendMoney(5000))
               {
                  Green.UnlockCar();
                  GreenUnlock.SetActive(false);
               }
               break;
            }
            case Car.Pink:
            {
               if (MoneyTracker.TrySpendMoney(500))
               {
                  Pink.UnlockCar();
                  PinkUnlock.SetActive(false);
               }
               break;
            }
         }
      }

      public void UpgradeSpeed(int car)
      {
         switch ((Car)car)
         {
            case Car.Blue:
            {
               if (MoneyTracker.TrySpendMoney(Blue.SpeedUpgradeCost))
               {
                  Blue.UpgradeSpeed();
               }
               else
               {
                  //flash red please
               }
               break;
            }
            case Car.Green:
            {
               if (MoneyTracker.TrySpendMoney(Green.SpeedUpgradeCost))
               {
                  Green.UpgradeSpeed();
               }
               else
               {
                  //flash red please
               }
               break;
            }
            case Car.Pink:
            {
               if (MoneyTracker.TrySpendMoney(Pink.SpeedUpgradeCost))
               {
                  Pink.UpgradeSpeed();
               }
               else
               {
                  //flash red please
               }
               break;
            }
         }
      }

      public void UpgradeArmor(int car)
      {
         switch ((Car)car)
         {
            case Car.Blue:
            {
               if (MoneyTracker.TrySpendMoney(Blue.ArmorUpgradeCost))
               {
                  Blue.UpgradeHealth();
               }
               else
               {
                  //flash red please
               }
               break;
            }
            case Car.Green:
            {
               if (MoneyTracker.TrySpendMoney(Green.ArmorUpgradeCost))
               {
                  Green.UpgradeHealth();
               }
               else
               {
                  //flash red please
               }
               break;
            }
            case Car.Pink:
            {
               if (MoneyTracker.TrySpendMoney(Pink.ArmorUpgradeCost))
               {
                  Pink.UpgradeHealth();
               }
               else
               {
                  //flash red please
               }
               break;
            }
         }

      }

      public void RepairCar(int car)
      {
         switch ((Car)car)
         {
            case Car.Blue:
            {
               DamageableCar carHealth = Blue.gameObject.GetComponent<DamageableCar>();
               if (MoneyTracker.TrySpendMoney(carHealth.RepairCost))
               {
                  carHealth.RepairCar();
               }
               break;
            }
            case Car.Green:
            {
               DamageableCar carHealth = Green.gameObject.GetComponent<DamageableCar>();
               if (MoneyTracker.TrySpendMoney(carHealth.RepairCost))
               {
                  carHealth.RepairCar();
               }
               break;
            }
            case Car.Pink:
            {
               DamageableCar carHealth = Pink.gameObject.GetComponent<DamageableCar>();
               if (MoneyTracker.TrySpendMoney(carHealth.RepairCost))
               {
                  carHealth.RepairCar();
               }
               break;
            }
         }

      }
   }
}