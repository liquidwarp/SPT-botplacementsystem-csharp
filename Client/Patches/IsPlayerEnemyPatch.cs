using System.Reflection;
using BotPlacementSystemClient.Spawning;
using EFT;
using HarmonyLib;
using SPT.Reflection.Patching;

namespace BotPlacementSystemClient.Patches;

internal class IsPlayerEnemyPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return AccessTools.Method(typeof(BotsGroup), nameof(BotsGroup.IsPlayerEnemy));
    }

    [PatchPrefix]
    private static bool PatchPrefix(BotsGroup __instance, IPlayer player, ref bool __result)
    {
        if (player.IsAI && player.Profile.Info.Settings.Role is WildSpawnType.pmcBEAR or WildSpawnType.pmcUSEC)
        {
            // Check our group mappings - as we spawn bots faster than the group manager can assign groups and handle them.
            // The group's first bot can be any member of the squad, whoever finished spawning first.
            var leaderId = PmcGroupSpawner.GetLeaderId(__instance._initialBot.Profile.ProfileId);
            if (leaderId != null && leaderId == PmcGroupSpawner.GetLeaderId(player.Profile.ProfileId))
            {
                __result = false;
                return false;
            }
            
            if (__instance._initialBot.BotsGroup.Contains(player.AIData.BotOwner))
            {
                __result = false;
                return false;
            }

            __result = true;
            return false;
        }

        return true;
    }
}