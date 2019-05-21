﻿using UnityEngine;
using ParkourRunner.Scripts.Managers;
using ParkourRunner.Scripts.Player.InvectorMods;

public class FinishMessage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var target = other.GetComponent<ParkourThirdPersonController>();

        if (target != null)
        {
            HUDManager.Instance.ShowGreatMessage(HUDManager.Messages.LevelComplete);

            EnvironmentController.CheckKeys();
            int level = PlayerPrefs.GetInt(EnvironmentController.LEVEL_KEY);
            int maxLevel = PlayerPrefs.GetInt(EnvironmentController.MAX_LEVEL);

            if (level == maxLevel)
            {
                maxLevel++;
                PlayerPrefs.SetInt(EnvironmentController.MAX_LEVEL, maxLevel);
                PlayerPrefs.Save();
            }
        }
    }
}