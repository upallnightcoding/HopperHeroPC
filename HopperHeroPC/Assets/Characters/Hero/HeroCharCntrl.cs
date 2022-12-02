using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCharCntrl : MonoBehaviour
{
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float flySpeed;

    private CharacterController controller;
    private Animator animator;

    private HeroCharCntrlState heroState = HeroCharCntrlState.IDLE;

    private float heroHeight = 0.0f;

    private Vector3 moveVector;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horiInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        bool startStopToggle = Input.GetKeyDown(KeyCode.Space);
        bool jumpRequest = Input.GetKeyDown(KeyCode.W);

        AdjustForGravity();

        switch(heroState) {
            case HeroCharCntrlState.IDLE:
                HeroIdle(startStopToggle);
                break;
            case HeroCharCntrlState.RUN:
                HeroRun(horiInput, vertInput, startStopToggle, jumpRequest);
                break;
            case HeroCharCntrlState.JUMP_START:
                StartFalling();
                break;
            case HeroCharCntrlState.FALLING:
                HeroFalling(horiInput);
                break;
            case HeroCharCntrlState.JUMP_END:
                EndJump(horiInput);
                break;
        }
    }

    private void AdjustForGravity()
    {
        heroHeight += Physics.gravity.y * Time.deltaTime;
    }

    private void HeroIdle(bool startStopToggle) 
    {
        if (startStopToggle) {
            ChangeHeroState(HeroCharCntrlState.RUN);
        } 
    }

    private void HeroRun(float horiInput, float vertInput, bool startStopToggle, bool jumpRequest) 
    {
        if (startStopToggle) {
            ChangeHeroState(HeroCharCntrlState.IDLE);
        } else {
            if (jumpRequest) {
                StartJumpAnimation();
            } else {
                MoveHeroForward(horiInput, runSpeed);
            }
        }
    }

    private void HeroFalling(float horiInput) 
    {
        MoveHeroForward(horiInput, flySpeed);

        if (heroHeight < 0.0f) {
            heroHeight = 0.0f;
            ChangeHeroState(HeroCharCntrlState.JUMP_END);    
        }
    }

    private void EndJump(float horiInput) 
    {
        MoveHeroForward(horiInput, runSpeed);
        ChangeHeroState(HeroCharCntrlState.RUN);    
    }

    private void StartJumpAnimation() 
    {
        ChangeHeroState(HeroCharCntrlState.JUMP_START);
        heroHeight = jumpHeight;
    }

    private void StartFalling() 
    {
        ChangeHeroState(HeroCharCntrlState.FALLING);
    }

    private void MoveHeroForward(float horiInput, float speed) 
    {
        moveVector.x = horiInput;
        moveVector.y = heroHeight;
        moveVector.z = speed;

        controller.Move(moveVector * Time.deltaTime);
    }

    private void ChangeHeroState(HeroCharCntrlState newState) 
    {
        heroState = newState;
        animator.SetInteger("state", (int) newState);
    }
}

public enum HeroCharCntrlState 
{
    IDLE = 0,
    RUN = 1,
    JUMP_START = 2,
    FALLING = 3,
    JUMP_END = 4
}
