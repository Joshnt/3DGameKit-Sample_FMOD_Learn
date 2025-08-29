using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gamekit3D
{
    public class GrenadierSMBPunch : SceneLinkedSMB<GrenadierBehaviour>
    {
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!m_MonoBehaviour.punchEvent.IsNull)
                FMODUnity.RuntimeManager.PlayOneShotAttached(m_MonoBehaviour.punchEvent, animator.gameObject);
        }
    }
}