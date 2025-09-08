using System.Collections;
using System.Collections.Generic;
using Gamekit3D;
using UnityEngine;
using UnityEngine.Events;

public class SwitchLanguageHelper : MonoBehaviour
{
    public UnityEvent GermanLanguage;
    public UnityEvent EnglishLanguage;

    public void NextLanguage()
    {
        Translator.SetLanguage(GetNextIndex());

        switch (Translator.CurrentLanguage)
        {
            case "German":
                GermanLanguage.Invoke();
                break;
            case "English":
                EnglishLanguage.Invoke();
                break;
        }
    }

    public int GetNextIndex()
    {
        if (Translator.Instance.phrases.Count == 0) return -1;

        int currentIndex = Translator.Instance.m_LanguageIndex + 1;

        if (currentIndex>= Translator.Instance.phrases.Count)
            currentIndex = 0; // restart at the beginning

        return currentIndex;
    }
}
