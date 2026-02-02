using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class DoorOperatorUI : MonoBehaviour
{
    public GameObject promptUI;
    public GameObject interactible;
    public GameObject door1;
    public GameObject door1a;
    public GameObject door2;
    public GameObject door3;
    public GameObject door3b;
    public GameObject door3a;
    public GameObject door4;
    private bool playerInRange = false;

    public LightOnUI bulb1;
    public bool isOnD1 = false;

    void Start()
    {
        if (promptUI != null)
            promptUI.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            promptUI.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            promptUI.SetActive(false);
        }
    }
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.T))
        {
            Interact();
        }
        if (interactible == null)
        {
            Debug.Log("Gonna destroy the text...");
            Destroy(promptUI);
        }
    }
    void Interact()
    {
        Debug.Log("Player interacted with a door operator!");

        if (Input.GetKeyDown(KeyCode.E))
        {
            DoorSwitch("E");            
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            DoorOpener doorscript3 = door3.GetComponent<DoorOpener>();
            doorscript3.isUnlocked = true;
            doorscript3.ToggleDoor();

            DoorOpener doorscript3a = door3a.GetComponent<DoorOpener>();
            doorscript3a.isUnlocked = true;
            doorscript3a.ToggleDoor();

            DoorOpener doorscript3b = door3b.GetComponent<DoorOpener>();
            doorscript3b.isUnlocked = true;
            doorscript3b.ToggleDoor();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            DoorOpener doorscript2 = door2.GetComponent<DoorOpener>();
            doorscript2.isUnlocked = true;
            doorscript2.ToggleDoor();

            DoorOpener doorscript4 = door4.GetComponent<DoorOpener>();
            doorscript4.isUnlocked = true;
            doorscript4.ToggleDoor();
        }
    }
    void DoorSwitch(string key)
    {
        switch (key)
        {

            case "E":

                isOnD1 = !isOnD1;
                bulb1.SetState(isOnD1);
                DoorOpener doorscript1 = door1.GetComponent<DoorOpener>();
                doorscript1.isUnlocked = true;
                doorscript1.ToggleDoor();

                DoorOpener doorscript1a = door1a.GetComponent<DoorOpener>();
                doorscript1a.isUnlocked = true;
                doorscript1a.ToggleDoor();
                break;

        }
        

    }
}
