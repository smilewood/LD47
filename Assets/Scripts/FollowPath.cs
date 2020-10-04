using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD47
{
   [RequireComponent(typeof(UpgradableCar))]
   public class FollowPath : MonoBehaviour
   {
      private UpgradableCar carStats;
      public PathNode currentTarget;
      private int laps;
      public bool broken;
      public int moneyPerLap;

      // Start is called before the first frame update
      void Start()
      {
         carStats = this.GetComponent<UpgradableCar>();
         if (currentTarget is null)
         {
            currentTarget = GameObject.Find("PathNode").GetComponent<PathNode>();
         }
      }

      public void Restart(PathNode target)
      {
         this.currentTarget = target;
         this.laps = 0;
         this.broken = false;
      }

      public void Restart()
      {
         this.Restart(this.currentTarget);
      }

      public void Broken()
      {
         broken = true;
      }

      // Update is called once per frame
      void Update()
      {
         if (broken)
         {
            return;
         }

         this.transform.position = Vector3.MoveTowards(this.transform.position, currentTarget.transform.position, Time.deltaTime * carStats.Speed);

         Vector3 vectorToTarget = currentTarget.transform.position - transform.position;
         float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
         Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
         transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 5 * carStats.Speed);

         if (Vector3.Distance(this.transform.position, currentTarget.transform.position) < .05)
         {
            if (currentTarget.Checkpoint)
            {
               int moneyGain = (int)(moneyPerLap * (++laps / 2f));
               GameStats.GameMaster.ReachedCheckpoint(moneyGain);
            }
            currentTarget = currentTarget.nextNode;
         }
      }
   }
}