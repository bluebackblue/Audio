

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief ＢＧＭ。プレイヤーパラメータ。
*/


/** BlueBack.Bgm
*/
namespace BlueBack.Bgm
{
	/** PlayerParam
	*/
	public sealed class PlayerParam
	{
		/** [cache]bgm
		*/
		public Bgm bgm;

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
		public PlayerParam(Bgm a_bgm,UnityEngine.GameObject a_root_gameobject)
		{
			//[cache]bgm
			this.bgm = a_bgm;

			//volume
			this.volume = 0.0f;

			//root_gameobject
			this.root_gameobject = a_root_gameobject;
		}
	}
}

