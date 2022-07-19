using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private Animator anim;
   

     private void OnEnable()
    {
        anim.SetTrigger("Spawn");
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.GetCoin();
            anim.SetTrigger("Collected");
            Destroy(gameObject, 0.1f);
        }
    }
}