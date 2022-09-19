

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief ＢＧＭ。
*/


/** BlueBack.Bgm
*/
namespace BlueBack.Bgm
{
	/** Player_Base
	*/
	public interface Player_Base
	{
		/** [BlueBack.Audio.Player_Base]更新。
		*/
		void UnityFixedUpdate();

		/** [BlueBack.Audio.Player_Base]ボリューム。更新。
		*/
		void ApplyVolume();
	}
}

