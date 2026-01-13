using UnityEngine;
using UnityEngine.InputSystem;
public class Player : Entity
{
    public InputActionAsset actions;
    
    private InputAction _move;
    private InputAction _jump;
    private InputAction _red;
    private InputAction _dash;


    private bool isGrounded;

    public float walkSpeed;
    public float jumpSpeed;
    public float dashDistance;

    private void OnEnable()
    {
        actions.FindActionMap("Player").Enable();
    }
    private void OnDisable()
    {
        actions.FindActionMap("Player").Disable();
    }
    private void Awake()
    {
        _move = InputSystem.actions.FindAction(PlayerStrings.PlayerInputStrings.move);
        _jump = InputSystem.actions.FindAction(PlayerStrings.PlayerInputStrings.jump);
        _red = InputSystem.actions.FindAction(PlayerStrings.PlayerInputStrings.red);
        _dash = InputSystem.actions.FindAction(PlayerStrings.PlayerInputStrings.dash);
    }

    private void Update()
    {
        InputRead();
    }

    public void InputRead()
    {
        if(_jump.WasPressedThisFrame())
        {
            //Aqui Mandar a llamar el metodo jump, falta crearlo
            // Para el movimiento utiliza las siguientes funciones
            // _move.x
            // move.y
        }

        if(_red.WasPressedThisFrame()) 
        {
            //Aqui mandar a llamar el metodo red, falta crearlo(recuerda que red referencia a todos los ataques de caperucita)
        }

        if(_dash.WasPressedThisFrame())
        {
            //Aqui Mandar a llamar el metodo Dash
        }
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

