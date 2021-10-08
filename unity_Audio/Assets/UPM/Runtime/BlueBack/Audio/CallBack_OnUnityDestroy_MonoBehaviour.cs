

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * @brief オーディオ。
*/


/** BlueBack.Audio
*/
namespace BlueBack.Audio
{
	/** CallBack_OnUnityDestroy_MonoBehaviour
	*/
	public class CallBack_OnUnityDestroy_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** instance
		*/
		public Audio instance;

		/** OnDestroy
		*/
		private void OnDestroy()
		{
			if(this.instance != null){
				this.instance.Dispose();
			}
		}
	}
}

