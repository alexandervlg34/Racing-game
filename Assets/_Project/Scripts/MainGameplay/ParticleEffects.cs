using System;
using UnityEngine;

public class ParticleEffects : MonoBehaviour
{
    public bool useEffects = false;

    [SerializeField] private CarMover _mover;

    [SerializeField] private ParticleSystem RLWParticleSystem;
    [SerializeField] private ParticleSystem RRWParticleSystem;

    [SerializeField] private TrailRenderer RLWTireSkid;
    [SerializeField] private TrailRenderer RRWTireSkid;
    
    private void Start()
    {
        if (!useEffects)
        {
            if (RLWParticleSystem != null)
            {
                RLWParticleSystem.Stop();
            }

            if (RRWParticleSystem != null)
            {
                RRWParticleSystem.Stop();
            }

            if (RLWTireSkid != null)
            {
                RLWTireSkid.emitting = false;
            }

            if (RRWTireSkid != null)
            {
                RRWTireSkid.emitting = false;
            }
        }
    }

    public void DriftCarPS()
    {
        if (useEffects)
        {
            try
            {
                if (_mover.isDrifting)
                {
                    RLWParticleSystem.Play();
                    RRWParticleSystem.Play();
                }
                else if (!_mover.isDrifting)
                {
                    RLWParticleSystem.Stop();
                    RRWParticleSystem.Stop();
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex);
            }

            try
            {
                if ((_mover.isTractionLocked || Mathf.Abs(_mover.localVelocityX) > 5f) &&
                    Mathf.Abs(_mover.Speed) > 12f)
                {
                    RLWTireSkid.emitting = true;
                    RRWTireSkid.emitting = true;
                }
                else
                {
                    RLWTireSkid.emitting = false;
                    RRWTireSkid.emitting = false;
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex);
            }
        }
        else if (!useEffects)
        {
            if (RLWParticleSystem != null)
            {
                RLWParticleSystem.Stop();
            }

            if (RRWParticleSystem != null)
            {
                RRWParticleSystem.Stop();
            }

            if (RLWTireSkid != null)
            {
                RLWTireSkid.emitting = false;
            }

            if (RRWTireSkid != null)
            {
                RRWTireSkid.emitting = false;
            }
        }
    }
}