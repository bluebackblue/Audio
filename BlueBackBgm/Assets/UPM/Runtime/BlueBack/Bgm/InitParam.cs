

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief 初期化パラメータ。
*/


/** BlueBack.Bgm
*/
namespace BlueBack.Bgm
{
	/** InitParam
	*/
	public struct InitParam
	{
		/** volume
		*/
		public float volume;

		/** player_default_crossfadeframe_max
		*/
		public int player_default_crossfadeframe_max;

		/** player_default_volume
		*/
		public float player_default_volume;

		/** CreateDefault
		*/
		public static InitParam CreateDefault()
		{
			return new InitParam(){
				volume = 1.0f,
				player_default_crossfadeframe_max = 10,
				player_default_volume = 1.0f
			};
		}
	}
}

