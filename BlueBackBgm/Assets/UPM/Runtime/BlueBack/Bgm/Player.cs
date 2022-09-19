

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief ＢＧＭ。プレイヤー。
*/


/** BlueBack.Bgm
*/
namespace BlueBack.Bgm
{
	/** Player
	*/
	public sealed class Player : System.IDisposable
	{
		/** param
		*/
		public Player_Param param;

		/** constructor
		*/
		public Player(Bgm a_bgm,UnityEngine.Audio.AudioMixerGroup a_audiomixergroup,string a_objectname)
		{
			//param
			this.param = new Player_Param();
			Player_Param_Tool.Create(ref this.param,a_bgm,a_audiomixergroup,a_objectname);

			//debugview
			#if(DEF_BLUEBACK_BGM_DEBUGVIEW)
			Player_DebugView_MonoBehaviour.Create(this.param.gameobject,this);
			#endif

			//登録。
			this.param.bgm.playerlist.Add(this);
		}

		/** [System.IDisposable]削除。
		*/
		public void Dispose()
		{
			//解除。
			this.param.bgm.playerlist.Remove(this);

			//root_gameobject
			if(this.param.gameobject != null){
				UnityEngine.GameObject.DestroyImmediate(this.param.gameobject);
				this.param.gameobject = null;
			}
		}

		/** ロード。リクエスト。
		*/
		public bool LoadRequest(Bank a_bank)
		{
			if(this.param.bank != null){
				#if(DEF_BLUEBACK_DEBUG_ASSERT)
				DebugTool.Assert(false,"Player:LoadRequest:param.bank != null");
				#endif
				return false;
			}

			if(this.param.bank_request != null){
				#if(DEF_BLUEBACK_DEBUG_ASSERT)
				DebugTool.Assert(false,"Player:LoadRequest:bank_request != null");
				#endif
				return false;
			}

			this.param.bank_request = a_bank;
			#if(DEF_BLUEBACK_DEBUG_LOG)
			DebugTool.Log("Player:LoadRequest:bank_request = " + this.param.bank_request.bankname);
			#endif

			return true;
		}

		/** アンロード。リクエスト。
		*/
		public bool UnLoadRequest()
		{
			if(this.param.bank_request == null){
				#if(DEF_BLUEBACK_DEBUG_ASSERT)
				DebugTool.Assert(false,"Player:UnLoadRequest:bank_request == null");
				#endif
				return false;
			}

			if(this.param.bank == null){
				#if(DEF_BLUEBACK_DEBUG_ASSERT)
				DebugTool.Assert(false,"Player:UnLoadRequest:param.bank == null");
				#endif
				return false;
			}

			this.param.bank_request = null;
			#if(DEF_BLUEBACK_DEBUG_LOG)
			DebugTool.Log("Player:UnLoadRequest:bank_request = null");
			#endif

			return true;
		}

		/** 再生。リクエスト。
		*/
		public void PlayRequest(int a_dataindex)
		{
			this.param.dataindex_request = a_dataindex;
		}

		/** クロスフェード。設定。
		*/
		public void SetCrossFadeFrame(int a_frame)
		{
			this.param.crossfadeframe_max = a_frame;
		}

		/** クロスフェード。チェック。
		*/
		public bool IsCrossFade()
		{
			#pragma warning disable 0162
			switch(this.param.mode){
			case Player_Mode.CrossFade0To1:
			case Player_Mode.CrossFade1To0:
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
			this.param.volume = a_volume;
			this.param.audiosourcelist[0].ApplyVolume();
			this.param.audiosourcelist[1].ApplyVolume();
		}

		/** ボリューム。更新。
		*/
		public void ApplyVolume()
		{
			this.param.audiosourcelist[0].ApplyVolume();
			this.param.audiosourcelist[1].ApplyVolume();
		}

		/** 更新。
		*/
		public void UnityFixedUpdate()
		{
			if(this.param.bank_request != this.param.bank){
				//アンロードリクエスト。

				if(this.param.mode == Player_Mode.None){
					this.param.bank = this.param.bank_request;
				}else{
					//再生停止リクエスト。

					this.param.dataindex_request = -1;
					#if(DEF_BLUEBACK_DEBUG_LOG)
					DebugTool.Log("dataindex_request = -1");
					#endif
				}
			}

			#pragma warning disable 168
			switch(this.param.mode){
			case Player_Mode.None:
				{
					if((this.param.dataindex_request >= 0)&&(this.param.bank != null)&&(this.param.bank_request != null)){

						//dataindex
						this.param.dataindex = this.param.dataindex_request;

						//mode
						this.param.mode = Player_Mode.Play0;

						AudioSourceItem t_current = this.param.audiosourcelist[0];
						t_current.SetData(this.param.dataindex_request);
						t_current.SetVolume(1.0f);
						t_current.ApplyVolume();
						t_current.PlayDirect();

						#if(DEF_BLUEBACK_DEBUG_LOG)
						DebugTool.Log("PlayDirect : " + this.param.dataindex_request.ToString());
						#endif
					}
				}break;
			case Player_Mode.Play0:
			case Player_Mode.Play1:
				{
					if(this.param.dataindex != this.param.dataindex_request){

						AudioSourceItem t_current;
						AudioSourceItem t_next;
						if(this.param.mode == Player_Mode.Play0){
							t_current = this.param.audiosourcelist[0];
							t_next = this.param.audiosourcelist[1];
						}else{
							t_current = this.param.audiosourcelist[1];
							t_next = this.param.audiosourcelist[0];
						}

						//dataindex
						this.param.dataindex = this.param.dataindex_request;

						//mode
						if(this.param.mode == Player_Mode.Play0){
							this.param.mode = Player_Mode.CrossFade0To1;
						}else{
							this.param.mode = Player_Mode.CrossFade1To0;
						}

						if(this.param.dataindex_request >= 0){
							t_next.SetData(this.param.dataindex_request);
							t_next.SetVolume(0.0f);
							t_next.ApplyVolume();
							t_next.PlayDirect();

							#if(DEF_BLUEBACK_DEBUG_LOG)
							DebugTool.Log("PlayDirect : " + this.param.dataindex_request.ToString());
							#endif
						}else{
							t_next.SetData(-1);
						}

						//crossfadeframe
						this.param.crossfadeframe = 0;
					}else{
						//再生中。
					}
				}break;
			case Player_Mode.CrossFade0To1:
			case Player_Mode.CrossFade1To0:
				{
					AudioSourceItem t_current;
					AudioSourceItem t_next;
					if(this.param.mode == Player_Mode.CrossFade0To1){
						t_current = this.param.audiosourcelist[0];
						t_next = this.param.audiosourcelist[1];
					}else{
						t_current = this.param.audiosourcelist[1];
						t_next = this.param.audiosourcelist[0];
					}

					this.param.crossfadeframe++;

					if(this.param.crossfadeframe < this.param.crossfadeframe_max){
						//ボリューム。

						//フェードイン。
						if(t_next.dataindex >= 0){
							float t_per = UnityEngine.Mathf.Clamp((float)this.param.crossfadeframe / this.param.crossfadeframe_max,0.0f,1.0f);
							t_next.SetVolume(t_per);
							t_next.ApplyVolume();
						}

						//フェードアウト。
						{
							float t_per = UnityEngine.Mathf.Clamp(1.0f - (float)this.param.crossfadeframe / this.param.crossfadeframe_max,0.0f,1.0f);
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

						if(this.param.dataindex < 0){
							this.param.mode = Player_Mode.None;
						}else if(this.param.mode == Player_Mode.CrossFade0To1){
							this.param.mode = Player_Mode.Play1;
						}else{
							this.param.mode = Player_Mode.Play0;
						}
					}
				}break;
			}
			#pragma warning restore
		}
	}
}

