using HarmonyLib;

using System;
using System.Collections.Generic;


namespace ModUtilites.Patches;


static public class Smelters {
	static public Eventter.Event @OnAwake = new Eventter.Event();
	static public Eventter.Event @OnDestroyed = new Eventter.Event();
}

[HarmonyPatch(typeof(Smelter))]
static internal class SmelterPatch {
	[HarmonyPatch("Awake")]
	[HarmonyPostfix]
	static void OnAwake (ref Smelter __instance, ref float ___m_addedOreTime) {
		object[] lastResults = Smelters.OnAwake.onEvent(__instance.m_maxFuel, __instance.m_maxOre, __instance.gameObject);
		
		string s_m_maxfuel = "m_maxfuel";
		string s_m_maxore = "m_maxore";
		string s_fuelperproduct = "m_fuelperproduct";
		string s_m_secperproduct = "m_secperproduct";
		string s_m_addedoretime = "m_addedoretime";

		int h_m_maxfuel = s_m_maxfuel.GetHashCode();
		int h_m_maxore = s_m_maxore.GetHashCode();
		int h_fuelperproductl = s_fuelperproduct.GetHashCode();
		int h_m_secperproduct = s_m_secperproduct.GetHashCode();
		int h_m_addedoretime = s_m_addedoretime.GetHashCode();

		Dictionary<System.String, System.Int64> dict = lastResults.ToDictionary<string, long>(new List<Type>() {typeof(System.Double)},
			new List<int>() {h_m_maxfuel, h_m_maxore, h_fuelperproductl, h_m_secperproduct, h_m_addedoretime});

		long _value;

		if (dict.TryGetValue(s_m_maxfuel, out _value)) {
			__instance.m_maxFuel = System.Convert.ToInt32(_value);
		}
		if (dict.TryGetValue(s_m_maxore, out _value)) {
			__instance.m_maxOre = System.Convert.ToInt32(_value);
		}

		if (dict.TryGetValue(s_fuelperproduct, out _value)) {
			__instance.m_fuelPerProduct = System.Convert.ToInt32(_value);
		}
		if (dict.TryGetValue(s_m_secperproduct, out _value)) {
			__instance.m_secPerProduct = System.Convert.ToSingle(_value);
		}
		if (dict.TryGetValue(s_m_addedoretime, out _value)) {
			___m_addedOreTime = System.Convert.ToSingle(_value);
		}
	}

	[HarmonyPatch("OnDestroyed")]
	[HarmonyPostfix]
	static void OnDestroyed (ref ZNetView ___m_nview) {
		Smelters.OnDestroyed.onEvent(___m_nview.IsOwner());
	}
}