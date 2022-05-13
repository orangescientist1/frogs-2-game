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
    public GameObject Sword;
    bool canAttack = true;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }
    void Update(){
        Move();
        Jump();
        CheckIfGrounded();
        Attack();
    }
    void Move(){
        float x = Input.GetAxis("Horizontal");
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
    }
    void Jump(){
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
        //Temporary Attack Animation
        canAttack = false;
        Sword.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Sword.SetActive(false);
        canAttack = true;
    }
}