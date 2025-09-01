using System.Collections;
using System.Collections.Generic;
using Gamekit3D.Message;
using UnityEngine;
using UnityEngine.AI;
using FMODUnity;
using FMOD.Studio;
#if UNITY_EDITOR
using UnityEditor;
using MessageType = UnityEditor.MessageType;
#endif

namespace Gamekit3D
{
    [DefaultExecutionOrder(100)]
    public class SpitterBehaviour : MonoBehaviour, IMessageReceiver
    {
        public static readonly int hashVerticalDot = Animator.StringToHash("VerticalHitDot");
        public static readonly int hashHorizontalDot = Animator.StringToHash("HorizontalHitDot");
        public static readonly int hashThrown = Animator.StringToHash("Thrown");
        public static readonly int hashHit = Animator.StringToHash("Hit");
        public static readonly int hashAttack = Animator.StringToHash("Attack");
        public static readonly int hashHaveEnemy = Animator.StringToHash("HaveTarget");
        public static readonly int hashFleeing = Animator.StringToHash("Fleeing");

        public static readonly int hashIdleState = Animator.StringToHash("Idle");

        public TargetScanner playerScanner;
        public float fleeingDistance = 3.0f;
        public RangeWeapon rangeWeapon;

        [Header("FMOD - Audio")]
        public EventReference attackEvent;
        public EventReference frontStepEvent;
        public EventReference backStepEvent;
        public EventReference hitEvent;
        public EventReference gruntEvent;
        public EventReference deathEvent;
        public EventReference spottedEvent;

        public EnemyController controller { get { return m_Controller; } }
        public PlayerController target { get { return m_Target; } }

        protected PlayerController m_Target = null;
        protected EnemyController m_Controller;
        protected bool m_Fleeing = false;

        protected Vector3 m_RememberedTargetPosition;
        public SurfaceDatabase surfaceDatabase;

        protected void OnEnable()
        {
            m_Controller = GetComponentInChildren<EnemyController>();

            m_Controller.animator.Play(hashIdleState, 0, Random.value);

            SceneLinkedSMB<SpitterBehaviour>.Initialise(m_Controller.animator, this);

        }

        public void OnReceiveMessage(Message.MessageType type, object sender, object msg)
        {
            switch (type)
            {
                case Message.MessageType.DEAD:
                    Death((Damageable.DamageMessage)msg);
                    break;
                case Message.MessageType.DAMAGED:
                    ApplyDamage((Damageable.DamageMessage)msg);
                    break;
                default:
                    break;
            }
        }

        public void Death(Damageable.DamageMessage msg)
        {
            Vector3 pushForce = transform.position - msg.damageSource;

            pushForce.y = 0;

            transform.forward = -pushForce.normalized;
            controller.AddForce(pushForce.normalized * 7.0f - Physics.gravity * 0.6f);

            controller.animator.SetTrigger(hashHit);
            controller.animator.SetTrigger(hashThrown);

            RuntimeManager.PlayOneShot(deathEvent, transform.position);
        }

        public void ApplyDamage(Damageable.DamageMessage msg)
        {
            if (msg.damager.name == "Staff")
                CameraShake.Shake(0.06f, 0.1f);

            float verticalDot = Vector3.Dot(Vector3.up, msg.direction);
            float horizontalDot = Vector3.Dot(transform.right, msg.direction);

            Vector3 pushForce = transform.position - msg.damageSource;

            pushForce.y = 0;

            transform.forward = -pushForce.normalized;
            controller.AddForce(pushForce.normalized * 5.5f, false);

            controller.animator.SetFloat(hashVerticalDot, verticalDot);
            controller.animator.SetFloat(hashHorizontalDot, horizontalDot);

            controller.animator.SetTrigger(hashHit);

            RuntimeManager.PlayOneShotAttached(hitEvent, gameObject);
        }

        public void Shoot()
        {
            rangeWeapon.Attack(m_RememberedTargetPosition);
        }

        public void TriggerAttack()
        {
            m_Controller.animator.SetTrigger(hashAttack);
        }

        public void RememberTargetPosition()
        {
            if (m_Target == null)
                return;

            m_RememberedTargetPosition = m_Target.transform.position;
        }

        void PlayStep(int frontFoot)
        {
            // Create instance
            EventInstance instance = RuntimeManager.CreateInstance(frontFoot == 1 ? frontStepEvent : backStepEvent);

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


            instance.setParameterByNameWithLabel("GroundType", surfaceString);

            RuntimeManager.AttachInstanceToGameObject(instance, gameObject);

            instance.start();
            instance.release();
        }

        public void Grunt ()
        {
            RuntimeManager.PlayOneShotAttached(gruntEvent, gameObject);
        }

        public void Spotted()
        {
            RuntimeManager.PlayOneShotAttached(spottedEvent, gameObject);
        }

        public void CheckNeedFleeing()
        {
            if (m_Target == null)
            {
                m_Fleeing = false;
                controller.animator.SetBool(hashFleeing, m_Fleeing);
                return;
            }

            Vector3 fromTarget = transform.position - m_Target.transform.position;

            if (m_Fleeing || fromTarget.sqrMagnitude <= fleeingDistance * fleeingDistance)
            {
                //player is too close from us, pick a point diametrically oppossite at twice that distance and try to move there.
                Vector3 fleePoint = transform.position + fromTarget.normalized * 2 * fleeingDistance;

                Debug.DrawLine(fleePoint, fleePoint + Vector3.up * 10.0f);

                if (!m_Fleeing)
                {
                    //if we're not already fleeing, we may be in the cooldown, so the navmesh agent is disabled, enable it
                    controller.SetFollowNavmeshAgent(true);
                }

                m_Fleeing = controller.SetTarget(fleePoint);

                if(m_Fleeing)
                    controller.animator.SetBool(hashFleeing, m_Fleeing);
            }

            if (m_Fleeing && fromTarget.sqrMagnitude > fleeingDistance * fleeingDistance * 4)
            {
                //we're twice the fleeing distance from the player and fleeing, we can stop now
                m_Fleeing = false;
                controller.animator.SetBool(hashFleeing, m_Fleeing);
            }
        }

        public void FindTarget()
        {
            //we ignore height difference if the target was already seen
            m_Target = playerScanner.Detect(transform, m_Target == null);
            m_Controller.animator.SetBool(hashHaveEnemy, m_Target != null);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            playerScanner.EditorGizmo(transform);
        }
#endif
    }
    
    
#if UNITY_EDITOR
    [CustomEditor(typeof(SpitterBehaviour))]
    public class SpitterBehaviourEditor : Editor
    {
        SpitterBehaviour m_Target;

        void OnEnable()
        {
            m_Target = target as SpitterBehaviour;
        }

        public override void OnInspectorGUI()
        {
            if (m_Target.playerScanner.detectionRadius < m_Target.fleeingDistance)
            {
                EditorGUILayout.HelpBox("The scanner detection radius is smaller than the fleeing range.\n" +
                    "The spitter will never shoot at the player as it will flee past the range at which it can see the player",
                    MessageType.Warning, true);    
            }
            
            base.OnInspectorGUI();
        }
    }

#endif
}
