

/** BlueBack.Bgm.Samples.Simple
*/
#if(!DEF_BLUEBACK_AUDIO_SAMPLES_DISABLE)
namespace BlueBack.Bgm.Samples.Simple
{
	/** Main_Monobehaviour
	*/
	public sealed class Main_Monobehaviour : UnityEngine.MonoBehaviour
	{
		/** instance
		*/
		private BlueBack.Bgm.Bgm instance;

		/** Awake
		*/
		public void Awake()
		{
			//listener
			this.gameObject.AddComponent<UnityEngine.AudioListener>();

			//audiomixer
			UnityEngine.Audio.AudioMixer t_audiomixer = UnityEngine.Resources.Load<UnityEngine.GameObject>("BlueBack.Bgm.Samples.Simple/AudioMixer").GetComponent<BlueBack.Bgm.AudioMixer_MonoBehaviour>().audiomixer;
			UnityEngine.Audio.AudioMixerGroup[] t_audiomixergroup_list = t_audiomixer.FindMatchingGroups("Master/Bgm");
			UnityEngine.Audio.AudioMixerGroup t_audiomixergroup = t_audiomixergroup_list[0];

			//instance
			BlueBack.Bgm.InitParam t_initparam = BlueBack.Bgm.InitParam.CreateDefault();
			{
			}
			this.instance = new BlueBack.Bgm.Bgm(in t_initparam);
			BlueBack.Bgm.Player_Bgm t_player = this.instance.CreateBgm("bgm_common",t_audiomixergroup);
			t_player.LoadRequest(UnityEngine.Resources.Load<UnityEngine.GameObject>("BlueBack.Bgm.Samples.Simple/BgmCommon").GetComponent<BlueBack.Bgm.Bank_MonoBehaviour>().bank);
			this.instance.SetMasterVolume(1.0f);
			t_player.SetVolume(1.0f);
			t_player.SetCrossFadeFrame(10);

			//Main
			StartCoroutine(this.CoroutineMain());
		}

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain()
		{
			do{
				this.instance.GetBgmPlayer("bgm_common").PlayRequest(0);
				yield return new UnityEngine.WaitForSeconds(4);
				this.instance.GetBgmPlayer("bgm_common").PlayRequest(1);
				yield return new UnityEngine.WaitForSeconds(4);
			}while(true);
		}

		/** OnDestroy
		*/
		public void OnDestroy()
		{
			if(this.instance != null){
				this.instance.Dispose();
				this.instance = null;
			}
		}
	}
}
#endif

