﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    float speed = 2;
    float rotspeed = 1;
    float rot = 0f;
    float gravity = 8;
    
    Vector3 moveDir = Vector3.zero;

    CharacterController controller;
    Animator anim;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        GetInput();
        

    }
    void Movement()
    {

        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rot = 270f;
                if(anim.GetBool("attacking") == true)
                {
                    return;
                }
                else if (anim.GetBool("attacking") == false)
                {
                    anim.SetBool("running", true);
                    anim.SetInteger("condition", 1);
                    moveDir = new Vector3(0, 0, 1);
                    moveDir *= speed;
                    moveDir = transform.TransformDirection(moveDir);
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                anim.SetBool("running", false);
                anim.SetInteger("condition", 0);

                moveDir = new Vector3(0, 0, 0);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rot = 90f;
                anim.SetBool("running", true);
                anim.SetInteger("condition", 1);
                moveDir = new Vector3(0, 0, 1);
                moveDir *= speed;
                moveDir = transform.TransformDirection(moveDir);
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                anim.SetBool("running", false);
                anim.SetInteger("condition", 0);

                moveDir = new Vector3(0, 0, 0);
            }
        }
        rot += Input.GetAxis("Horizontal") * rotspeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);


        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    
    }
    void GetInput()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (anim.GetBool("running") == true)
                {
                    anim.SetBool("running", false);
                    anim.SetInteger("condition", 0);
                    
                }
                if(anim.GetBool("running") == false)
                {

                    Attacking();
                }
            }
        }
    }
    void Attacking()
    {
        
        StartCoroutine(AttackRoutine());
       
    }


    IEnumerator AttackRoutine()
    {
        anim.SetBool("attacking", true);
        anim.SetInteger("condition", 2);
        yield return new WaitForSeconds (1);
        anim.SetInteger("condition", 0);
        anim.SetBool("attacking", false);
    }
}
