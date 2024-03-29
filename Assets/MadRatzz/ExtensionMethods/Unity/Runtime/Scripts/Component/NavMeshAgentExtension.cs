﻿using UnityEngine.AI;

namespace MadRatz.Extensions
{
	public static class NavMeshAgentExtension
	{
		public static bool IsArrive(this NavMeshAgent agent)
		{
			if (!agent.enabled) return false;
			return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
		}
	}
}