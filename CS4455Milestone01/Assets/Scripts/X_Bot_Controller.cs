using UnityEngine;
using System.Collections;

public class X_Bot_Controller : MonoBehaviour {


    public SmoothFollowCSharp smoothFollow;
    
    [SerializeField] public AudioClip[] footstepSounds;
    [SerializeField] public ParticleSystem footStepParticles;

    private AudioSource footStepAudio;
    private Animator animator;
    private AnimatorStateInfo currBaseState;
    private CharacterController characterController;
    private Transform lookPos;
    private Transform X_Bot;

    private float currentSpeed;
    private bool footTouchGround;
    private float groundDistance;

    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int forwardState = Animator.StringToHash("Base Layer.Forward");
    static int forwardJumpState = Animator.StringToHash("Base Layer.ForwardJump");
    static int forwardFallingState = Animator.StringToHash("Base Layer.ForwardFalling");
    static int backState = Animator.StringToHash("Base Layer.Backward");
    static int idleJumpState = Animator.StringToHash("Base Layer.IdleJump");

    private float lastFootLeft;
    private float currFootLeft;
    private float lastFootRight;
    private float currFootRight;

    // Use this for initialization
    void Start () {
        footStepAudio = gameObject.GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        lookPos = transform.Find("LookPos");
        X_Bot = transform.Find("X_Bot");

        DisableRagdoll();
        currentSpeed = 0;
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 10; ++i)
        {
            if (Input.GetKey("" + i))
            {
                currentSpeed = i;
                print(currentSpeed);
            }
        }
        
        animator.SetFloat("forward", (currentSpeed == 0) ? Input.GetAxis("Vertical") : currentSpeed / 10f * Input.GetAxis("Vertical"));
        //animator.SetFloat("forward", Input.GetAxis("Vertical"));
        animator.SetFloat("left_right", Input.GetAxis("Horizontal"));
        animator.SetFloat("soft_left_right", Input.GetAxis("SoftHorizontal"));
        currBaseState = animator.GetCurrentAnimatorStateInfo(0);

        if (currBaseState.fullPathHash == idleState)
        {
            if (Input.GetKeyDown("space"))
            {
                animator.SetBool("isJumping", true);
            }
        }
        else if (currBaseState.fullPathHash == idleJumpState)
        {
            if (!animator.IsInTransition(0))
            {
                animator.SetBool("isJumping", false);
            }
        }

        if (currBaseState.fullPathHash == forwardState)
        {
            if (Input.GetKeyDown("space"))
            {
                animator.SetBool("isJumping", true);
            }
        }
        else if (currBaseState.fullPathHash == forwardJumpState)
        {
            if (!animator.IsInTransition(0))
            {
                animator.SetBool("isJumping", false);
            }
        }


        if (Input.GetKeyDown("p"))
        {
            EnableRagdoll();
        }
        if (Input.GetKeyDown("o"))
        {
            DisableRagdoll();
        }

        FootStep();
    }


    void EnableRagdoll()
    {
        if (animator.enabled)
        {
            animator.SetBool("isRagDoll", true);
            foreach (Rigidbody rb in X_Bot.GetComponentsInChildren<Rigidbody>())
            {
                rb.isKinematic = false;
                rb.detectCollisions = true;
            }
            characterController.enabled = false;
            animator.enabled = false;

            smoothFollow.target = X_Bot.Find("mixamorig:Hips");
        }
    }

    void DisableRagdoll()
    {
        if (!animator.enabled)
        {
            // Set camera and Player_Container to ragdoll;
            transform.position = X_Bot.Find("mixamorig:Hips").position;
            smoothFollow.target = transform;

            foreach (Rigidbody rb in X_Bot.GetComponentsInChildren<Rigidbody>())
            {
                rb.isKinematic = true;
                rb.detectCollisions = false;
            }
            characterController.enabled = true;
            animator.enabled = true;
        }
        animator.SetBool("isRagDoll", false);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.3f) || characterController.isGrounded;
    }

    public void FootStep()
    {
        currFootLeft = animator.GetFloat("FootLeft");   // negative to positive
        currFootRight = animator.GetFloat("FootRight"); // positive to negative


        if (IsGrounded() && (currFootLeft > 0 && lastFootLeft <= 0 || currFootRight < 0 && lastFootRight >= 0))
        {
            footStepAudio.PlayOneShot(footstepSounds[Random.Range(0, footstepSounds.Length)]);

            if (footStepParticles != null)
            {
                footStepParticles.Play();
            }
        }


        lastFootLeft = currFootLeft;
        lastFootRight = currFootRight;
    }
}



