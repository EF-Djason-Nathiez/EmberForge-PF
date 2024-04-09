using System;
using System.Collections.Generic;
using System.Reflection;
using Artificial_Intelligence;
using Player;
using UnityEngine;

public class GameRepository : MonoSingleton<GameRepository>
{
    public PlayerManager playerManager;

    public List<AI> visibleNPC;
}

public static class GameRepositoryVisibleList
{
    public static void SubscribeToVisible(this AI n)
    {
        Logs.Log($"Tried to do subscribe");

        if (!GameRepository.instance.visibleNPC.Contains(n))
        {
            Logs.Log($"{MethodBase.GetCurrentMethod()}", $"{n.title} is now visible and has been added from list.");
            GameRepository.instance.visibleNPC.Add(n);
        }
    }

    public static void UnsubscribeToVisible(this AI n)
    {
        Logs.Log($"Tried to do unsubscribe");
        if (GameRepository.instance.visibleNPC.Contains(n))
        {
            Logs.Log($"{MethodBase.GetCurrentMethod()}", $"{n.title} is now invisible and has been removed from list.");
            GameRepository.instance.visibleNPC.Remove(n);
        }
    }
}