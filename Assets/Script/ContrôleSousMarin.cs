using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ContrôleSousMarin : MonoBehaviour
{
    [SerializeField] private float _vitesseDéplacement = 5f;
    [SerializeField] private float _vitesseBoost = 10f; // Vitesse avec Shift
    [SerializeField] private float _modifierAnimTranslation;
    private Rigidbody _rb;
    private Vector3 directionInput;
    private Animator _animator;
    private float _boostMultiplier = 1f; // Par défaut, vitesse normale

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        if (_rb == null)
        {
            Debug.LogError("Rigidbody manquant sur le sous-marin !");
        }
    }

    void OnDéplacer(InputValue directionBase)
    {
        directionInput = directionBase.Get<Vector2>() * _vitesseDéplacement * _boostMultiplier;
    }

    void OnMonterDescendre(InputValue valeur)
    {
        float inputValue = valeur.Get<float>();
        directionInput.y = inputValue * _vitesseDéplacement * _boostMultiplier;
    }

    void OnAccelererReculer(InputValue valeur)
    {
        float inputValue = valeur.Get<float>();
        directionInput.z = inputValue * _vitesseDéplacement * _boostMultiplier;
    }

    // Gère le boost avec ton action "AccelererMouvement"
    void OnAccelererMouvement(InputValue valeur)
    {
        float boostValue = valeur.Get<float>(); // -1 (Right Shift) ou 1 (Left Shift)
        _boostMultiplier = (boostValue > 0) ? 2f : 0.5f; // Boost ou ralentissement
    }

    void FixedUpdate()
    {
        _rb.velocity = directionInput;

        // Animation
        _rb.AddForce(directionInput, ForceMode.VelocityChange);

        Vector3 vitesseSurPlane = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
        _animator.SetFloat("Vitesse", vitesseSurPlane.magnitude * _modifierAnimTranslation);
        _animator.SetFloat("Deplacement", vitesseSurPlane.magnitude);
    }
}
