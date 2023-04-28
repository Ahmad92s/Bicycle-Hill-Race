using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeadCollider : MonoBehaviour
{
    public static event Action hitHead;
    public PlayerManager playerManager;

    public GameObject bombFX;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
        {
            HeadCollisionHandler();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            HeadCollisionHandler();
        }
    }

    void HeadCollisionHandler()
    {
        Debug.Log("YOS");

        AudioManager.instance.PlaySound("boom");

        hitHead?.Invoke();

        playerManager.visual.gameObject.SetActive(false);
        Instantiate(bombFX, this.transform.position, transform.rotation);
    }
}
