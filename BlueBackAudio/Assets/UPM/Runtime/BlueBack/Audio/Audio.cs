

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * @brief オーディオ。
*/


/** BlueBack.Audio
*/
namespace BlueBack.Audio
{
	/** Audio
	*/
	public sealed class Audio : System.IDisposable
	{
		/** volume
		*/
		private float volume;

		/** player
		*/
		public System.Collections.Generic.Dictionary<string,Player_Base> player;

		/** constructor
		*/
		public Audio()
		{
			//PlayerLoopSystem
			UnityEngine.LowLevel.PlayerLoopSystem t_playerloopsystem = BlueBack.UnityPlayerLoop.UnityPlayerLoop.GetCurrentPlayerLoop();
			BlueBack.UnityPlayerLoop.Add.AddFromType(ref t_playerloopsystem,UnityPlayerLoop.Mode.AddFirst,typeof(UnityEngine.PlayerLoop.FixedUpdate),typeof(PlayerLoopType.FixedUpdate),this.OnUnityFixedUpdate);
			BlueBack.UnityPlayerLoop.UnityPlayerLoop.SetPlayerLoop(t_playerloopsystem);
			BlueBack.UnityPlayerLoop.UnityPlayerLoop.SetDefaultPlayerLoopOnUnityDestroy();

			//player
			this.player = new System.Collections.Generic.Dictionary<string,Player_Base>();
		}

		/** [System.IDisposable]削除。
		*/
		public void Dispose()
		{
			//PlayerLoopSystem
			UnityEngine.LowLevel.PlayerLoopSystem t_playerloopsystem = BlueBack.UnityPlayerLoop.UnityPlayerLoop.GetCurrentPlayerLoop();
			BlueBack.UnityPlayerLoop.Remove.RemoveFromType(ref t_playerloopsystem,typeof(PlayerLoopType.FixedUpdate));
			BlueBack.UnityPlayerLoop.UnityPlayerLoop.SetPlayerLoop(t_playerloopsystem);
		}

		/** GetMasterVolume
		*/
		public float GetMasterVolume()
		{
			return this.volume;
		}

		/** GetBgmPlayer
		*/
		public Player_Bgm GetBgmPlayer(string a_playername)
		{
			Player_Base t_player;
			if(this.player.TryGetValue(a_playername,out t_player)){
				return t_player as Player_Bgm;
			}
			return null;
		}

		/** GetPlayer
		*/
		public Player_Base GetPlayer(string a_playername)
		{
			Player_Base t_player;
			if(this.player.TryGetValue(a_playername,out t_player)){
				return t_player;
			}
			return null;
		}

		/** SetMasterVolume
		*/
		public void SetMasterVolume(float a_volume)
		{
			//volume
			this.volume = a_volume;

			//ApplyVolume
			foreach(System.Collections.Generic.KeyValuePair<string,Player_Base> t_pair in this.player){
				t_pair.Value.ApplyVolume();
			}
		}

		/** CreateBgm
		*/
		public Player_Bgm CreateBgm(string a_playername,UnityEngine.Audio.AudioMixerGroup a_audiomixergroup)
		{
			Player_Bgm t_player = new Player_Bgm(this,a_audiomixergroup);
			this.player.Add(a_playername,t_player);
			return t_player;
		}

		/** OnUnityFixedUpdate
		*/
		private void OnUnityFixedUpdate()
		{
			foreach(System.Collections.Generic.KeyValuePair<string,Player_Base> t_pair in this.player){
				t_pair.Value.OnUnityFixedUpdate();
			}
		}
	}
}

