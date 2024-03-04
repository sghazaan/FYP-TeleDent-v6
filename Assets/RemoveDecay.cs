using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveDecay : MonoBehaviour
{
    //public Animation deletionAnimation;

    private bool collided = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "drill" && !collided)
        {
            collided = true;
           // deletionAnimation.Play(); // Trigger deletion animation
            // Destroy(gameObject, deletionAnimation.clip.length); // Destroy the cube after the animation finishes
            Destroy(collision.gameObject);
        }
    }
}
