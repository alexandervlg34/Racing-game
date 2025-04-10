using System;
using UnityEngine;


public class Explosion : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _force;
    [SerializeField] private GameObject _explosionEffect;

    
    private bool _exploded;
    private void Explode()
    {
        if(_exploded) return;
        _exploded = true;
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody rb = colliders[i].attachedRigidbody;

            if (rb != null)
            {
                rb.AddExplosionForce(_force, transform.position, _radius);

                Explosion explosion = rb.GetComponent<Explosion>();

                if (explosion)
                {
                    if (Vector3.Distance(transform.position, rb.position) < _radius / 4f)
                    {
                        explosion.Explode();
                    }
                    
                    
                }
            }
        }
        
        Destroy(gameObject);
        Instantiate(_explosionEffect, transform.position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius / 4f);
        
    }
}