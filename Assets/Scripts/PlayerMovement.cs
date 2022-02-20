using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{ 
    FreeMove,
    Planting,
    Sleeping,
    Brewing,
    BackpackChoosing,
    battling,
    MenuOpening,
}

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public bool m_FacingRight = true;
    public Rigidbody rb;

    public PlayerState playerState = PlayerState.FreeMove;

    public Vector3 movement;
    [SerializeField] GameObject FrontBody; SpriteRenderer[] FrontSP;
    [SerializeField] GameObject BackBody;
    Animator animate;

    private void Start()
    {
        animate = FrontBody.GetComponent<Animator>();
        FrontSP = FrontBody.GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
       
        if (playerState == PlayerState.FreeMove)
        {   
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.z = Input.GetAxisRaw("Vertical");
            if (movement.x > 0 && !m_FacingRight)
        {
            Flip();
        }
            else if (movement.x < 0 && m_FacingRight)
        {
            Flip();
        }
        }
        
    }

    private void FixedUpdate()
    {
        if (playerState == PlayerState.FreeMove)
        {
            rb.MovePosition(rb.position + movement * Time.fixedDeltaTime * moveSpeed);
            {
                if (movement.z > 0)
                {
                    foreach (var item in FrontSP)
                    {
                        item.enabled = false;
                    }
                    BackBody.SetActive(true);
                }
                else
                {
                    BackBody.SetActive(false);
                    foreach (var item in FrontSP)
                    {
                        item.enabled = true;
                    }
                }
                animate.SetFloat("Vertical", Mathf.Abs(movement.x));
                animate.SetFloat("Horizontal", movement.z);
                animate.SetFloat("Speed", movement.sqrMagnitude);
            }
        }
    }
    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        float scaleX = this.transform.localScale.x*-1;        
        this.transform.localScale = new Vector3(scaleX, this.transform.localScale.y,this.transform.localScale.z);        
    }

    public void IM_Planting()
    {
        animate.SetBool("Planting",true);
        playerState = PlayerState.Planting;
        StartCoroutine(Delay.DelayToInvokeDo(() => 
        {
            playerState = PlayerState.FreeMove;
            animate.SetBool("Planting", false);
        }, 
        2.6f));
    }
    public void IM_Fertilizing()
    {
        animate.SetBool("Fertilizing", true);
        playerState = PlayerState.Planting;
        StartCoroutine(Delay.DelayToInvokeDo(() =>
        {
            playerState = PlayerState.FreeMove;
            animate.SetBool("Fertilizing", false);
        },
        2.1f));
    }
    public void IM_Sleeping ()
    {
        playerState = PlayerState.Sleeping;
    }
    public void setAnimate ()
    {
        if (movement.z > 0)
        {
            foreach (var item in FrontSP)
            {
                item.enabled = false;
            }
            BackBody.SetActive(true);
        }
        else
        {
            BackBody.SetActive(false);
            foreach (var item in FrontSP)
            {
                item.enabled = true;
            }
        }
        animate.SetFloat("Vertical", Mathf.Abs(movement.x));
        animate.SetFloat("Horizontal", movement.z);
        animate.SetFloat("Speed", movement.sqrMagnitude);
    }

}
