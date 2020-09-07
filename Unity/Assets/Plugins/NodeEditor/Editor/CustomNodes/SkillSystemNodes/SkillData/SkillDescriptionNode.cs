//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年9月17日 19:24:42
//------------------------------------------------------------

using ETModel;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using Plugins.NodeEditor.Editor.Canvas;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins
{
    [Node(false, "技能数据部分/技能描述结点", typeof (NPBehaveCanvas))]
    public class SkillDescriptionNode: SkillNodeBase
    {
        [LabelText("技能描述数据")]
        public SkillDesNodeData mSkillDesNodeData;

        public override string GetID => Id;

        public const string Id = "技能描述结点";

        public override SkillBaseNodeData Skill_GetNodeData()
        {
            return this.mSkillDesNodeData;
        }

        public override void NodeGUI()
        {
            RTEditorGUI.TextField("技能描述结点");
        }
    }
}