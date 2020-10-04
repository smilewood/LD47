using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD47
{
   public class ProjectileLauncher : MonoBehaviour
   {
      public GameObject ProjectilePrefab;
      public GameObject ProjectileParent;
      public float range;
      public float projectileSpeed;
      public float projectileDamage;
      public float fireRate;
      private float cooldown;

      private AudioSource fireSound;

      // Start is called before the first frame update
      void Start()
      {
         cooldown = 1f/fireRate;
         fireSound = GetComponent<AudioSource>();
      }
      private void OnDrawGizmosSelected()
      {
         Gizmos.DrawWireSphere(this.transform.position, range);  
      }

      // Update is called once per frame
      void Update()
      {

         if (ClosestTargetInRange() is GameObject target)
         {
            Vector3 towordsTarget = Vector3.Normalize(target.transform.position - this.gameObject.transform.position);
            float angle = (Mathf.Atan2(towordsTarget.y, towordsTarget.x) * Mathf.Rad2Deg);
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            this.transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 5);

            if (cooldown <= 0)
            {
               //fire
               cooldown = 1f/fireRate;
               Projectile projectile = Instantiate(ProjectilePrefab, this.transform.position, this.transform.rotation, ProjectileParent.transform).GetComponent<Projectile>();
               projectile.velocity = towordsTarget * projectileSpeed;
               projectile.impactDamage = projectileDamage;
               fireSound.Play();
            }
         }
         cooldown -= cooldown <= 0 ? 0 : Time.deltaTime;
      }

      private GameObject ClosestTargetInRange()
      {
         float closeDist = float.MaxValue;
         GameObject closeCar = null;
         foreach (GameObject car in GameObject.FindGameObjectsWithTag("Car"))
         {
            if (!car.GetComponent<FollowPath>().broken)
            {
               float dist = Vector3.Distance(this.gameObject.transform.position, car.transform.position);
               if (dist < closeDist && dist < range)
               {
                  closeDist = dist;
                  closeCar = car;
               }
            }
            
         }
         return closeCar;
      }
   }
}