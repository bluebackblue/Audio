

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief ＢＧＭ。バンク。
*/


/** BlueBack.Bgm
*/
namespace BlueBack.Bgm
{
	/** Bank_Item
	*/
	#if(UNITY_EDITOR)
	[System.Serializable]
	#endif
	public struct Bank_Item
	{
		/** audioclip
		*/
		public UnityEngine.AudioClip audioclip;

		/** datavolume
		*/
		public float datavolume;
	}
}

