using HarmonyLib;

using System;
using System.Collections.Generic;


namespace ModUtilites.Patches;


static public class Beehives {
	static public Eventter.Event @OnAwake = new Eventter.Event();

}


[HarmonyPatch(typeof(Beehive))]
static internal class BeehivePatch {
	[HarmonyPatch("Awake")]
	[HarmonyPostfix]
	static void OnAwake(ref Beehive __instance) {
		object[] lastResults = Beehives.OnAwake.onEvent(__instance.m_maxHoney, __instance.m_maxCover, __instance.m_secPerUnit);

		string s_m_maxhoney = "m_maxhoney";
		string s_m_maxcover = "m_maxcover";
		string s_m_secPerUnit = "m_secPerUnit";

		int h_m_maxhoney = s_m_maxhoney.GetHashCode();
		int h_m_maxcover = s_m_maxcover.GetHashCode();
		int h_m_secPerUnit = s_m_secPerUnit.GetHashCode();

		Dictionary<string, long> dict = lastResults.ToDictionary<string, long>(new List<Type>() {typeof(double)},
			new List<int>() {h_m_maxhoney, h_m_maxcover, h_m_secPerUnit});

		long _value;

		if (dict.TryGetValue(s_m_maxhoney, out _value)) {
			__instance.m_maxHoney = System.Convert.ToInt32(_value);
		}
		if (dict.TryGetValue(s_m_maxcover, out _value)) {
			__instance.m_maxCover = System.Convert.ToInt32(_value);
		}
		if (dict.TryGetValue(s_m_secPerUnit, out _value)) {
			__instance.m_secPerUnit = System.Convert.ToInt32(_value);
		}
	}
}