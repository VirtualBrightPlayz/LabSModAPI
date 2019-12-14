using Smod2.API;
using UnityEngine;

namespace LabSModAPI.API
{
	public class LabRound : Round
	{
		public override RoundStats Stats => throw new System.NotImplementedException();

		public override int Duration { get => RoundSummary.roundTime; }

		public override void AddNTFUnit(string unit)
		{
			NineTailedFoxUnits.host.list.Add(unit);
		}

		public override void EndRound()
		{
			RoundSummary.singleton.IsRoundEnded = true;
		}

		public override void MTFRespawn(bool isCI)
		{
			GameObject.FindObjectOfType<MTFRespawn>().nextWaveIsCI = isCI;
			GameObject.FindObjectOfType<MTFRespawn>().timeToNextRespawn = 0.1f;
		}

		public override void RestartRound()
		{
			PlayerManager.localPlayer.GetComponent<PlayerStats>().Roundrestart();
		}
	}
}