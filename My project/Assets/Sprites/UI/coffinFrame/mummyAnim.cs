using UnityEngine;

public class mummyAnim : MonoBehaviour
{
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAnim(bool toggle)
    {
        animator.SetBool("play", toggle);
    }
}
