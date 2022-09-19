

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief ＢＧＭ。プレイヤー。
*/


/** BlueBack.Bgm
*/
namespace BlueBack.Bgm
{
	/** Player_Param_Tool
	*/
	public static class Player_Param_Tool
	{
		/** Create
		*/
		public static void Create(ref Player_Param a_param,Bgm a_bgm,UnityEngine.Audio.AudioMixerGroup a_audiomixergroup,string a_objectname)
		{
			//gameobject
			UnityEngine.GameObject t_gameobject = new UnityEngine.GameObject(a_objectname);
			UnityEngine.GameObject.DontDestroyOnLoad(t_gameobject);
			a_param.gameobject = t_gameobject;

			//mode
			a_param.mode = Player_Mode.None;

			//bgm
			a_param.bgm = a_bgm;

			//volume
			a_param.volume = a_bgm.player_default_volume;

			//crossfadeframe
			a_param.crossfadeframe = 0;
			a_param.crossfadeframe_max = a_bgm.player_default_crossfadeframe_max;

			//dataindex
			a_param.dataindex = -1;
			a_param.dataindex_request = -1;

			//request_bank
			a_param.bank = null;
			a_param.bank_request = null;

			//audiosourcelist
			a_param.audiosourcelist = new AudioSourceItem[2];
			a_param.audiosourcelist[0] = new AudioSourceItem(t_gameobject.AddComponent<UnityEngine.AudioSource>(),a_param);
			a_param.audiosourcelist[1] = new AudioSourceItem(t_gameobject.AddComponent<UnityEngine.AudioSource>(),a_param);
			a_param.audiosourcelist[0].SetVolume(0.0f);
			a_param.audiosourcelist[1].SetVolume(0.0f);
			a_param.audiosourcelist[0].ApplyVolume();
			a_param.audiosourcelist[1].ApplyVolume();
			a_param.audiosourcelist[0].SetMixer(a_audiomixergroup);
			a_param.audiosourcelist[1].SetMixer(a_audiomixergroup);
			a_param.audiosourcelist[0].SetLoop(true);
			a_param.audiosourcelist[1].SetLoop(true);
		}
	}
}

