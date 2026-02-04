using UnityEngine;

public class AnimAutoDestroy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var animator = GetComponent<Animator>();
        if (animator == null) return;

        float length = animator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, length);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
