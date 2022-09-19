

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief ＢＧＭ。バンク。
*/


/** BlueBack.Bgm
*/
namespace BlueBack.Bgm
{
	/** Bank
	*/
	#if(UNITY_EDITOR)
	[System.Serializable]
	#endif
	public sealed class Bank
	{
		/** Item
		*/
		#if(UNITY_EDITOR)
		[System.Serializable]
		#endif
		public struct Item
		{
			/** audioclip
			*/
			public UnityEngine.AudioClip audioclip;

			/** datavolume
			*/
			public float datavolume;
		}

		/** bankname
		*/
		public string bankname;

		/** list
		*/
		public Item[] list;
	}
}

