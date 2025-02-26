using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class ContrôleSousMarin : MonoBehaviour
{
    [SerializeField] private float _vitesseDéplacement = 5f;
    private Rigidbody _rb; 

    private Vector2 directionInput;
    private Animator _animator;

    void Start()
    {
        _rb = GetComponent<Rigidbody>(); 

        _animator = GetComponent<Animator>();

        _animator = GetComponent<Animator>();

        if (_rb == null)
        {
            Debug.LogError("Rigidbody manquant sur le sous-marin !");
        }
    }

    void OnDéplacer(InputValue directionBase)
    {
        directionInput = directionBase.Get<Vector2>() * _vitesseDéplacement;
    }

    void FixedUpdate()
    {
        _rb.velocity = directionInput;

        // Animation
        _animator.SetFloat("Vitesse", directionInput.magnitude);
    }

    void OnMonterDescendre(InputAction.CallbackContext context)
    {
        float inputValue = context.ReadValue<float>();
        directionInput.y = inputValue * _vitesseDéplacement;
    }

    void OnAccelerer(InputAction.CallbackContext context)
    {
        float inputValue = context.ReadValue<float>();
        directionInput.x = inputValue * _vitesseDéplacement;
    }
}
