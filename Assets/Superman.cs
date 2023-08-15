using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Superman : MonoBehaviour
{
    public float force = 10f; // сила удара 
    public float forceTouch = 3f; // сила удара м€чиков


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
            Vector3 forceDirection = new Vector3(-1, 0, 0).normalized; // направление силы удара
            rb.AddForce(forceDirection * force, ForceMode.Impulse); // добавление импульса дл€ движени€ объекта
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (rb != null && collision.gameObject.GetComponent<Rigidbody>() != null)
        {
            int collisionLayer = collision.gameObject.layer;

            //if (collisionLayer == goodboyLayer)
            //{
            //    Physics.IgnoreCollision(collision.collider, GetComponent<Collider>(), true);
            //}
            if (collisionLayer == badguyLayer)
            {
                Vector3 forceDirection = collision.contacts[0].point - transform.position; // направление на центр объекта
                float forceMagnitude = forceDirection.magnitude; // величина силы удара
                forceDirection.Normalize(); // нормализаци€ направлени€ силы

                // добавление силы удара дл€ обоих объектов
                rb.AddForce(-forceDirection * forceMagnitude, ForceMode.Impulse);
                collision.gameObject.GetComponent<Rigidbody>().AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
                // collision.gameObject.GetComponent<Rigidbody>().AddForce(forceDirection * forceTouch, ForceMode.Impulse); // добавление силы удара
            }
        }

    }

}