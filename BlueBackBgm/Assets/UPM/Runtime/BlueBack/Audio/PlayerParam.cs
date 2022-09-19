

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief オーディオ。プレイヤーパラメータ。
*/


/** BlueBack.Audio
*/
namespace BlueBack.Audio
{
	/** PlayerParam
	*/
	public sealed class PlayerParam
	{
		/** audio
		*/
		public Audio audio;

		/** volume
		*/
		public float volume;

		/** root_gameobject
		*/
		public UnityEngine.GameObject root_gameobject;

		/** bank
		*/
		public Bank bank;

		/** constructor
		*/
		public PlayerParam(Audio a_audio,UnityEngine.GameObject a_root_gameobject)
		{
			//audio
			this.audio = a_audio;

			//volume
			this.volume = 0.0f;

			//root_gameobject
			this.root_gameobject = a_root_gameobject;
		}
	}
}

