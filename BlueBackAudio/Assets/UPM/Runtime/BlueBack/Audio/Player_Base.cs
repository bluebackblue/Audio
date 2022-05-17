

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief オーディオ。
*/


/** BlueBack.Audio
*/
namespace BlueBack.Audio
{
	/** Player_Base
	*/
	public interface Player_Base
	{
		/** [BlueBack.Audio.Player_Base]更新。
		*/
		void OnUnityFixedUpdate();

		/** [BlueBack.Audio.Player_Base]ボリューム。更新。
		*/
		void ApplyVolume();
	}
}

