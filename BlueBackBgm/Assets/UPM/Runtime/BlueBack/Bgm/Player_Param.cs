

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief ＢＧＭ。プレイヤー。パラメータ。
*/


/** BlueBack.Bgm
*/
namespace BlueBack.Bgm
{
	/** Player_Param
	*/
	public sealed class Player_Param
	{
		/** gameobject
		*/
		public UnityEngine.GameObject gameobject;

		/** mode
		*/
		public Player_Mode mode;

		/** bgm
		*/
		public Bgm bgm;

		/** volume
		*/
		public float volume;

		/** crossfadeframe
		*/
		public int crossfadeframe;
		public int crossfadeframe_max;

		/** dataindex
		*/
		public int dataindex;
		public int dataindex_request;

		/** bank
		*/
		public Bank bank;
		public Bank bank_request;

		/** audiosourcelist
		*/
		public AudioSourceItem[] audiosourcelist;
	}
}

