using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Falliant : MonoBehaviour
{
    [SerializeField] private TMP_Text faliantCounter; 

    static private List<int> abilitiesID;
    private int rand;

    private static int faliantsCarriedAmount;
    private static int abilitiesGotten;
    [SerializeField] private GameObject showAbilityDash;
    [SerializeField] private GameObject showAbilityAttack;
    [SerializeField] private GameObject showAbilityShield;
    [SerializeField] private GameObject showAbilityHP;

    [SerializeField] private Transform player;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private Transform goal;
    [SerializeField] private float followSpeed;
    public bool taken = false;
    public bool carried = false;
    private Vector3 targetFollow;

    [SerializeField] private Sprite mageEnergyFull;
    [SerializeField] private TMP_Text fullTXT;
    [SerializeField] private Image[] mageEmptyEnergy;

    private Vector3 startPosition;

    static private bool alreadyCarrying = false;

    private void Start()
    {
        abilitiesID = new List<int> { 0, 1, 2, 3 };
        faliantsCarriedAmount = 0;
        abilitiesGotten = 0;
        alreadyCarrying = false;
        startPosition = this.transform.position;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Mage" && carried == false && alreadyCarrying == false)
        {
            taken = true;
            alreadyCarrying = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goal")
        {
            alreadyCarrying = false;
            taken = false;
            carried = true;
            faliantsCarriedAmount++;
            faliantCounter.text = $"{faliantsCarriedAmount}";

            if (faliantsCarriedAmount % 2 != 0)
            {
                mageEmptyEnergy[abilitiesGotten].sprite = mageEnergyFull;
                abilitiesGotten++;

                if (abilitiesGotten == 4)
                    fullTXT.text = "Full";

                rand = Random.Range(0, abilitiesID.Count);

                if (abilitiesID[rand] == 0)
                {
                    playerManager.gotAbilityHeal = true;
                    showAbilityHP.SetActive(true);
                }
                if (abilitiesID[rand] == 1)
                {
                    playerManager.gotAbilityDash = true;
                    showAbilityDash.SetActive(true);
                }
                if (abilitiesID[rand] == 2)
                {
                    playerManager.gotAbilityShield = true;
                    showAbilityShield.SetActive(true);
                }
                if (abilitiesID[rand] == 3)
                {
                    playerManager.gotAbilityAttack = true;
                    showAbilityAttack.SetActive(true);
                }

                abilitiesID.RemoveAt(rand);
            }

        }
    }

    private void FixedUpdate()
    {
        if (taken)
        {
            StartCoroutine(Follow());
        }

        if (carried)
        {
            StartCoroutine(PutTheBook());
        }
    }

    IEnumerator Follow()
    {
        while (Vector3.Distance(transform.position, player.position) > 2f && carried == false && taken == true)
        {
            targetFollow = new Vector3(player.position.x, player.position.y + 1, player.position.z); //чтобы фалиант парил
            transform.position = Vector3.Lerp(transform.position, targetFollow, followSpeed * Time.deltaTime);
            yield return null; 
        }
        taken = false;
        Invoke("QuickPause", 0.25f);
    }

    IEnumerator PutTheBook()
    {
        while (Vector3.Distance(transform.position, goal.position) > 2f)
        {
            transform.position = Vector3.Lerp(transform.position, goal.position, followSpeed * Time.deltaTime);
            yield return null;
        }
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        this.enabled = false;
    }

    void QuickPause()
    {
        taken = true;
    }

    public void ReturnToTheStart()
    {
        if (taken)
        {
            this.transform.position = startPosition;
            taken = false;
            Debug.Log(23);
        }
    }
}
