

/** BlueBack.Bgm.Samples.Simple
*/
#if(!DEF_BLUEBACK_AUDIO_SAMPLES_DISABLE)
namespace BlueBack.Bgm.Samples.Simple
{
	/** Main_Monobehaviour
	*/
	public sealed class Main_Monobehaviour : UnityEngine.MonoBehaviour
	{
		/** bgm
		*/
		private BlueBack.Bgm.Bgm bgm;

		/** player
		*/
		private BlueBack.Bgm.Player player;

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

			//bgm
			{
				//initparam
				BlueBack.Bgm.InitParam t_initparam = BlueBack.Bgm.InitParam.CreateDefault();
				{
					t_initparam.volume = 1.0f;
					t_initparam.player_default_crossfadeframe_max = 100;
					t_initparam.player_default_volume = 1.0f;
				}

				//bgm
				this.bgm = new BlueBack.Bgm.Bgm(in t_initparam);

				//player
				this.player = new BlueBack.Bgm.Player(this.bgm,t_audiomixergroup,"player");
				this.player.LoadRequest(UnityEngine.Resources.Load<UnityEngine.GameObject>("BlueBack.Bgm.Samples.Simple/BgmCommon").GetComponent<BlueBack.Bgm.Bank_MonoBehaviour>().bank);
			}

			//Main
			StartCoroutine(this.CoroutineMain());
		}

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain()
		{
			do{
				this.player.PlayRequest(0);
				yield return new UnityEngine.WaitForSeconds(4);
				this.player.PlayRequest(1);
				yield return new UnityEngine.WaitForSeconds(4);
			}while(true);
		}

		/** OnDestroy
		*/
		public void OnDestroy()
		{
			//player
			if(this.player != null){
				this.player.Dispose();
				this.player = null;
			}

			//bgm
			if(this.bgm != null){
				this.bgm.Dispose();
				this.bgm = null;
			}
		}
	}
}
#endif

