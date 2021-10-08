

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * @brief オーディオ。ＢＧＭ。
*/


/** BlueBack.Audio
*/
namespace BlueBack.Audio
{
	/** Player_Bgm
	*/
	public class Player_Bgm : Player_Base, System.IDisposable
	{
		/** mode
		*/
		private Player_Bgm_Mode mode;

		/** playerparam
		*/
		private PlayerParam playerparam;

		/** audiosourceitem_list
		*/
		private AudioSourceItem[] audiosourceitem_list;

		/** request_bank
		*/
		private Bank request_bank;

		/** dataindex
		*/
		private int dataindex;

		/** request_dataindex
		*/
		private int request_dataindex;

		/** crossfadeframe
		*/
		private int crossfadeframe;
		private int crossfadeframe_max;

		/** constructor
		*/
		public Player_Bgm(Audio a_audio,UnityEngine.Audio.AudioMixerGroup a_audiomixergroup)
		{
			//mode
			this.mode = Player_Bgm_Mode.None;

			//playerparam
			UnityEngine.GameObject t_root_gameobject = new UnityEngine.GameObject("Player_Bgm");
			UnityEngine.GameObject.DontDestroyOnLoad(t_root_gameobject);
			this.playerparam = new PlayerParam(a_audio,t_root_gameobject);

			//audiosourceitem_list
			this.audiosourceitem_list = new AudioSourceItem[2];
			this.audiosourceitem_list[0] = new AudioSourceItem(t_root_gameobject.AddComponent<UnityEngine.AudioSource>(),this.playerparam);
			this.audiosourceitem_list[1] = new AudioSourceItem(t_root_gameobject.AddComponent<UnityEngine.AudioSource>(),this.playerparam);
			this.audiosourceitem_list[0].SetVolume(0.0f);
			this.audiosourceitem_list[1].SetVolume(0.0f);
			this.audiosourceitem_list[0].ApplyVolume();
			this.audiosourceitem_list[1].ApplyVolume();
			this.audiosourceitem_list[0].SetMixer(a_audiomixergroup);
			this.audiosourceitem_list[1].SetMixer(a_audiomixergroup);
			this.audiosourceitem_list[0].SetLoop(true);
			this.audiosourceitem_list[1].SetLoop(true);

			//request_bank
			this.request_bank = null;

			//dataindex
			this.dataindex = -1;

			//request_dataindex
			this.request_dataindex = -1;

			//crossfadeframe
			this.crossfadeframe = 0;
			this.crossfadeframe_max = 0;
		}

		/** [System.IDisposable]削除。
		*/
		public void Dispose()
		{
			if(this.playerparam.root_gameobject != null){
				UnityEngine.GameObject.DestroyImmediate(this.playerparam.root_gameobject);
				this.playerparam.root_gameobject = null;
			}
		}

		/** ロード。リクエスト。
		*/
		public bool LoadRequest(Bank a_bank)
		{
			if(this.playerparam.bank != null){
				#if(DEF_BLUEBACK_AUDIO_ASSERT)
				DebugTool.Assert(false,"Player_Bgm:LoadRequest:playerparam.bank != null");
				#endif
				return false;
			}

			if(this.request_bank != null){
				#if(DEF_BLUEBACK_AUDIO_ASSERT)
				DebugTool.Assert(false,"Player_Bgm:LoadRequest:request_bank != null");
				#endif
				return false;
			}

			this.request_bank = a_bank;
			#if(DEF_BLUEBACK_AUDIO_LOG)
			DebugTool.Log("Player_Bgm:LoadRequest:request_bank = " + this.request_bank.bankname);
			#endif

			return true;
		}

		/** アンロード。リクエスト。
		*/
		public bool UnLoadRequest()
		{
			if(this.request_bank == null){
				#if(DEF_BLUEBACK_AUDIO_ASSERT)
				DebugTool.Assert(false,"Player_Bgm:UnLoadRequest:request_bank == null");
				#endif
				return false;
			}

			if(this.playerparam.bank == null){
				#if(DEF_BLUEBACK_AUDIO_ASSERT)
				DebugTool.Assert(false,"Player_Bgm:UnLoadRequest:playerparam.bank == null");
				#endif
				return false;
			}

			this.request_bank = null;
			#if(DEF_BLUEBACK_AUDIO_LOG)
			DebugTool.Log("Player_Bgm:UnLoadRequest:request_bank = null");
			#endif

			return true;
		}

		/** 再生。リクエスト。
		*/
		public void PlayRequest(int a_dataindex)
		{
			this.request_dataindex = a_dataindex;
		}

		/** モード。取得。
		*/
		public Player_Bgm_Mode GetMode()
		{
			return this.mode;
		}

		/** クロスフェード。設定。
		*/
		public void SetCrossFadeFrame(int a_frame)
		{
			this.crossfadeframe_max = a_frame;
		}

		/** クロスフェード。チェック。
		*/
		public bool IsCrossFade()
		{
			#pragma warning disable 0162
			switch(this.mode){
			case Player_Bgm_Mode.Cross0To1:
			case Player_Bgm_Mode.Cross1To0:
				{
					return true;
				}break;
			}
			#pragma warning restore

			return false;
		}
		
		/** ボリューム。設定。
		*/
		public void SetVolume(float a_volume)
		{
			this.playerparam.volume = a_volume;
			this.audiosourceitem_list[0].ApplyVolume();
			this.audiosourceitem_list[1].ApplyVolume();
		}

		/** [BlueBack.Audio.Player_Base]ボリューム。更新。
		*/
		public void ApplyVolume()
		{
			this.audiosourceitem_list[0].ApplyVolume();
			this.audiosourceitem_list[1].ApplyVolume();
		}

		/** ボリューム。取得。
		*/
		public float GetVolume()
		{
			return this.playerparam.volume;
		}

		/** [BlueBack.Audio.Player_Base]更新。
		*/
		public void OnUnityFixedUpdate()
		{
			if(this.request_bank != this.playerparam.bank){
				//アンロードリクエスト。

				if(this.mode == Player_Bgm_Mode.None){
					this.playerparam.bank = this.request_bank;
				}else{
					//再生停止リクエスト。

					this.request_dataindex = -1;
					#if(DEF_BLUEBACK_AUDIO_LOG)
					DebugTool.Log("request_dataindex = -1");
					#endif
				}
			}

			#pragma warning disable 168
			switch(this.mode){
			case Player_Bgm_Mode.None:
				{
					if((this.request_dataindex >= 0)&&(this.playerparam.bank != null)&&(this.request_bank != null)){

						//dataindex
						this.dataindex = this.request_dataindex;

						//mode
						this.mode = Player_Bgm_Mode.Play0;

						AudioSourceItem t_current = this.audiosourceitem_list[0];
						t_current.SetData(this.request_dataindex);
						t_current.SetVolume(1.0f);
						t_current.ApplyVolume();
						t_current.PlayDirect();

						#if(DEF_BLUEBACK_AUDIO_LOG)
						DebugTool.Log("PlayDirect : " + this.request_dataindex.ToString());
						#endif
					}
				}break;
			case Player_Bgm_Mode.Play0:
			case Player_Bgm_Mode.Play1:
				{
					if(this.dataindex != this.request_dataindex){

						AudioSourceItem t_current;
						AudioSourceItem t_next;
						if(this.mode == Player_Bgm_Mode.Play0){
							t_current = this.audiosourceitem_list[0];
							t_next = this.audiosourceitem_list[1];
						}else{
							t_current = this.audiosourceitem_list[1];
							t_next = this.audiosourceitem_list[0];
						}

						//dataindex
						this.dataindex = this.request_dataindex;

						//mode
						if(this.mode == Player_Bgm_Mode.Play0){
							this.mode = Player_Bgm_Mode.Cross0To1;
						}else{
							this.mode = Player_Bgm_Mode.Cross1To0;
						}

						if(this.request_dataindex >= 0){
							t_next.SetData(this.request_dataindex);
							t_next.SetVolume(0.0f);
							t_next.ApplyVolume();
							t_next.PlayDirect();

							#if(DEF_BLUEBACK_AUDIO_LOG)
							DebugTool.Log("PlayDirect : " + this.request_dataindex.ToString());
							#endif
						}else{
							t_next.SetData(-1);
						}

						//crossfadeframe
						this.crossfadeframe = 0;
					}else{
						//再生中。
					}
				}break;
			case Player_Bgm_Mode.Cross0To1:
			case Player_Bgm_Mode.Cross1To0:
				{
					AudioSourceItem t_current;
					AudioSourceItem t_next;
					if(this.mode == Player_Bgm_Mode.Cross0To1){
						t_current = this.audiosourceitem_list[0];
						t_next = this.audiosourceitem_list[1];
					}else{
						t_current = this.audiosourceitem_list[1];
						t_next = this.audiosourceitem_list[0];
					}

					this.crossfadeframe++;

					if(this.crossfadeframe < this.crossfadeframe_max){
						//ボリューム。

						//フェードイン。
						if(t_next.dataindex >= 0){
							float t_per = UnityEngine.Mathf.Clamp((float)this.crossfadeframe / this.crossfadeframe_max,0.0f,1.0f);
							t_next.SetVolume(t_per);
							t_next.ApplyVolume();
						}

						//フェードアウト。
						{
							float t_per = UnityEngine.Mathf.Clamp(1.0f - (float)this.crossfadeframe / this.crossfadeframe_max,0.0f,1.0f);
							t_current.SetVolume(t_per);
							t_current.ApplyVolume();
						}
					}else{
						//ボリューム。

						if(t_next.dataindex >= 0){
							t_next.SetVolume(1.0f);
							t_next.ApplyVolume();
						}

						t_current.SetVolume(0.0f);
						t_current.ApplyVolume();

						//停止。
						t_current.Stop();
						t_current.SetData(-1);

						if(this.dataindex < 0){
							this.mode = Player_Bgm_Mode.None;
						}else if(this.mode == Player_Bgm_Mode.Cross0To1){
							this.mode = Player_Bgm_Mode.Play1;
						}else{
							this.mode = Player_Bgm_Mode.Play0;
						}
					}
				}break;
			}
			#pragma warning restore
		}
	}
}

