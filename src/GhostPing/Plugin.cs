using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace GhostPing;
[BepInAutoPlugin]
public partial class Plugin : BaseUnityPlugin
{
    internal static ManualLogSource Log { get; private set; } = null!;

    private void Awake()
    {
        Log = Logger;
        Log.LogInfo($"Plugin {Name} is loaded!");

        Harmony.CreateAndPatchAll(typeof(GhostPing));
    }
}

class GhostPing
{
    [HarmonyTranspiler]
    [HarmonyPatch(typeof(PointPinger), nameof(PointPinger.Update))]
    private static IEnumerable<CodeInstruction> IgnoreDeathCheck(IEnumerable<CodeInstruction> instructions)
    {
        var instructionList = instructions.ToList();

        if (instructionList.Count > 27)
        {
            instructionList.RemoveAt(27);
        }

        return instructionList;
    }

    [HarmonyTranspiler]
    [HarmonyPatch(typeof(PointPinger), nameof(PointPinger.ReceivePoint_Rpc))]
    private static IEnumerable<CodeInstruction> IgnoreLineOfSightCheck(IEnumerable<CodeInstruction> instructions)
    {
        var instructionList = instructions.ToList();

        if (instructionList.Count > 50)
        {
            instructionList.RemoveAt(50);
        }

        return instructionList;
    }
}
