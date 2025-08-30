using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MovementFMODEvents : MonoBehaviour
{
    public EventReference runStepPlayerEvent;
    public EventReference walkStepPlayerEvent;
    public EventReference quickTurnStepPlayerEvent;
    public EventReference landingNormalPlayerEvent;
    public EventReference landingRollPlayerEvent;
    public EventReference respawnEvent;

    public void Movement(int isRunning = 0) // walk = 0, run = 1
    {
        // Create instance
        FMOD.Studio.EventInstance instance = RuntimeManager.CreateInstance(isRunning == 1 ? runStepPlayerEvent : walkStepPlayerEvent);
        groundCheckForInstance(instance);
        Debug.Log("played Movement with isRunning " + isRunning);
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
            SurfaceType surface = hit.collider.GetComponent<SurfaceType>();

            if (surface != null)
            {
                surfaceString = surface.surfaceTypeName.ToString();
            }
        }

        eventInstance.setParameterByNameWithLabel("GroundType", surfaceString);

        RuntimeManager.AttachInstanceToGameObject(eventInstance, gameObject);

        eventInstance.start();
        eventInstance.release();
    }
}
