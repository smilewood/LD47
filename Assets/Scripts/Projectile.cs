using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD47
{
   public class Projectile : MonoBehaviour
   {
      public Vector3 velocity;
      public float impactDamage;

      public AudioSource impactSound;

      // Start is called before the first frame update
      void Start()
      {

      }

      // Update is called once per frame
      void Update()
      {
         this.transform.position = this.transform.position + (velocity * Time.deltaTime);
      }

      private void OnCollisionEnter2D(Collision2D collision)
      {
         if(collision.gameObject.tag == "Projectile")
         {
            return;
         }
         if (collision.gameObject.GetComponent<DamageableCar>() is DamageableCar target)
         {
            target.TakeDamage(impactDamage);
            impactSound.Play();
         }
         GetComponent<ParticleSystem>().Play();
         GetComponent<SpriteRenderer>().enabled = false;
         this.enabled = false;
      }
      
      
   }
}