

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * @brief オーディオ。
*/


/** BlueBack.Audio
*/
namespace BlueBack.Audio
{
	/** AudioSourceItem
	*/
	public class AudioSourceItem
	{
		/** raw
		*/
		public UnityEngine.AudioSource raw;

		/** playerparam
		*/
		public PlayerParam playerparam;

		/** volume
		*/
		public float volume;

		/** datavolume
		*/
		public float datavolume;

		/** dataindex
		*/
		public int dataindex;

		/** constructor
		*/
		public AudioSourceItem(UnityEngine.AudioSource a_audiosource,PlayerParam a_playerparam)
		{
			//raw
			this.raw = a_audiosource;
			this.raw.playOnAwake = false;

			//playerparam
			this.playerparam = a_playerparam;

			//volume
			this.volume = 0.0f;

			//datavolume
			this.datavolume = 1.0f;

			//dataindex
			this.dataindex = -1;
		}

		/** ボリューム。設定。
		*/
		public void SetVolume(float a_volume)
		{
			this.volume = a_volume;
		}

		/** ボリューム。適応。
		*/
		public void ApplyVolume()
		{
			float t_volume_new = this.playerparam.audio.GetMasterVolume() * this.playerparam.volume * this.volume * this.datavolume;
			if(t_volume_new != this.raw.volume){
				this.raw.volume = t_volume_new;
			}
		}

		/** ミキサー。設定。
		*/
		public void SetMixer(UnityEngine.Audio.AudioMixerGroup a_audiomixergroup)
		{
			this.raw.outputAudioMixerGroup = a_audiomixergroup;
		}

		/** ループ。設定。
		*/
		#pragma warning disable 168
		public void SetLoop(bool a_loop)
		{
			this.raw.loop = a_loop;
		}

		/** 再生。
		*/
		public void PlayDirect()
		{
			this.raw.Play();
		}

		/** 停止。
		*/
		public void Stop()
		{
			this.raw.Stop();
		}

		/** データ。設定。
		*/
		public void SetData(int a_dataindex)
		{
			if(this.playerparam.bank != null){
				if(this.playerparam.bank.list != null){
					if((0<=a_dataindex)&&(a_dataindex<this.playerparam.bank.list.Length)){
						this.raw.clip = this.playerparam.bank.list[a_dataindex].audioclip;
						this.datavolume = this.playerparam.bank.list[a_dataindex].datavolume;
						this.dataindex = a_dataindex;
						return;
					}
				}
			}

			this.raw.clip = null;
			this.datavolume = 0.0f;
			this.dataindex = -1;
		}
	}
}

