using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    public Ground Instance { get; private set; }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetTrigger("trigger");
            StartCoroutine(Falling());
        }
    }
    IEnumerator Falling()
    {
        yield return new WaitForSeconds(2f);
        rb.isKinematic = false;
        
    }
    public void ResetObj()
    {
        rb.isKinematic = true;
        transform.localPosition = Vector3.zero;
    }
}
