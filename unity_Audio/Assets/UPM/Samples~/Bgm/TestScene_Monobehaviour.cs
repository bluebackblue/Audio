

/** Samples.AssetLib.Asset
*/
namespace Samples.Audio.Bgm
{
	/** TestScene_Monobehaviour
	*/
	public class TestScene_Monobehaviour : UnityEngine.MonoBehaviour
	{
		/** audio
		*/
		private BlueBack.Audio.Audio audio;

		/** Awake
		*/
		public void Awake()
		{
			//Audio
			this.audio = new BlueBack.Audio.Audio();
			this.audio.CreateBgm(null);
			this.audio.bgm.LoadRequest(UnityEngine.Resources.Load<UnityEngine.GameObject>("BgmCommon").GetComponent<BlueBack.Audio.Bank_MonoBehaviour>().bank);
			this.audio.SetMasterVolume(1.0f);

			//Main
			StartCoroutine(this.Main());
		}

		/** Main
		*/
		public System.Collections.IEnumerator Main()
		{
			do{
				this.audio.bgm.PlayRequest(0);
				yield return new UnityEngine.WaitForSeconds(3);

				this.audio.bgm.PlayRequest(1);
				yield return new UnityEngine.WaitForSeconds(3);
			}while(true);
		}

		/** OnDestroy
		*/
		public void OnDestroy()
		{
			if(this.audio != null){
				this.audio.Dispose();
				this.audio = null;
			}
		}
	}
}

