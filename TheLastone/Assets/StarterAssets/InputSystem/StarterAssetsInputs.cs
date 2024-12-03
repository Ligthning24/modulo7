using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.Properties;
using UnityEngine.Rendering.Universal;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;
        public bool blood;
        public bool noblood;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

        [Header("Effect Settings")]
        public Image bloodEffectImage;
        private float r, g, b, a;
        public ParticleSystem healingEffect;
        private AudioSource curarJugador;
        public AudioClip fire;

        // Nueva bandera para controlar si el jugador se est� curando
        private bool isHealing = false;
        // Nueva bandera para rastrear el estado previo de curaci�n
        private bool wasHealing = false;

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        public void OnJump(InputValue value)
        {
            JumpInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }

        public void OnBlood(InputValue value)
        {
            blood = value.isPressed;
            if (blood)
            {
                Blood(); // Aumenta la opacidad del efecto de sangre
            }
        }

        public void OnNoBlood(InputValue value)
        {
            // Cambia el estado de curaci�n seg�n la entrada del jugador
            isHealing = value.isPressed;

            // Si se deja de curar, det�n el sonido y las part�culas
            if (!isHealing)
            {
                if (curarJugador.isPlaying)
                {
                    curarJugador.Stop(); // Det�n el sonido
                }

                if (healingEffect != null && healingEffect.isPlaying)
                {
                    healingEffect.Stop(); // Det�n las part�culas
                }
            }
        }
#endif

        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        public void BloodFromFireball()
        {
            a += 1f; // Incremento m�s grande para representar un da�o m�s severo
            a = Mathf.Clamp(a, 0, 1f); // Asegura que la opacidad est� entre 0 y 1
            ChangeColor(); // Aplica el cambio visual
        }

        private void Start()
        {
            if (bloodEffectImage != null)
            {
                r = bloodEffectImage.color.r;
                g = bloodEffectImage.color.g;
                b = bloodEffectImage.color.b;
                a = bloodEffectImage.color.a;
            }

            curarJugador = GetComponent<AudioSource>();
        }

        private void Update()
        {
            a = Mathf.Clamp(a, 0, 1f); // Limita la opacidad entre 0 y 1
            ChangeColor(); // Actualiza el color visualmente

            // Si el estado de curaci�n cambi� a verdadero
            if (isHealing && !wasHealing)
            {
                // Reproducir sonido solo al inicio de la curaci�n
                if (fire != null)
                {
                    curarJugador.PlayOneShot(fire, 1.0f);
                }
                if (healingEffect != null && !healingEffect.isPlaying)
                {
                    healingEffect.Play(); // Activa las part�culas
                }
            }

            // Mientras se est� curando, reducir la opacidad de la sangre
            if (isHealing)
            {
                NoBlood(); // Reduce visualmente el da�o
            }

            // Si se dej� de curar, detener efectos
            if (!isHealing && wasHealing)
            {
                if (curarJugador.isPlaying)
                {
                    curarJugador.Stop(); // Det�n el sonido
                }
                if (healingEffect != null && healingEffect.isPlaying)
                {
                    healingEffect.Stop(); // Det�n las part�culas
                }
            }

            // Actualiza el estado previo
            wasHealing = isHealing;
        }


        private void Blood()
        {
            a += 0.05f;
        }

        private void NoBlood()
        {
            a -= 0.07f; // Reduce el valor alfa
            ChangeColor(); // Actualiza visualmente el efecto de da�o
        }

        private void ChangeColor()
        {
            if (bloodEffectImage != null)
            {
                Color c = new Color(r, g, b, a);
                bloodEffectImage.color = c;
            }
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}
 