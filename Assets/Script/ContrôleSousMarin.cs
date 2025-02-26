using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ContrôleSousMarin : MonoBehaviour
{
    [SerializeField] private float _vitesseDéplacement = 5f;
    [SerializeField] private float _modifierAnimTranslation;
    private Rigidbody _rb; 

    private Vector3 directionInput;
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

    

    void OnMonterDescendre(InputValue valeur)
    {
        float inputValue = valeur.Get<float>();
        directionInput.y = inputValue * _vitesseDéplacement;
        
    }

    void OnAccelererReculer(InputValue valeur)
    {
        float inputValue = valeur.Get<float>();
        directionInput.z = inputValue * _vitesseDéplacement;
    }

void FixedUpdate()
    {
        Vector3 mouvement = directionInput;
        _rb.velocity = directionInput;

        // Animation
        //_animator.SetFloat("Vitesse", directionInput.magnitude);
        _rb.AddForce(mouvement, ForceMode.VelocityChange);

        // calculer un modifiant pour la vitesse d'animation
        Vector3 vitesseSurPlane = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
        _animator.SetFloat("Vitesse", vitesseSurPlane.magnitude * _modifierAnimTranslation);
        _animator.SetFloat("Deplacement", vitesseSurPlane.magnitude);
    }
}
