using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCharCntrl : MonoBehaviour
{
    [SerializeField] private float runSpeed;
    [SerializeField] private float justHeight;

    private CharacterController controller;
    private Animator animator;

    private HeroCharCntrlState heroState = HeroCharCntrlState.IDLE;

    private float gravity = 0.0f;

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
        }
    }

    private void AdjustForGravity()
    {
        gravity += Physics.gravity.y;
    }

    private void HeroIdle(bool startStopToggle) {
        if (startStopToggle) {
            ChangeHeroState(HeroCharCntrlState.RUN);
        } 
    }

    private void HeroRun(float horiInput, float vertInput, bool startStopToggle, bool jumpRequest) {
        if (startStopToggle) {
            ChangeHeroState(HeroCharCntrlState.IDLE);
        } else {
            if (jumpRequest) {
                StartJumpAnimation();
            } else {
                MoveHeroForward(horiInput);
            }
        }
    }

    private void StartJumpAnimation() {
        ChangeHeroState(HeroCharCntrlState.JUMP_START);
        gravity = justHeight;
    }

    private void MoveHeroForward(float horiInput) {
        //Vector3 moveVector = new Vector3(horiInput, 0.0f, 1.0f) * runSpeed;

        moveVector.x = horiInput;
        moveVector.y = 0.0f;
        moveVector.z = 1.0f;

        controller.Move(moveVector * runSpeed * Time.deltaTime);
    }

    private void ChangeHeroState(HeroCharCntrlState newState) {
        heroState = newState;
        animator.SetInteger("state", (int) newState);
    }
}

public enum HeroCharCntrlState {
    IDLE = 0,
    RUN = 1,
    JUMP_START = 2,
    FALLING = 3,
    JUMP_END = 4
}
