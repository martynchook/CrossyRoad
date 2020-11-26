using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{  
    [SerializeField] private AudioSource DeathSound;

    public void PlayDeathSound()
    {
        DeathSound.Play();
    }
}
