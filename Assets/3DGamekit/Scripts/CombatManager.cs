using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance { get; private set; }

    private HashSet<IEnemy> activeEnemies = new HashSet<IEnemy>();

    public bool PlayerInCombat => activeEnemies.Count > 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void RegisterEnemy(IEnemy enemy)
    {
        bool wasEmpty = activeEnemies.Count == 0;
        activeEnemies.Add(enemy);
        if (wasEmpty && activeEnemies.Count > 0)
            RuntimeManager.StudioSystem.setParameterByName("MC_inCombat", 1);
    }

    public void UnregisterEnemy(IEnemy enemy)
    {
        activeEnemies.Remove(enemy);
        if (activeEnemies.Count == 0)
            RuntimeManager.StudioSystem.setParameterByName("MC_inCombat", 0);
    }

    // Optional: get number of enemies currently engaged
    public int EnemyCount => activeEnemies.Count;
}
