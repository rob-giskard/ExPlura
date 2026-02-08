using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.Rendering.Universal;

public class GameHandler : MonoBehaviour
{
    public GameObject player; 
    Vector2 defaultPlayerPos;
    public static GameHandler Instance { get; private set; } // Singleton reference
    
    public int emittersGotten = 0; 

    public int EmittersGotteneFlightgr
    {
        get => emittersGotten;
        private set
        {
            emittersGotten = value;
        }
    }
    public TMP_Text pillCount;

    // Camera list
    public Camera uiCamera;
    public Camera mainCamera;
    
    // UIs
    public GameObject uiCanvas;
    public GameObject hudCanvas;

    // Global light
    public Light2D globalLight;

    // Beacons
    public Light2D beacon1;
    public Light2D beacon2;
    public Light2D beacon3;
    public Light2D beacon4;

    // Doors
    // delete later, doors now handle themselves
    public GameObject door1;
    public GameObject door1a;
    public GameObject door2;
    public GameObject door3;
    public GameObject door3a;
    public GameObject door3b;
    public GameObject door4;

    public GameObject spawnPoint;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Debug.Log("*Game handler running.*");

        player.SetActive(false);
        Debug.Log("*player dectivated.*");
        
        // Start in menu mode
        SwitchCamera(uiCamera);
        Debug.Log("*Camera switched to menu.*");

        uiCanvas.SetActive(true);
        Debug.Log("*menu canvas activated.*");

        hudCanvas.SetActive(true);
        Debug.Log("*HUD canvas activated.*");
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.activeSelf) && Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnMenu();
        }
        if (uiCamera.enabled == true)
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                StartGame();
            }
        }
        UpdateUI();
        // admin codes for doors
        //TODO make a switch or use some input handler
        // make toggle to close them
        if ((player.activeSelf) && Input.GetKeyDown(KeyCode.U))
        {
            DoorOpener doorscript1 = door1.GetComponent<DoorOpener>();
            doorscript1.isUnlocked = true;
            doorscript1.ToggleDoor();

            DoorOpener doorscript1a = door1a.GetComponent<DoorOpener>();
            doorscript1a.isUnlocked = true;
            doorscript1a.ToggleDoor();
        }
        if ((player.activeSelf) && Input.GetKeyDown(KeyCode.I))
        {
            DoorOpener doorscript2 = door2.GetComponent<DoorOpener>();
            doorscript2.isUnlocked = true;
            doorscript2.ToggleDoor();
        }
        if ((player.activeSelf) && Input.GetKeyDown(KeyCode.O))
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
        if ((player.activeSelf) && Input.GetKeyDown(KeyCode.P))
        {
            DoorOpener doorscript4 = door4.GetComponent<DoorOpener>();
            doorscript4.isUnlocked = true;
            doorscript4.ToggleDoor();
        }
        // slowly light up rooms after collectible milestones
        switch (emittersGotten)
        {
            case (1):
                LightGradualUp lightScript1 = beacon1.GetComponent<LightGradualUp>();
                lightScript1.isHeatingUp = true;
                break;
            case (2):
                LightGradualUp lightScript2 = beacon2.GetComponent<LightGradualUp>();
                lightScript2.isHeatingUp = true;
                break;
            case (3):
                LightGradualUp lightScript3 = beacon3.GetComponent<LightGradualUp>();
                lightScript3.isHeatingUp = true;
                break;
            case (4):
                DoorOpener doorscript1 = door1.GetComponent<DoorOpener>();
                doorscript1.isUnlocked = true;
                break;
            case (5):
                LightGradualUp lightscript4 = beacon4.GetComponent<LightGradualUp>();
                lightscript4.isHeatingUp = true;
                // trigger text with R + T tutorial
                break;
            default:
                break;
        }
        
    }

    public void StartGame()
    {
        player.SetActive(true);
        Debug.Log("*player activated.*");
        defaultPlayerPos = player.transform.position;
        SwitchCamera(mainCamera);
        Debug.Log("*main camera enabled.*");
        uiCanvas.SetActive(false);
        hudCanvas.SetActive(true);
        // this is pain 
        if (uiCamera == null) Debug.LogError("uiCamera is NULL!");
        if (uiCanvas == null) Debug.LogError("uiCanvas is NULL!");
        if (player == null) Debug.LogError("Player is NULL!");

        StartCoroutine(DelayedLog());
    }

    private IEnumerator DelayedLog()
    {
        yield return new WaitForEndOfFrame(); 
        Debug.Log("-----> Delay log logged <-----");
    }

    public void ExitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR // Preprocessor directive - compiles only if running in Unity Editor
        UnityEditor.EditorApplication.isPlaying = false; 
        #endif
    }

    public void ReturnMenu()
    {
        player.SetActive(false);

        SwitchCamera(uiCamera);
        uiCanvas.SetActive(true);

        ResetGame();    
    }

    private void ResetGame()
    {
        player.transform.position = spawnPoint.transform.position;
        PlayerController playerHPscript = player.GetComponent<PlayerController>();
        playerHPscript.HP = 3;
        
    }

    private void SwitchCamera(Camera activeCamera) // Neat switcher
    {
        mainCamera.enabled = (activeCamera == mainCamera);
        uiCamera.enabled = (activeCamera == uiCamera);
    }

    private void UpdateUI()
    {
        pillCount.text = emittersGotten.ToString();
    }
}
