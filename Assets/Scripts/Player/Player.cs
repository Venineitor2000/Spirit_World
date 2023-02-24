using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ControladorEspiritu
{
    public enum ME_states //Representan los estados de la Maquina de estados de Player
    {
        Quieto,
        EnMovimiento,
        AtaqueEstatica,
        AtaqueMovible,
        Muerto
    }
    
    
    Animator animatorComportamiento;
    Rigidbody rb; //Despues quizas pueda ser un character controller pero de momento usamos fisicas
    // Start is called before the first frame update
    void Start()
    {
        
        
        Inicializar();
        if(rb == null)
            rb = GetComponent<Rigidbody>();
        if(animatorComportamiento == null)
            animatorComportamiento = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Temporal solo para pruebas
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<HabilidadActiva>().Execute();
            Debug.Log("hola");
        }

        //Hasta aca es lo temproal

        if (!isDead)
        CheckHp();
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");
        if (horizontalAxis == 0 && verticalAxis == 0)
        {
            
            if(ChangeMeState(ME_states.Quieto))
            {
                animatorAnimaciones.SetTrigger(Animaciones.IdleBreathe.ToString());
                Moverse(0,0,0);
            }
            
            
        }

        else
        {
            if (ChangeMeState(ME_states.EnMovimiento))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                   
                    if (verticalAxis != 0)
                    {
                        //Establecer animacion
                        animatorAnimaciones.SetTrigger(Animaciones.Run.ToString());
                        animatorAnimaciones.SetFloat("RunMultiplier", verticalAxis);
                        //Realizar movimiento
                        
                    }

                    else if (horizontalAxis != 0)
                    {
                        if (horizontalAxis > 0)
                            animatorAnimaciones.SetTrigger(Animaciones.StrafeRight.ToString());
                        else
                            animatorAnimaciones.SetTrigger(Animaciones.StrafeLeft.ToString());
                    }

                    Moverse(speedRun, horizontalAxis, verticalAxis);


                }

                else
                {
                    if(verticalAxis != 0)
                    {
                        //Establecer animacion
                        animatorAnimaciones.SetTrigger(Animaciones.Walk.ToString());
                        animatorAnimaciones.SetFloat("WalkMultiplier", verticalAxis);
                        //Realizar movimiento
                        
                    }

                    else if(horizontalAxis != 0)
                    {
                        if(horizontalAxis > 0)
                            animatorAnimaciones.SetTrigger(Animaciones.StrafeRight.ToString());
                        else
                            animatorAnimaciones.SetTrigger(Animaciones.StrafeLeft.ToString());
                    }
                    Moverse(speedWalk, horizontalAxis, verticalAxis);
                }

            }
            
            
        }

        

    }

    public override void CheckHp()
    {
        if (GetHp() <= 0)
        {

            animatorAnimaciones.SetTrigger(Animaciones.Death.ToString());
            ChangeMeState(ME_states.Muerto);
            SetIsDead(true);
        }
    }

    bool ChangeMeState(ME_states state)
    {
        animatorComportamiento.SetTrigger(state.ToString());
        if (animatorComportamiento.GetCurrentAnimatorStateInfo(0).IsName(state.ToString()))
            return true;
        else
            return false;
    }

    void Moverse(float speed, float aceleracionX, float aceleracionZ)
    {
        
        
        Vector3 direction = transform.forward * aceleracionZ + transform.right * aceleracionX;
        direction.y = 0;
        direction.Normalize();
        Vector3 velocity = direction * speed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
        
    }

    
}
