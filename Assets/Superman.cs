using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Superman : MonoBehaviour
{
    public float force = 10f; // ñèëà óäàðà 
    public float forceTouch = 3f; // ñèëà óäàðà ìÿ÷èêîâ


    private int goodboyLayer;
    private int badguyLayer;
    private Rigidbody rb;

    private void Start()
    {
        goodboyLayer = LayerMask.NameToLayer("GoodBoy");
        badguyLayer = LayerMask.NameToLayer("BadGuy");
        rb = GetComponent<Rigidbody>();

        Physics.IgnoreLayerCollision(gameObject.layer, goodboyLayer, true);

        if (rb != null)
        {
            Vector3 forceDirection = new Vector3(-1, 0, 0).normalized; // íàïðàâëåíèå ñèëû óäàðà
            rb.AddForce(forceDirection * force, ForceMode.Impulse); // äîáàâëåíèå èìïóëüñà äëÿ äâèæåíèÿ îáúåêòà
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (rb != null && collision.gameObject.GetComponent<Rigidbody>() != null)
        {
            int collisionLayer = collision.gameObject.layer;
            
            if (collisionLayer == badguyLayer)
            {
                Vector3 forceDirection = collision.contacts[0].point - transform.position; // íàïðàâëåíèå íà öåíòð îáúåêòà
                float forceMagnitude = forceDirection.magnitude; // âåëè÷èíà ñèëû óäàðà
                forceDirection.Normalize(); // íîðìàëèçàöèÿ íàïðàâëåíèÿ ñèëû

                // äîáàâëåíèå ñèëû óäàðà äëÿ îáîèõ îáúåêòîâ
                rb.AddForce(-forceDirection * forceMagnitude, ForceMode.Impulse);
                collision.gameObject.GetComponent<Rigidbody>().AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
             
            }
        }

    }

}
