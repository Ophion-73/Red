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
    }
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _move = InputSystem.actions.FindAction(PlayerStrings.PlayerInputStrings.move);
        _jump = InputSystem.actions.FindAction(PlayerStrings.PlayerInputStrings.jump);
        _red = InputSystem.actions.FindAction(PlayerStrings.PlayerInputStrings.red);
        _dash = InputSystem.actions.FindAction(PlayerStrings.PlayerInputStrings.dash);
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        InputRead();
        UpdateAnimatorParameters();
        Flip();
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
        
        _moveInput = _move.ReadValue<Vector2>();
        if(_jump.WasPressedThisFrame())
        {
            //Aqui Mandar a llamar el metodo jump, falta crearlo
            // Para el movimiento utiliza las siguientes funciones
            // _move.x
            // move.y
            _animator.SetTrigger("Jump");
        }
        
        AnimatorStateInfo state = _animator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("Jump"))
        {
            _animator.SetBool("IsGrounded", false);
        }
        else
        {
            _animator.SetBool("IsGrounded", true);
            Jump();
        }

        if(_red.WasPressedThisFrame()) 
        {
            AttackDirection dir = GetAttackDir(_moveInput);
            _attackSystem.Attack(isGrounded,dir);

            //Aqui mandar a llamar el metodo red, falta crearlo(recuerda que red referencia a todos los ataques de caperucita)
            _animator.SetTrigger("REDButton");
        }

        if(_dash.WasPressedThisFrame())
        {
            //Aqui Mandar a llamar el metodo Dash
            _animator.SetTrigger("Dodge");
            Dash();
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
            
            _anim.SetTrigger("Jump");
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
        if (input.y > 1) return AttackDirection.Up;
        if (input.y < -1) return AttackDirection.Down;
        if (input.x > 1) return AttackDirection.Right;
        if (input.x < -1) return AttackDirection.Left;
        return AttackDirection.Neutral;
    }
}



public static class PlayerStrings
{
    public static class PlayerInputStrings
    {
        public const string move = "Move";
        public const string red = "RED";
        public const string jump = "JUMP";
        public const string dash = "Dash";
     }
}