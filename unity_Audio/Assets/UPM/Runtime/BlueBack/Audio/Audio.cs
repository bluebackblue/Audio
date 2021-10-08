

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
	public class Audio : System.IDisposable
	{
		/** volume
		*/
		private float volume;

		/** player
		*/
		public System.Collections.Generic.Dictionary<string,Player_Base> player;

		/** callback_onunitydestroy
		*/
		private CallBack_OnUnityDestroy_MonoBehaviour callback_onunitydestroy;

		/** [シングルトン]constructor
		*/
		public Audio()
		{
			//PlayerLoopSystem
			UnityEngine.LowLevel.PlayerLoopSystem t_playerloopsystem = BlueBack.UnityPlayerLoop.UnityPlayerLoop.GetCurrentPlayerLoop();
			BlueBack.UnityPlayerLoop.Add.AddFromType(ref t_playerloopsystem,UnityPlayerLoop.Mode.AddFirst,typeof(UnityEngine.PlayerLoop.FixedUpdate),typeof(PlayerLoopType.Main),this.OnUnityFixedUpdate);
			BlueBack.UnityPlayerLoop.UnityPlayerLoop.SetPlayerLoop(t_playerloopsystem);

			//player
			this.player = new System.Collections.Generic.Dictionary<string,Player_Base>();

			//callback_gameobject
			UnityEngine.GameObject t_callback_gameobject = new UnityEngine.GameObject("audio_callback");
			UnityEngine.GameObject.DontDestroyOnLoad(t_callback_gameobject);
			this.callback_onunitydestroy = t_callback_gameobject.AddComponent<CallBack_OnUnityDestroy_MonoBehaviour>();
			this.callback_onunitydestroy.instance = this;
		}

		/** [シングルトン]削除。
		*/
		public void Dispose()
		{
			//PlayerLoopSystem
			UnityEngine.LowLevel.PlayerLoopSystem t_playerloopsystem = BlueBack.UnityPlayerLoop.UnityPlayerLoop.GetCurrentPlayerLoop();
			BlueBack.UnityPlayerLoop.Remove.RemoveFromType(ref t_playerloopsystem,typeof(PlayerLoopType.Main));
			BlueBack.UnityPlayerLoop.UnityPlayerLoop.SetPlayerLoop(t_playerloopsystem);

			if(this.callback_onunitydestroy != null){
				this.callback_onunitydestroy.instance = null;
				UnityEngine.GameObject.Destroy(this.callback_onunitydestroy.gameObject);
				this.callback_onunitydestroy = null;
			}
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

