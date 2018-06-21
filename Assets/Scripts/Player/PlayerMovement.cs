using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //private InputController m_Input;

    public float walkSpeed = 6.0f;
    public float runSpeed = 10.0f;

    /*If true, the diagonal speed is limited to normal speed and 
     * will not be multiplied by left or right strafing combined 
     * with forward or backward*/
    public bool limitDiagonalSpeed = true;

    /*If true, Player does not need to hold down the key,
     * only needs to toggle between walk and run speed.*/
    public bool toggleRun = false;

    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    /* Units the player will be able to fall before taking fall damage.
     * To Disable, in insapector give the value infinity*/
    public float fallingLimit = 10.0f;

    /*If the player ends up on a sloper which is at least the sloper limit
     * as set on the character controller*/
    public bool sliderWhenOverSlopeLimit = false;

    public bool slideOnTaggedObjects = false;
    public float slideSpeed = 12.0f;

    public bool airControl = false;

    /*Small amounts of this results in bumping when walking down slopes
     * but large amounts results in falling too fast*/
    public float antiBumpFactor = 0.75f;

    //Player must be grounded for at least the amount of physics frame shown
    public int antiBunnyHopFactor = 1;


    private Vector3 m_MoveDirection = Vector3.zero;
    private bool m_Grounded = false;
    private CharacterController m_Controller;
    private Transform m_MyTransform;
    private float m_MovementSpeed;
    private RaycastHit m_Hit;
    private float m_FallStartLevel;
    private bool m_Falling;
    private float m_SlideLimit;
    private float m_RayDistance;
    private Vector3 m_ContactPoint;
    private bool m_PlayerControl = false;
    private int m_JumpTimer;

    // Use this for initialization
    void Start ()
    {
        m_Controller = GetComponent<CharacterController>();       
        m_MyTransform = transform;
        m_MovementSpeed = walkSpeed;
        m_RayDistance = m_Controller.height * 0.5f + m_Controller.radius;
        m_SlideLimit = m_Controller.slopeLimit - 0.1f;
        m_JumpTimer = antiBunnyHopFactor;

    }

	// FixedUpdate is called once per Physics frame
	void FixedUpdate ()
    {
        /*If Both horizontal and vertical axis are being used simultaneously,
         * limit speed(if set), so the total does not exceed normal speed*/
        //float inputModifyFactor = (m_Input.GetAxis().x != 0.0f && m_Input.GetAxis().y != 0.0f && limitDiagonalSpeed) ? 0.7071f : 1.0f;

        if (m_Grounded)
        {
            bool sliding = false;
            /*See if surface immediately below should be slid down.
                * we use this normally rather than a ControllerColliderHit point,
                * because that interferes with step climbing amongst other annoyances*/
            if (Physics.Raycast(m_MyTransform.position, -Vector3.up, out m_Hit, m_RayDistance))
            {
                if (Vector3.Angle(m_Hit.normal, Vector3.up) > m_SlideLimit)
                {
                    sliding = true;
                }
            }
            /* However, just raycasting straight down from the center can fail when on steep slopes
                * So if the above raycast didn't catch anything, ray cast down from the stored ControllerColliderHit point instead*/
            else
            {
                Physics.Raycast(m_ContactPoint + Vector3.up, -Vector3.up, out m_Hit);
                if (Vector3.Angle(m_Hit.normal, Vector3.up) > m_SlideLimit)
                {
                    sliding = true;
                }
            }
            /*If we were falling, and we fell a vertical distance greater than the threshold
                * run a falling damage routine*/
            if (m_Falling)
            {
                m_Falling = false;
                if (m_MyTransform.position.y < m_FallStartLevel - fallingLimit)
                {
                    //FallingDamageAlert(fallStartLevel - myTransform.position.y);
                }
            }

            /* if running isn't toggled, then use the appropriate speed depending on whether
                * the run button is down */
            if (!toggleRun)
            {
                m_MovementSpeed = InputController.IsShiftDown() ? runSpeed : walkSpeed;
            }
            /* ifsliding(and that its allowed), or if we're on an object tagged "Slide",
                * get a vector pointing down the slope we're on*/
            if ((sliding && sliderWhenOverSlopeLimit) || (slideOnTaggedObjects && m_Hit.collider.tag == "Slide"))
            {
                Vector3 hitNormal = m_Hit.normal;
                m_MoveDirection = new Vector3(hitNormal.x, -hitNormal.y, hitNormal.z);
                Vector3.OrthoNormalize(ref hitNormal, ref m_MoveDirection);
                m_MoveDirection *= slideSpeed;
                m_PlayerControl = false;
            }
            /*Otherwise recalculate moveDirection directly from the axes,
                * adding a bit of -y to avoid bumping down inclines.*/
            else
            {
                m_MoveDirection = new Vector3(InputController.GetAxis().x, -antiBumpFactor, InputController.GetAxis().y);
                m_MoveDirection = m_MyTransform.TransformDirection(m_MoveDirection) * m_MovementSpeed;
                m_PlayerControl = true;
            }

            /*Jump! but only if the jump button has been released and
                * player has been grounded for a given number of frames*/
            if (!Input.GetButton("Jump"))
            {
                m_JumpTimer++;
            }
            else if (m_JumpTimer >= antiBunnyHopFactor)
            {
                m_MoveDirection.y = jumpSpeed;
                m_JumpTimer = 0;
            }
        }
        else
        {
            /*If we stepped over a cliff or something else,
                * set the height at which we started falling */
            if (!m_Falling)
            {
                m_Falling = true;
                m_FallStartLevel = m_MyTransform.position.y;
            }

            /*if air control is allowed, check movement 
                * but down touch the y component*/
            if (airControl && m_PlayerControl)
            {
                //m_MoveDirection.x = m_Input.GetAxis().x * m_MovementSpeed * inputModifyFactor;
                //m_MoveDirection.z = m_Input.GetAxis().y * m_MovementSpeed * inputModifyFactor;
                m_MoveDirection = m_MyTransform.TransformDirection(m_MoveDirection);
            }
        }
        //Apply Gravity
        m_MoveDirection.y -= gravity * Time.deltaTime;

        /*Move the controller, and set grounded true or false
         * depending on whether we're standing on something*/
        m_Grounded = (m_Controller.Move(m_MoveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;
	}



void Update()
    {
        /*If the run button is set to toggle, then switch between walk/run speed. (we use Update for this
         * FixedUpdate is a poor place to use GetButtonDown, since it doesn't necessarily run every frame and can miss the event */


        if (toggleRun && m_Grounded && InputController.IsShiftDown())
        {
            m_MovementSpeed = (m_MovementSpeed == walkSpeed ? runSpeed : walkSpeed);
        }

    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        m_ContactPoint = hit.point;
    }
}
