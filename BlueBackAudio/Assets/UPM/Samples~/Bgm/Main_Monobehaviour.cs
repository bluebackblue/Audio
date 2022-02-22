

/** BlueBack.Audio.Samples.Bgm
*/
namespace BlueBack.Audio.Samples.Bgm
{
	/** TestScene_Monobehaviour
	*/
	public sealed class TestScene_Monobehaviour : UnityEngine.MonoBehaviour
	{
		/** instance
		*/
		private BlueBack.Audio.Audio instance;

		/** Awake
		*/
		public void Awake()
		{
			//audiomixer
			UnityEngine.Audio.AudioMixer t_audiomixer = UnityEngine.Resources.Load<UnityEngine.GameObject>("Bgm/AudioMixer").GetComponent<BlueBack.Audio.AudioMixer_MonoBehaviour>().audiomixer;
			UnityEngine.Audio.AudioMixerGroup[] t_audiomixergroup_list = t_audiomixer.FindMatchingGroups("Master/Bgm");
			UnityEngine.Audio.AudioMixerGroup t_audiomixergroup = t_audiomixergroup_list[0];

			//instance
			this.instance = new BlueBack.Audio.Audio();
			BlueBack.Audio.Player_Bgm t_player = this.instance.CreateBgm("bgm_common",t_audiomixergroup);
			t_player.LoadRequest(UnityEngine.Resources.Load<UnityEngine.GameObject>("Bgm/BgmCommon").GetComponent<BlueBack.Audio.Bank_MonoBehaviour>().bank);
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

