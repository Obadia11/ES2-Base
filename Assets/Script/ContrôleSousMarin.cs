using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ContrôleSousMarin : MonoBehaviour
{
    [SerializeField] private float _vitesseDéplacement = 5f;
    [SerializeField] private float _modifierAnimTranslation = 1f;
    private Rigidbody _rb;
    private Vector3 directionInput;
    private Animator _animator;
    private bool estAccéléré = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        if (_rb == null)
        {
            Debug.LogError("Rigidbody manquant sur le sous-marin !");
        }
        if (_animator == null)
        {
            Debug.LogError("Animator manquant sur le sous-marin !");
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

    void OnAccelererMouvement(InputValue valeur)
    {
        float accelInput = valeur.Get<float>();
        estAccéléré = accelInput > 0;
    }

    void FixedUpdate()
    {
        // Appliquer le mouvement et accélération (2x si Shift est pressé)
        Vector3 mouvement = directionInput * (estAccéléré ? 2f : 1f);
        _rb.velocity = mouvement;

        // Vitesse horizontale et verticale
        float vitesseHorizontale = new Vector2(_rb.velocity.x, _rb.velocity.z).magnitude;
        float vitesseVerticale = Mathf.Abs(_rb.velocity.y);

        // Choix des animations selon le mouvement
        if (_rb.velocity == Vector3.zero)
        {
            _animator.Play("AnimRepos");
        }
        else if (vitesseVerticale > 0)
        {
            _animator.Play("AnimPetitesHelices");
        }
        else
        {
            _animator.Play("AnimGrandeHelice");
        }

        // Ajuster la vitesse des animations (inversée si on recule)
        float vitesseAnim = (vitesseHorizontale + vitesseVerticale) * _modifierAnimTranslation;
        _animator.SetFloat("VitesseAnim", estAccéléré ? vitesseAnim * 2f : vitesseAnim);
        _animator.SetFloat("Direction", Mathf.Sign(_rb.velocity.z)); // 1 pour avant, -1 pour arrière
    }
}
