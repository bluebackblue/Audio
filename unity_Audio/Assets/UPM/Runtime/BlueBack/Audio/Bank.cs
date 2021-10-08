

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * @brief オーディオ。バンク。
*/


/** BlueBack.Audio
*/
namespace BlueBack.Audio
{
	/** Bank
	*/
	#if(UNITY_EDITOR)
	[System.Serializable]
	#endif
	public class Bank
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

