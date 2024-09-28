using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float pickupCooldown;
    [SerializeField] int pickupPower;
    SpriteRenderer renderer;

    bool deactivated = false;
    float sleepTime = 0;
    void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.tag == "Cloud" && !deactivated) {
            collision.gameObject.GetComponent<CloudScript>().gotPickup(pickupPower);
            deactivated = true;
            sleepTime = 0;
        }
    }

    void Start() {
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (deactivated) {
            renderer.enabled = false;
        } else renderer.enabled = true;
        sleepTime += Time.deltaTime;
        if (sleepTime >= pickupCooldown) deactivated = false;
    }
}
