using UnityEngine;
using System;
public class SoundEffects : MonoBehaviour
{
    [SerializeField] private CarMover  _mover;
    
    [SerializeField] private bool useSounds = false;
    
    [SerializeField] private  AudioSource carEngineSound; 
    [SerializeField] private  AudioSource tireScreechSound;
    
    private float initialCarEngineSoundPitch; 
    
    private void Start()
    {
        if(carEngineSound != null)
        {
            initialCarEngineSoundPitch = carEngineSound.pitch;
        }
        
        if(useSounds)
        {
            InvokeRepeating("CarSounds", 0f, 0.1f);
        }
        else if(!useSounds)
        {
            if(carEngineSound != null)
            {
                carEngineSound.Stop();
            }
            if(tireScreechSound != null)
            {
                tireScreechSound.Stop();
            }
        }
    }
    
    public void CarSounds()
    {
        if (useSounds)
        {
            try
            {
                if (carEngineSound != null)
                {
                    float engineSoundPitch =
                        initialCarEngineSoundPitch + (Mathf.Abs(_mover.carRigidbody.velocity.magnitude) / 25f);
                    carEngineSound.pitch = engineSoundPitch;
                }

                if ((_mover.isDrifting) || (_mover.isTractionLocked && Mathf.Abs(_mover.Speed) > 12f))
                {
                    if (!tireScreechSound.isPlaying)
                    {
                        tireScreechSound.Play();
                    }
                }
                else if ((!_mover.isDrifting) && (!_mover.isTractionLocked || Mathf.Abs(_mover.Speed) < 12f))
                {
                    tireScreechSound.Stop();
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex);
            }
        }
        else if (!useSounds)
        {
            if (carEngineSound != null && carEngineSound.isPlaying)
            {
                carEngineSound.Stop();
            }

            if (tireScreechSound != null && tireScreechSound.isPlaying)
            {
                tireScreechSound.Stop();
            }
        }
    }
}