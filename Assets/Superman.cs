using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Superman : MonoBehaviour
{
    public float force = 10f; // ���� ����� 
    public float forceTouch = 3f; // ���� ����� �������


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
            Vector3 forceDirection = new Vector3(-1, 0, 0).normalized; // ����������� ���� �����
            rb.AddForce(forceDirection * force, ForceMode.Impulse); // ���������� �������� ��� �������� �������
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
                Vector3 forceDirection = collision.contacts[0].point - transform.position; // ����������� �� ����� �������
                float forceMagnitude = forceDirection.magnitude; // �������� ���� �����
                forceDirection.Normalize(); // ������������ ����������� ����

                // ���������� ���� ����� ��� ����� ��������
                rb.AddForce(-forceDirection * forceMagnitude, ForceMode.Impulse);
                collision.gameObject.GetComponent<Rigidbody>().AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
                // collision.gameObject.GetComponent<Rigidbody>().AddForce(forceDirection * forceTouch, ForceMode.Impulse); // ���������� ���� �����
            }
        }

    }

}