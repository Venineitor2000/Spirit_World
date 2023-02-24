using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [SerializeField]float rangoMuerte;
    Transform owner;
    [SerializeField] int damage;
    [SerializeField] float speed;
    [SerializeField] Rigidbody rb;
    Vector3 posicionInicial;
    [SerializeField] GameObject particulaImpacto;
    [SerializeField] GameObject particulaFallo;
    public void Inicializar(Transform owner)
    {
        this.owner = owner;
        rb.velocity = transform.forward * speed;
    }

    private void Start()
    {
        posicionInicial = transform.position;
    }

    private void Update()
    {
        if((transform.position - posicionInicial).magnitude >= rangoMuerte)
        {
            Destroy(Instantiate(particulaFallo, transform.position, transform.rotation), 3f);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.transform != owner)
        {
            ControladorEspiritu target = other.GetComponent<ControladorEspiritu>();
            if (target != null)
            {
                target.TakeDamage(damage);

                Destroy(Instantiate(particulaImpacto, transform.position,transform.rotation), 3f);
                Destroy(gameObject);
            }
        }
        

    }
}
