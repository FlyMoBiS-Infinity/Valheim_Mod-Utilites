using HarmonyLib;

using UnityEngine;

using System;


namespace ModUtilites.Patches;


static public class Doors {
	static public Eventter.Event @OnAwake = new Eventter.Event();

	static public Eventter.Event @OnOpened = new Eventter.Event();
	static public Eventter.Event @OnClosed = new Eventter.Event();

	static public Eventter.Event @OnUpdatedState = new Eventter.Event();
}

[HarmonyPatch(typeof(Door))]
static internal class DoorPatch {
	[HarmonyPatch("Awake")]
	[HarmonyPostfix]
	static void OnAwake() {
		Doors.OnAwake.onEvent();
	}

	[HarmonyPatch("UpdateState")]
	[HarmonyPrefix]
	static void OnUpdateState(ref Door __instance, ref bool __runOriginal, ref ZNetView ___m_nview, ref uint ___m_lastDataRevision, ref Animator ___m_animator) {
		__runOriginal = false;

		if (!___m_nview.IsValid()) {
			return;
		}
		uint dataRevision = ___m_nview.GetZDO().DataRevision;
		if (___m_lastDataRevision == dataRevision) {
			return;
		}

		___m_lastDataRevision = dataRevision;
		int @int = ___m_nview.GetZDO().GetInt(ZDOVars.s_state, 0);
		//this.SetState(@int);
		//SetState =>
		if (___m_animator.GetInteger("state") != @int) {
			if (@int != 0) {
				__instance.m_openEffects.Create(__instance.transform.position, __instance.transform.rotation, null, 1f, -1);
				Doors.OnOpened.onEvent();
			} else {
				__instance.m_closeEffects.Create(__instance.transform.position, __instance.transform.rotation, null, 1f, -1);
				Doors.OnClosed.onEvent();
			}
			___m_animator.SetInteger("state", @int);
		}
		if (__instance.m_openEnable) {
			__instance.m_openEnable.SetActive(@int != 0);
		}
		Doors.OnUpdatedState.onEvent();
		return;
	}
}