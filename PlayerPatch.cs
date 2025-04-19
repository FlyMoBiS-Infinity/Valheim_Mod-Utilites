using HarmonyLib;


namespace ModUtilites.Patches;


static public class Players {
	static public Eventter.Event @OnAwake = new Eventter.Event();

	static public Eventter.Event @OnSpawn = new Eventter.Event();
	static public Eventter.Event @OnDeath = new Eventter.Event();
}

[HarmonyPatch(typeof(Player))]
static internal class PlayerPatch {
	[HarmonyPatch("OnDeath")]
	[HarmonyPostfix]
	static void OnDeaded() { Players.OnDeath.onEvent(); }
	[HarmonyPatch("OnSpawned")]
	[HarmonyPostfix]
	static void OnSpawned() { Players.OnSpawn.onEvent(); }
}