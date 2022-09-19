

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
	/** DebugView_CustomEditor
	*/
	[UnityEditor.CustomEditor(typeof(Player_DebugView_MonoBehaviour),true)]
	public sealed class Player_DebugView_CustomEditor : UnityEditor.Editor
	{
		/** expanded
		*/
		private System.Collections.Generic.Dictionary<string,bool> expanded;

		/** GetExpanded
		*/
		private bool GetExpanded(string a_address)
		{
			if(this.expanded == null){
				this.expanded = new System.Collections.Generic.Dictionary<string,bool>();
			}

			if(this.expanded.ContainsKey(a_address) == true){
				return this.expanded[a_address];
			}else{
				this.expanded[a_address] = false;
				return false;
			}
		}

		/** SetExpanded
		*/
		private bool SetExpanded(string a_address,bool a_flag)
		{
			if(this.expanded == null){
				this.expanded = new System.Collections.Generic.Dictionary<string,bool>();
			}
			this.expanded[a_address] = a_flag;
			return a_flag;
		}

		/** OnInspectorGUI
		*/
		public override void OnInspectorGUI()
		{
			//OnInspectorGUI
			base.OnInspectorGUI();

			//this
			Player_DebugView_MonoBehaviour t_this = this.target as Player_DebugView_MonoBehaviour;

			//Space
			UnityEditor.EditorGUILayout.Space(18);

			//mode
			UnityEditor.EditorGUILayout.LabelField("mode",string.Format("{0}",t_this.player.param.mode));

			//dataindex
			UnityEditor.EditorGUILayout.LabelField("dataindex",string.Format("{0}",t_this.player.param.dataindex));

			//crossfadeframe
			UnityEditor.EditorGUILayout.LabelField("crossfadeframe",string.Format("{0}",t_this.player.param.crossfadeframe));
			UnityEditor.EditorGUILayout.LabelField("crossfadeframe_max",string.Format("{0}",t_this.player.param.crossfadeframe_max));
				
			//volume
			UnityEditor.EditorGUILayout.LabelField("volume",string.Format("{0}",t_this.player.param.volume));

			//bank
			{
				string t_name = "bank";
				string t_address = "bank";
				if(this.SetExpanded(t_address,UnityEditor.EditorGUILayout.Foldout(this.GetExpanded(t_address),t_name)) == true){
					UnityEditor.EditorGUI.indentLevel++;

					//bankname
					UnityEditor.EditorGUILayout.LabelField("bankname",string.Format("{0}",t_this.player.param.bank.bankname));

					int ii_max = t_this.player.param.bank.list.Length;
					for(int ii=0;ii<ii_max;ii++){
						//datavolume
						UnityEditor.EditorGUILayout.LabelField("datavolume",string.Format("{0} : {1}",ii,t_this.player.param.bank.list[ii].datavolume));
					}
					UnityEditor.EditorGUI.indentLevel--;
				}
			}

			//Repaint
			this.Repaint();
		}
	}
}
#endif

