using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudTargetScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public float mouseSensitivity;
    [SerializeField] LayerMask heightMask;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rawMoveDelta = new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));
        Vector3 processedDelta = Quaternion.Euler(0, 45, 0) * rawMoveDelta;
        transform.position = transform.position + processedDelta * mouseSensitivity;
        if (transform.position.x > 60) {
            transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        } else if (transform.position.x < -50) {
            transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        } else if (transform.position.z > 60) {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z- 1);
        } else if (transform.position.z < -45) {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        }
        RaycastHit rayHit;
        Physics.Raycast(new Ray(transform.position, Vector3.down), out rayHit, heightMask);
        if(rayHit.distance < 4.4f && rayHit.distance != 0) {
            transform.position = new Vector3(transform.position.x, transform.position.y + (4.4f - rayHit.distance), transform.position.z);
        } else if (rayHit.distance > 4.6f && rayHit.distance != 0) {
            transform.position = new Vector3(transform.position.x, transform.position.y - (rayHit.distance - 4.6f), transform.position.z);
        }
    }
}
