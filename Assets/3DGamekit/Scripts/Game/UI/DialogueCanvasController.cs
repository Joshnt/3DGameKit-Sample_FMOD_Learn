using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using TMPro;
using UnityEngine;

namespace Gamekit3D
{
    public class DialogueCanvasController : MonoBehaviour
    {
        public Animator animator;
        public TextMeshProUGUI textMeshProUGUI;

        protected Coroutine m_DeactivationCoroutine;

        protected readonly int m_HashActivePara = Animator.StringToHash("Active");
        [System.Serializable]
        public struct Phrase
        {
            public string key;           // e.g. "Hello", "Bye"
            public EventReference eventRef;  // Drag the FMOD event here
        }

        public Phrase[] phrases;

        private Dictionary<string, EventReference> phraseDict;

        public void Awake()
        {
            phraseDict = new Dictionary<string, EventReference>();
            foreach (var p in phrases)
            {
                if (!phraseDict.ContainsKey(p.key))
                    phraseDict.Add(p.key, p.eventRef);
            }
        }

        IEnumerator SetAnimatorParameterWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            animator.SetBool(m_HashActivePara, false);
        }

        public void ActivateCanvasWithText(string text)
        {
            if (m_DeactivationCoroutine != null)
            {
                StopCoroutine(m_DeactivationCoroutine);
                m_DeactivationCoroutine = null;
            }

            gameObject.SetActive(true);
            animator.SetBool(m_HashActivePara, true);
            textMeshProUGUI.text = text;
        }

        public void ActivateCanvasWithTranslatedText(string phraseKey)
        {
            if (m_DeactivationCoroutine != null)
            {
                StopCoroutine(m_DeactivationCoroutine);
                m_DeactivationCoroutine = null;
            }

            gameObject.SetActive(true);
            animator.SetBool(m_HashActivePara, true);
            textMeshProUGUI.text = Translator.Instance[phraseKey];

            playFMODDialogueEvent(phraseKey);
        }

        public void DeactivateCanvasWithDelay(float delay)
        {
            m_DeactivationCoroutine = StartCoroutine(SetAnimatorParameterWithDelay(delay));
        }

        void playFMODDialogueEvent(string phraseKey) {
            if (phraseDict.TryGetValue(phraseKey, out EventReference ev))
            

            RuntimeManager.PlayOneShot(ev);
        }
    }
}
