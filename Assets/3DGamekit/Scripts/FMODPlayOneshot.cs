using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODPlayOneshot : MonoBehaviour
{
    public EventReference eventReference;


    public void PlayFMODEventOneShot()
    {
        RuntimeManager.PlayOneShot(eventReference, transform.position);
    }

    public void PlayFMODEventOneShotAttached()
    {
        RuntimeManager.PlayOneShotAttached(eventReference, gameObject);
    }
}
