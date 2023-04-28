using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpponentAI : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    float moveSpeed;
    float minSpeed, maxSpeed;

    [SerializeField]
    Transform visual;

    [SerializeField]
    Transform player;

    bool isFlipping = false, onGround = false;

    [SerializeField]
    float checkGroundRayLength;


    private void Start()
    {
        minSpeed = moveSpeed * .9f;
        maxSpeed = moveSpeed * 1.1f;
    }
    private void Update()
    {
        //fluctuateSpeed();

        if (onGround)
        {
            //visual.transform.LookAt(rb.velocity + transform.position);
            visual.transform.DOLookAt(rb.velocity + transform.position, 0.3f);
        }
        else
        {
            if (!isFlipping)
            {
                RaycastHit hit;
                if(Physics.Raycast(this.transform.localPosition, Vector3.down, out hit, checkGroundRayLength))
                {
                    if(hit.transform.tag == "Ground")
                    {
                        if(Vector3.Angle(transform.forward, Vector3.forward) < 90)
                        {
                            doLean(-1, 600);
                        }
                        else
                        {
                            doLean(-1, 400);
                        }
                        //doFlip(1);
                    }
                }
                else
                {
                    doLean(-1, 400);
                    //doFlip(1);
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (rb.velocity.magnitude < moveSpeed)
        {
            rb.AddForce(Vector3.forward * (moveSpeed - rb.velocity.z), ForceMode.VelocityChange);
        }

        fluctuateSpeed();
    }




    void doLean(int dir, float speed)
    {
        DOTween.Kill(visual);
        visual.Rotate(dir * Vector3.right * speed * Time.deltaTime);
    }
    void doFlip(float time)
    {
        isFlipping = true;

        Debug.Log("doing flip");
        visual.DOLocalRotate(new Vector3(-360, 0, 0), time, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);

        Invoke("setflipFlagOff", time);
    }
    void setflipFlagOff()
    {
        isFlipping = false;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ground")
        {
            onGround = true;
            DOTween.Kill(visual);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        onGround = false;
    }


    void fluctuateSpeed()
    {
        if(transform.position.z < player.position.z)
        {
            if(moveSpeed < maxSpeed)
            {
                moveSpeed += (maxSpeed - minSpeed) * 1.03f;
                //moveSpeed *= 1.02f * Time.deltaTime;
            }
        }
        else
        {
            if (moveSpeed > minSpeed)
            {
                moveSpeed -= (maxSpeed - minSpeed) * 0.2f;
                //moveSpeed *= 0.95f * Time.deltaTime;
            }
        }
    }
}

