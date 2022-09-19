

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief ＢＧＭ。プレイヤー。デバッグ表示。
*/


/** BlueBack.Bgm
*/
#if(DEF_BLUEBACK_BGM_DEBUGVIEW)
namespace BlueBack.Bgm
{
	/** Player_DebugView_MonoBehaviour
	*/
	public sealed class Player_DebugView_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** player
		*/
		public Player player;

		/** Create
		*/
		public static void Create(UnityEngine.GameObject a_parent,Player a_player)
		{
			Player_DebugView_MonoBehaviour t_debugview_monobehaviour = a_parent.AddComponent<Player_DebugView_MonoBehaviour>();
			t_debugview_monobehaviour.player = a_player;
		}
	}
}
#endif

