using UnityEngine;

public class RewardController : MonoBehaviour
{

    public GameObject loot;
    public int value = 69;
    public int totalLoot;
    public GameObject lootPickupSound;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameHandler.Instance.emittersGotten+=value;
            EmitterSoundController sound = lootPickupSound.GetComponent<EmitterSoundController>();
            sound.EmitPickupSound();

            Destroy(gameObject);
            Debug.Log($"Emmiter picked up! Total emmitters in inventory: {GameHandler.Instance.emittersGotten}");
        }

    }
}
