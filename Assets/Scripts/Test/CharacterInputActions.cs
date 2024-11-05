using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputActions : MonoSingleton<CharacterInputActions>
{
    private InputSystem inputActions;

    protected override void Awake()
    {
        base.Awake();
        inputActions = new InputSystem();
        inputActions.Charater.Enable();          // ÷∂Øº§ªÓ
    }

    public Vector2 GetAxis()
    {
        return inputActions.Charater.Movement.ReadValue<Vector2>().normalized;
    }

    public bool IsLeftShiftPressed()
    {
        return inputActions.Charater.Sprint.IsPressed();
    }


}
