using HarmonyLib;
using UnityEngine;

using System;
using System.Collections.Generic;


using ModUtilites.Patches;
namespace ModUtilites;

static public class Patcher {
	static public Harmony harmony = new Harmony("ValheimUtilites");
	static public Eventter.Event open = new Eventter.Event();
	static public void patching () {
		harmony.PatchAll(typeof(PlayerPatch));
		harmony.PatchAll(typeof(DoorPatch));
		harmony.PatchAll(typeof(SmelterPatch));
	}
}