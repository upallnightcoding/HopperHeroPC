using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCntrl : MonoBehaviour
{
    [SerializeField] private float runSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float jumpSpeed;

    //private CharacterController controller;

    private Rigidbody rb;

    private Animator animator;

    private HeroState heroState = HeroState.IDLE;

     float horiInput ;
     float vertInput ;
     bool trigger = false;
     bool jump = false;

    // Start is called before the first frame updte
    void Start()
    {
        //controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        //horiInput = Input.GetAxis("Horizontal");
        //vertInput = Input.GetAxis("Vertical");
        trigger = trigger ? trigger : Input.GetKeyDown(KeyCode.Space);
        //jump = jump ? jump : (vertInput != 0.0f);
    }

    void FixedUpdate() {
        horiInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");
        //trigger = Input.GetKeyDown(KeyCode.Space);
        jump = vertInput != 0.0f;

        switch(heroState) {
            case HeroState.IDLE:
                heroState = MovementIdle(trigger);
                break;
            case HeroState.RUN:
                heroState = MovementRun(horiInput, vertInput, trigger, jump);
                break;
            case HeroState.JUMPING:
                MoveHero(horiInput, vertInput);
                break;
            case HeroState.JUMP_END:
                heroState = HeroState.RUN;
                animator.SetInteger("state", (int) heroState);
                break;
        }

        trigger = false;
        jump = false;
    }

    private HeroState MovementIdle(bool trigger) {
        HeroState state = HeroState.IDLE;

        if (trigger) {
            state = HeroState.RUN;
            animator.SetInteger("state", (int) state);
        }

        return(state);
    }

    private HeroState MovementRun(float horiInput, float vertInput, bool trigger, bool jumpRequest) {

        HeroState state = HeroState.RUN;

        if (jumpRequest) {
            state = HeroState.JUMPING;
            animator.SetInteger("state", (int) state);
            JumpHero(); 
        } 
        
        if (trigger) {
            state = HeroState.IDLE;
            animator.SetInteger("state", (int) state);
        } else { 
            RotateHero(horiInput);
            MoveHero(horiInput, vertInput);
        }

        return(state);
    }

    private void MovementJumpEnd() {
        heroState = HeroState.JUMP_END;
    }

    private void JumpHero() {
        //Vector3 jumpForces = Vector3.up * jumpSpeed;
        Vector3 jumpForces = Vector3.up * 100.0f;
        rb.AddForce(jumpForces, ForceMode.VelocityChange);
    }

    private void RotateHero(float horiInput) {
        transform.Rotate(0.0f, horiInput * rotateSpeed * Time.deltaTime, 0.0f);
    }

    private void MoveHero(float horiInput, float vertInput) {
        Vector3 currentVelocity = rb.velocity;
        Vector3 targetVelocity = new Vector3(horiInput, 0.0f, 1.0f) * runSpeed;
        targetVelocity = transform.TransformDirection(targetVelocity);
        Vector3 velocityChange = (targetVelocity - currentVelocity);
        Vector3.ClampMagnitude(velocityChange, 1.0f);
        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

/*
    void Update2()
    {
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
*/

}

public enum HeroState {
    IDLE = 0,
    RUN = 1,
    JUMPING = 2,
    JUMP_END = 3
}
