using UnityEngine;
using TMPro;

public class UIInteractWith : MonoBehaviour
{
    public GameObject promptUI;
    public GameObject interactible;
    public GameObject door1;
    public GameObject door1a;
    private bool playerInRange = false;
    public bool isDoorOpener = false;


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
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
        if (interactible == null && !isDoorOpener)
        {
            Debug.Log("Gonna destroy the text...");
            Destroy(promptUI);
        }
    }

    public void DestroyText()
    {
        if (promptUI != null && !isDoorOpener)
        {
            Destroy(promptUI);
            promptUI = null;
        }
    }

    void Interact()
    {
        Debug.Log("Player interacted!");
        DestroyText();

        DoorOpener doorscript1 = door1.GetComponent<DoorOpener>();
        doorscript1.isUnlocked = true;
        doorscript1.ToggleDoor();

        DoorOpener doorscript1a = door1a.GetComponent<DoorOpener>();
        doorscript1a.isUnlocked = true;
        doorscript1a.ToggleDoor();
    }
}
