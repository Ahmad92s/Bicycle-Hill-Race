using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerMovement : MonoBehaviour
{

    public PlayerManager playerManager;
    public float moveSpeed, rotateSpeed, downSpeed, fastSpeed;
    private float normalSpeed;

    private bool isFlipping;
    private float flipTime;

    internal int rotateDir;

    private float initRotation;

    int totalRotations = -1, rotationCheck = 1;

    public GameObject speedTextFX, speedParticleFX;
    public Transform speedTextFXPosition;

    Camera cam;
    [SerializeField]
    float fovMin, fovMax, fovZoomSpeed;

    bool gameEndedWithHeadache;

    private void Start()
    {
        initRotation = playerManager.visual.rotation.eulerAngles.x;
        normalSpeed = moveSpeed;

        cam = Camera.main;

        HeadCollider.hitHead += stopMovement;
    }
    private void OnDisable()
    {
        HeadCollider.hitHead -= stopMovement;
    }

    private void Update()
    {

        if (playerManager.playerCollision.onGround)
        {
            //playerManager.visual.LookAt(playerManager.rb.velocity + transform.position);
            playerManager.visual.DOLookAt(playerManager.rb.velocity + transform.position, 0.3f);
            zoomIn();
        }
        else
        {
            if (playerManager.playerInput.buildSpeed)
            {
                zoomOut();
                if (!isFlipping)
                {
                    rotateDir = -1;
                    doLean(rotateDir, rotateSpeed);
                }
            }
            else
            {
                zoomIn();

                if (rotateDir == 0)
                {
                    //playerManager.visual.LookAt(playerManager.rb.velocity + transform.position);
                    playerManager.visual.DOLookAt(playerManager.rb.velocity + transform.position, 0.3f);
                }
                else
                {
                    doLean(rotateDir, rotateSpeed * 0.08f);
                }
            }
        }
    }
    void FixedUpdate()
    {
        if(playerManager.rb.velocity.magnitude < moveSpeed && !gameEndedWithHeadache)
        {
            playerManager.rb.AddForce(Vector3.forward * (moveSpeed - playerManager.rb.velocity.z), ForceMode.VelocityChange);
        }
        if (gameEndedWithHeadache)
        {
            playerManager.rb.isKinematic = true;
        }

        rotate360Check();
    }

    void doLean(int dir, float speed)
    {
        DOTween.Kill(playerManager.visual);
        playerManager.visual.Rotate(dir * Vector3.right * speed * Time.deltaTime);
    }

    void rotate360Check()
    {
        float currRotation = playerManager.visual.localEulerAngles.x;
        //Debug.Log(currRotation);

        if (currRotation <= 10f && rotationCheck == 1)
        {
            totalRotations += 1;
            rotationCheck = -1;

            moveSpeed *= 1.02f;

            if (totalRotations % 2 == 0 && totalRotations != 0)
            {
                Instantiate(speedTextFX, playerManager.visual);
                Instantiate(speedParticleFX, playerManager.visual);
            }
        }
        else if(currRotation >= 350 && rotationCheck == -1)
        {
            rotationCheck = 1;
        }

        //Debug.Log(totalRotations);
    }

    void zoomOut()
    {
        if(cam.fieldOfView < fovMax)
        {
            cam.fieldOfView += fovZoomSpeed * Time.deltaTime;
        }
    }

    void zoomIn()
    {
        if (cam.fieldOfView > fovMin)
        {
            cam.fieldOfView -= fovZoomSpeed * Time.deltaTime;
        }
    }

    void stopMovement()
    {
        gameEndedWithHeadache = true;
    }
}
