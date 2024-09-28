using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudInterface : MonoBehaviour
{
    public int currentEnergy = 5;
    int currentAbilityIndex = 0;
    public GameObject[] frames;
    public CloudScript clousScript;

    [SerializeField] private Sprite cloudEnergyFull;
    [SerializeField] private Sprite cloudEnergyEmpty;
    [SerializeField] private Image[] cloudEmptyEnergy;

    private void Start()
    {
        UpdateInterface();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            useAbility();
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            currentAbilityIndex++;
            if (currentAbilityIndex == 5)
                currentAbilityIndex = 0;
            UpdateInterface();

        }
        else if (scroll < 0f)
        {
            currentAbilityIndex--;
            if (currentAbilityIndex == -1)
                currentAbilityIndex = 4;
            UpdateInterface();
        }
    }

    public void getEnergy()
    {
        if (currentEnergy <= 5)
            currentEnergy++;
        UpdateInterface();
    }

    void useAbility()
    {
        if (currentAbilityIndex == 0 && currentEnergy >= 1)
        {
            clousScript.Ability1();
            currentEnergy--;
            UpdateInterface();
        }
        if (currentAbilityIndex == 1 && currentEnergy >= 2)
        {
            currentEnergy -= 2;
            UpdateInterface();
            clousScript.Ability5();
        }
        if (currentAbilityIndex == 2 && currentEnergy >= 3)
        {
            currentEnergy -= 3;
            UpdateInterface();
            clousScript.Ability4();
        }
        if (currentAbilityIndex == 3 && currentEnergy >= 4)
        {
            currentEnergy -= 4;
            UpdateInterface();
            clousScript.Ability2();
        }
        if (currentAbilityIndex == 4 && currentEnergy >= 5)
        {
            currentEnergy -= 5;
            UpdateInterface();
            clousScript.Ability3();
        }
    }

    void UpdateInterface()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i == currentAbilityIndex)
                frames[i].GetComponent<RectTransform>().localScale = new Vector3(0.52f, 0.52f, 1);
            else
                frames[i].GetComponent<RectTransform>().localScale = new Vector3(0.5038357f, 0.4571707f, 1);
        }         

        for (int i = 0; i < 5; i++)
        {
            if (i < currentEnergy)
            {
                cloudEmptyEnergy[i].sprite = cloudEnergyFull;
            }
            else
            {
                cloudEmptyEnergy[i].sprite = cloudEnergyEmpty;
            }
        }
        Debug.Log($"current ability is {currentAbilityIndex}");
    }
}
