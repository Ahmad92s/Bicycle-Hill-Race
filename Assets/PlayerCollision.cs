using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerCollision : MonoBehaviour
{
    public static event Action<bool> finish;

    public PlayerManager playerManager;

    [SerializeField]
    internal bool onGround;

    bool finishFired = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ground")
        {
            onGround = true;
            DOTween.Kill(playerManager.visual);

            playerManager.playerMovement.rotateDir = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Finish")
        {
            if (!finishFired)
            {
                if (transform.position.z > playerManager.opponent.position.z)
                {
                    finish?.Invoke(true);
                }
                else
                {
                    finish?.Invoke(false);
                }

                finishFired = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        onGround = false;
    }
}
