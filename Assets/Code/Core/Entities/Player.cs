using System;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public enum AttackDirection { Up, Down, Right, Left, Neutral }
public class Player : Entity
{
    public InputActionAsset actions;
    
    private InputAction _move;
    private InputAction _jump;
    private InputAction _red;
    private InputAction _dash;
    private InputAction _interact;


    [SerializeField] bool isGrounded;

    [Header("Movement Settings")]
    public float walkSpeed = 8f;
    public float jumpSpeed = 12f;
    public float dashForce = 20f;
    
    private Vector2 _moveInput;
    
    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    
    [Header("Interaction Settings")]
    [SerializeField] private float interactionRadius = 1.5f;
    [SerializeField] private LayerMask interactableLayer;
    
    private Animator _anim;

    private Animator _animator;

    private void OnEnable()
    {
        actions.FindActionMap("Player").Enable();
    }
    private void OnDisable()
    {
        actions.FindActionMap("Player").Disable();
    }
    protected override void Awake()
    {
        base.Awake();
        
        var map = actions.FindActionMap("Player");

        _move = map.FindAction(PlayerStrings.PlayerInputStrings.move);
        _jump = map.FindAction(PlayerStrings.PlayerInputStrings.jump);
        _red = map.FindAction(PlayerStrings.PlayerInputStrings.red);
        _dash = map.FindAction(PlayerStrings.PlayerInputStrings.dash);
        _interact = map.FindAction(PlayerStrings.PlayerInputStrings.interact);
    }
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        /*_move = InputSystem.actions.FindAction(PlayerStrings.PlayerInputStrings.move);
        _jump = InputSystem.actions.FindAction(PlayerStrings.PlayerInputStrings.jump);
        _red = InputSystem.actions.FindAction(PlayerStrings.PlayerInputStrings.red);
        _dash = InputSystem.actions.FindAction(PlayerStrings.PlayerInputStrings.dash);
        _interact = InputSystem.actions.FindAction(PlayerStrings.PlayerInputStrings.interact);*/
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        InputRead();
        UpdateAnimatorParameters();
        Flip();
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }

    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        ApplyMovement();
    }

    public void InputRead()
    {
        Vector2 moveInput = _move.ReadValue<Vector2>();
        
        _animator.SetFloat("Horizontal", moveInput.x);
        _animator.SetFloat("Vertical", moveInput.y);
        
        if(_jump.WasPressedThisFrame())
        {
            Jump();
            _animator.SetTrigger("Jump");
            _animator.SetBool("IsGrounded", false);
        }
        
        //AnimatorStateInfo state = _animator.GetCurrentAnimatorStateInfo(0);

        if(_red.WasPressedThisFrame()) 
        {
            AttackDirection dir = GetAttackDir(_moveInput);
            _attackSystem.Attack(isGrounded,dir);

            //Aqui mandar a llamar el metodo red, falta crearlo(recuerda que red referencia a todos los ataques de caperucita)
            _animator.SetTrigger("REDButton");
        }

        if(_dash.WasPressedThisFrame())
        {
            Dash();
            _animator.SetTrigger("Dodge");
        }

        if (_interact.WasPressedThisFrame())
        {
            PerformInteraction();
        }
    }
    
    private void ApplyMovement()
    {
        float horizontalSpeed = _moveInput.x * walkSpeed;
        float currentVerticalVelocity = _rb.linearVelocity.y;

        _rb.linearVelocity = new Vector2(horizontalSpeed, currentVerticalVelocity);
    }
    
    public void Jump()
    {
        if (isGrounded)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0); 
            _rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            
            isGrounded = false;
            
            _anim.SetTrigger("Jump");
            _animator.SetBool("IsGrounded",false);
        }
    }
    
    public void Dash()
    {
        float dashDirection = _moveInput.x != 0 ? Mathf.Sign(_moveInput.x) : transform.localScale.x;
        _rb.linearVelocity = new Vector2(dashDirection * dashForce, 0);
        
        _anim.SetTrigger("Dodge");
    }
    
    private void UpdateAnimatorParameters()
    {
        _anim.SetFloat("Horizontal", Mathf.Abs(_moveInput.x));
        _anim.SetFloat("Vertical", _rb.linearVelocity.y);
        _anim.SetBool("IsGrounded", isGrounded);
    }
    
    private void Flip()
    {
        if (_moveInput.x > 0 && transform.localScale.x < 0 || _moveInput.x < 0 && transform.localScale.x > 0)
        {
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    AttackDirection GetAttackDir(Vector2 input)
    {
        if (input.y > 0.5f) return AttackDirection.Up;
        if (input.y < -0.5f) return AttackDirection.Down;
        if (input.x > 0.5f) return AttackDirection.Right;
        if (input.x < -0.5f) return AttackDirection.Left;
        return AttackDirection.Neutral;
    }

    private void PerformInteraction()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, interactionRadius, interactableLayer);
        if (hit != null) Debug.Log("Interactuando con: " + hit.name);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}



public static class PlayerStrings
{
    public static class PlayerInputStrings
    {
        public const string move = "Move";
        public const string red = "RED";
        public const string jump = "Jump";
        public const string dash = "Dash";
        public const string interact = "Interact";
     }
}