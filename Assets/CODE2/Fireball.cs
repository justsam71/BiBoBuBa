using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private Transform playerCenter;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform targetCamera;
    void Update()
    {
        this.gameObject.transform.LookAt(targetCamera);
        playerCenter.Rotate(new Vector3(0, rotationSpeed, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cloud")
        {
            // ???
        }
    }
}
