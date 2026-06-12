using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float xinp,yinp;
    public float rotationSpeed,jumpForce,maxStepHight,stepSmoothness,rotationSpeed180;
    Transform cam;
    Rigidbody rb;
    public bool IsSprint, IsCrouched=false,IsJump,IsGrounded,turn180;
    public float groundCheckDistance;
    public bool animationBusy;
    Animator animator;
    RaycastHit groundHit;
    public GameObject defaultCam, crouchCam;
    float timer=0;
    float desired_Y;
    Quaternion desiredRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        xinp = Input.GetAxis("Horizontal");
        yinp = Input.GetAxis("Vertical");

        IsSprint = Input.GetKey(KeyCode.LeftShift) ? true : false;
        if (Input.GetKeyDown(KeyCode.C)) { IsCrouched = !IsCrouched; }
        IsJump = Input.GetKeyDown(KeyCode.Space) ? true : false;
        animator.SetFloat("WalkSpeed", Mathf.Clamp01(GetInp().magnitude));
        if (Physics.Raycast(transform.position+Vector3.up*0.1f, Vector3.down,out groundHit, groundCheckDistance))
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded= false;
            print("Not grounded");

        }

    }
    private void FixedUpdate()
    {
        print(transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(transform.position+Vector3.up*0.01f, transform.forward+new Vector3(0, 0.01f, 0), out hit, 0.25f) && !Physics.Raycast(transform.position + new Vector3(0, maxStepHight, 0), transform.forward, 0.3f) && GetInp().magnitude > 0)
        {
            if(Vector3.Angle(hit.normal,Vector3.up)>45)
                rb.position =Vector3.Lerp(rb.position, rb.position+Vector3.up,stepSmoothness*Time.fixedDeltaTime);
                //rb.MovePosition(transform.position + new Vector3(0,stepSmoothness, 0));
        }
        else if (Physics.Raycast(transform.position + Vector3.up * 0.01f, transform.forward + new Vector3(0.45f, 0.01f, 0), out hit, 0.3f) && !Physics.Raycast(transform.position + new Vector3(0.45f, maxStepHight, 0), transform.forward, 0.35f) && GetInp().magnitude > 0)
        {
            if (Vector3.Angle(hit.normal, Vector3.up) > 45)
                rb.position = Vector3.Lerp(rb.position, rb.position + Vector3.up, stepSmoothness * Time.fixedDeltaTime);
            //rb.MovePosition(transform.position + new Vector3(0,stepSmoothness, 0));
        }
        else if (Physics.Raycast(transform.position + Vector3.up * 0.01f, transform.forward + new Vector3(-0.45f, 0.01f, 0), out hit, 0.3f) && !Physics.Raycast(transform.position + new Vector3(-0.45f, maxStepHight, 0), transform.forward, 0.35f) && GetInp().magnitude > 0)
        {
            if (Vector3.Angle(hit.normal, Vector3.up) > 45)
                rb.position = Vector3.Lerp(rb.position, rb.position + Vector3.up, stepSmoothness * Time.fixedDeltaTime);
            //rb.MovePosition(transform.position + new Vector3(0,stepSmoothness, 0));
        }
        else if (IsGrounded)
        {
            if (Vector3.Distance(groundHit.point, transform.position) > 0.1)
            {
                rb.position = Vector3.Lerp(rb.position, groundHit.point, 5 * Time.fixedDeltaTime);
            }
        }
    }

    public void HandlePlayerRotation()
    {
        if (turn180)
        {
            if ((animator.GetCurrentAnimatorStateInfo(0).IsName("walkTurn180") || animator.GetCurrentAnimatorStateInfo(0).IsName("RunTurn180")) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            {
                if (Mathf.Abs(Mathf.Abs(transform.rotation.y) - Mathf.Abs(desiredRotation.y)) > 0.01)
                {
                    print("transform.rotation:" + transform.rotation.y + " desired rotation:" + desiredRotation.y);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, rotationSpeed180 + rotationSpeed * Time.deltaTime);
                }
                else
                {
                    animator.SetBool("Turn 180", false);
                    turn180 = false;
                    animationBusy = false;
                }
            }
        }
        else if (GetInp().magnitude > 0.1f)
        {
            Vector3 direc = new Vector3(xinp, 0, yinp); 
            Vector3 cam = Camera.main.transform.forward;
            direc = Quaternion.LookRotation(new Vector3(cam.x, 0, cam.z)) * direc;
            Quaternion rotation = Quaternion.LookRotation(direc);
            if (Mathf.Abs(Vector3.SignedAngle(transform.forward, direc, Vector3.up)) > 165)
            {
                desiredRotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y + 180, 0));
                turn180 = true;
                return;
            }
            //rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.red;
        Gizmos.DrawLine(transform.position,transform.position+new Vector3(0,-groundCheckDistance,0));
        Gizmos.DrawLine(transform.position+Vector3.up*0.01f, transform.position + new Vector3(0,0.01f,0.25f));
        Gizmos.DrawLine(transform.position+Vector3.up*0.01f, transform.position + new Vector3(0.5f,0.01f,0.25f));
        Gizmos.DrawLine(transform.position+Vector3.up*0.01f, transform.position + new Vector3(-0.5f,0.01f,0.25f));
        Gizmos.DrawLine(transform.position + Vector3.up*maxStepHight, transform.position+new Vector3(0,maxStepHight,0.3f));
        Gizmos.DrawLine(transform.position + Vector3.up*maxStepHight, transform.position+new Vector3(1,maxStepHight,0.3f));
        Gizmos.DrawLine(transform.position + Vector3.up*maxStepHight, transform.position+new Vector3(-1,maxStepHight,0.3f));
    }


    public void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x,0,rb.linearVelocity.z);
        rb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
    }
    public float VerticalVelocity()
    {
        return rb.linearVelocity.y;
    }
    public Vector2 GetInp()
    {
        return new Vector2(xinp,yinp);
    }
}
