//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年8月22日 21:43:58
//------------------------------------------------------------

using NPBehave;
using Action = System.Action;

namespace ETModel.TheDataContainsAction
{
    public class NP_ChangeBlackValue: NP_ClassForStoreAction
    {
        public Blackboard theBlackBoardWillBedo;

        public override Action GetActionToBeDone()
        {
            this.m_Action = this.TestChangeBlackBoard;
            return this.m_Action;
        }

        public void TestChangeBlackBoard()
        {
            if (this.theBlackBoardWillBedo == null)
            {
                theBlackBoardWillBedo = Game.Scene.GetComponent<UnitComponent>().Get(this.Unitid).GetComponent<NP_RuntimeTreeManager>()
                        .GetTree(this.RuntimeTreeID)
                        .GetBlackboard();
            }

            theBlackBoardWillBedo["foo"] = !theBlackBoardWillBedo.Get<bool>("foo");
            
            //Log.Info($"改变了黑板值:{theBlackBoardWillBedo["foo"] }");
        }
    }
}