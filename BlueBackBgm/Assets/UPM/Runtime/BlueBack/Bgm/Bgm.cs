

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief ＢＧＭ。
*/


/** define
*/
#if((ASMDEF_BLUEBACK_UNITYPLAYERLOOP)||(USERDEF_BLUEBACK_UNITYPLAYERLOOP))
#define ASMDEF_TRUE
#else
#warning "ASMDEF_TRUE"
#endif


/** BlueBack.Bgm
*/
namespace BlueBack.Bgm
{
	/** Bgm
	*/
	public sealed class Bgm : System.IDisposable
	{
		/** volume
		*/
		public float volume;

		/** playerlist
		*/
		public System.Collections.Generic.List<Player> playerlist;

		/** player_default
		*/
		public int player_default_crossfadeframe_max;
		public float player_default_volume;

		/** constructor
		*/
		public Bgm(in InitParam a_initparam)
		#if(ASMDEF_TRUE)
		{
			//PlayerLoopSystem
			UnityEngine.LowLevel.PlayerLoopSystem t_playerloopsystem = BlueBack.UnityPlayerLoop.UnityPlayerLoop.GetCurrentPlayerLoop();
			BlueBack.UnityPlayerLoop.Add.AddFromType(ref t_playerloopsystem,UnityPlayerLoop.Mode.AddFirst,typeof(UnityEngine.PlayerLoop.FixedUpdate),typeof(PlayerLoopType.UnityFixedUpdate),this.Inner_UnityFixedUpdate);
			BlueBack.UnityPlayerLoop.UnityPlayerLoop.SetPlayerLoop(t_playerloopsystem);
			BlueBack.UnityPlayerLoop.UnityPlayerLoop.SetDefaultPlayerLoopOnUnityDestroy(null);

			//volume
			this.volume = a_initparam.volume;

			//playerlist
			this.playerlist = new System.Collections.Generic.List<Player>();

			//player_default
			this.player_default_crossfadeframe_max = a_initparam.player_default_crossfadeframe_max;
			this.player_default_volume = a_initparam.player_default_volume;
		}
		#else
		{
		}
		#endif

		/** [System.IDisposable]削除。
		*/
		public void Dispose()
		#if(ASMDEF_TRUE)
		{
			//playerlist
			{
				while(this.playerlist.Count > 0){
					this.playerlist[0].Dispose();
				}
				this.playerlist = null;
			}

			//PlayerLoopSystem
			UnityEngine.LowLevel.PlayerLoopSystem t_playerloopsystem = BlueBack.UnityPlayerLoop.UnityPlayerLoop.GetCurrentPlayerLoop();
			BlueBack.UnityPlayerLoop.Remove.RemoveFromType(ref t_playerloopsystem,typeof(PlayerLoopType.UnityFixedUpdate));
			BlueBack.UnityPlayerLoop.UnityPlayerLoop.SetPlayerLoop(t_playerloopsystem);
		}
		#else
		{
		}
		#endif

		/** SetMasterVolume
		*/
		public void SetMasterVolume(float a_volume)
		{
			//volume
			this.volume = a_volume;

			//ApplyVolume
			{
				int ii_max = this.playerlist.Count;
				for(int ii=0;ii<ii_max;ii++){
					this.playerlist[ii].ApplyVolume();
				}
			}
		}

		/** Inner_UnityFixedUpdate
		*/
		private void Inner_UnityFixedUpdate()
		{
			//ApplyVolume
			{
				int ii_max = this.playerlist.Count;
				for(int ii=0;ii<ii_max;ii++){
					this.playerlist[ii].UnityFixedUpdate();
				}
			}
		}
	}
}

