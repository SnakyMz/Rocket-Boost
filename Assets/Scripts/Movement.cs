using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 1000f;
    [SerializeField] float rotationStrength = 100f;
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftEngineParticles;
    [SerializeField] ParticleSystem rightEngineParticles;

    AudioSource audioSource;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngineSFX);
            }
            if (!mainEngineParticles.isPlaying)
            {
                mainEngineParticles.Play();
            }
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        
        if (rotationInput < 0)
        {
            ApplyRotation(rotationStrength);
            if (!rightEngineParticles.isPlaying)
            {
                leftEngineParticles.Stop();
                rightEngineParticles.Play();
            }
        }
        else if (rotationInput > 0)
        {
            ApplyRotation(-rotationStrength);
            if (!leftEngineParticles.isPlaying)
            {
                rightEngineParticles.Stop();
                leftEngineParticles.Play();
            }
        }
        else
        {
            leftEngineParticles.Stop();
            rightEngineParticles.Stop();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
