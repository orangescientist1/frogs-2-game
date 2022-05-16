using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
[SerializeField]
    Rigidbody2D rb;
    public float speed = 7f;
    public float jumpForce = 12f;
    bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkGroundRadius = 0.4f;
    public LayerMask groundLayer;
    public float rememberGroundedFor = 0.06f;
    float lastTimeGrounded;
    bool canAttack = true;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public Animator anim;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }
    void Update(){
        Move();
        Jump();
        JumpPlus();
        CheckIfGrounded();
        Attack();

        if (rb.velocity.x == 0f){
            anim.SetBool("IsRunning", false);
            Debug.Log("HELP");
        } else{
            anim.SetBool("IsRunning", true);
        }

        if (isGrounded){
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsFalling", false);
        }

        if (rb.velocity.y == 0f){
            //Not Moving Vertically
        } else if (rb.velocity.y > 0.2f){
            anim.SetBool("IsJumping", true);
            anim.SetBool("IsFalling", false);
        } else if (rb.velocity.y < 0.2f){
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsFalling", true);
        }
    }
    void Move(){
        float x = Input.GetAxis("Horizontal");
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
        if(rb.velocity.x < 0 )
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        if(rb.velocity.x > 0 )
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);;
        }
    }
    void Jump(){
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor))
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }
    void JumpPlus()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    void CheckIfGrounded(){
        Collider2D colliders = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);
        if (colliders != null)
        {
            isGrounded = true;
        }
        else
        {
            if (isGrounded)
            {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
        }
    }
    public void Death(){

    }
    void Attack(){
        if(Input.GetKeyDown(KeyCode.Mouse0) & (canAttack)){
            StartCoroutine(AttackAnimation());
        }
    }
    public IEnumerator AttackAnimation(){
        canAttack = false;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.3f);
        canAttack = true;
    }

    public void Frogify(){

    }
}
