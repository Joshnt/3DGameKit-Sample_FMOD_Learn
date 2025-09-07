using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MovementFMODEvents : MonoBehaviour
{
    public EventReference runStepPlayerEvent;
    public EventReference quickTurnStepPlayerEvent;
    public EventReference landingNormalPlayerEvent;
    public EventReference landingRollPlayerEvent;
    public EventReference respawnEvent;
    public EventReference idleSprechenEvent;
    Coroutine speechCoroutine;

    public SurfaceDatabase surfaceDatabase;

    bool idleSpeechIsRunning = false;

    public void Movement()
    {
        // Create instance
        FMOD.Studio.EventInstance instance = RuntimeManager.CreateInstance(runStepPlayerEvent);
        groundCheckForInstance(instance);
    }

    public void QuickTurn()
    {
        // Create instance
        FMOD.Studio.EventInstance instance = RuntimeManager.CreateInstance(quickTurnStepPlayerEvent);
        groundCheckForInstance(instance);
    }

    public void Landing(int isRollAnimation = 0)
    {
        // Create instance
        FMOD.Studio.EventInstance instance = RuntimeManager.CreateInstance(isRollAnimation == 1 ? landingRollPlayerEvent : landingNormalPlayerEvent);
        groundCheckForInstance(instance);
    }

    public void RespawnSound()
    {
        RuntimeManager.PlayOneShotAttached(respawnEvent, gameObject);
    }

    void groundCheckForInstance(EventInstance eventInstance)
    {
        string surfaceString = "Earth";

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1f, LayerMask.GetMask("Environment")))
        {
            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend == null) rend = hit.collider.GetComponentInChildren<Renderer>();

            if (rend != null && rend.sharedMaterial != null)
            {
                surfaceString = surfaceDatabase.GetSurfaceType(rend.sharedMaterial);
            }
        }

        eventInstance.setParameterByNameWithLabel("GroundType", surfaceString);

        RuntimeManager.AttachInstanceToGameObject(eventInstance, gameObject);

        eventInstance.start();
        eventInstance.release();
    }

    public void PlayIdleSpeechWithDelay()
    {
        speechCoroutine = StartCoroutine(TalkWithDelay());
        idleSpeechIsRunning = true;
    }

    public void StopCoroutineIdleSpeech()
    {
        if (speechCoroutine != null)
        {
            StopCoroutine(speechCoroutine);
            speechCoroutine = null;
        }
        idleSpeechIsRunning = false;
    }

    IEnumerator TalkWithDelay()
    {
        if (idleSpeechIsRunning)
            yield return null;

        while (true)
            {
                yield return new WaitForSeconds(Random.Range(10f, 20f));
                FMODUnity.RuntimeManager.PlayOneShot(idleSprechenEvent);
            }
    }
}
