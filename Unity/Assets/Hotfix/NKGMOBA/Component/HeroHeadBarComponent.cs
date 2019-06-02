//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年5月31日 14:21:00
//------------------------------------------------------------

using ETModel;
using FairyGUI;
using UnityEngine;

namespace ETHotfix
{
    [ObjectSystem]
    public class HeroHeadBarComponentAwakeSystem: AwakeSystem<HeroHeadBarComponent, Unit, FUI>
    {
        public override void Awake(HeroHeadBarComponent self, Unit m_Hero, FUI m_HeadBar)
        {
            self.Awake(m_Hero, m_HeadBar);
        }
    }

    [ObjectSystem]
    public class HeroHeadBarComponentUpdateSystem: UpdateSystem<HeroHeadBarComponent>
    {
        public override void Update(HeroHeadBarComponent self)
        {
            self.Update();
        }
    }

    public class HeroHeadBarComponent: Component
    {
        private Unit m_Hero;
        private FUI m_HeadBar;
        private Vector2 m_Hero2Screen;
        private Vector2 m_HeadBarScreenPos;

        public void Awake(Unit hero, FUI headBar)
        {
            this.m_Hero = hero;
            this.m_HeadBar = headBar;
        }

        public void Update()
        {
            // 游戏物体的世界坐标转屏幕坐标
            this.m_Hero2Screen =
                    Camera.main.WorldToScreenPoint(new Vector3(m_Hero.Position.x, this.m_Hero.Position.y, this.m_Hero.Position.z));

            Log.Info($"人物坐标为{this.m_Hero.Position}屏幕坐标为{this.m_Hero2Screen}");

            // 屏幕坐标转FGUI全局坐标
            this.m_HeadBarScreenPos.x = m_Hero2Screen.x;
            this.m_HeadBarScreenPos.y = Screen.height - m_Hero2Screen.y;

            // FGUI全局坐标转头顶血条本地坐标
            this.m_HeadBar.GObject.position = GRoot.inst.GlobalToLocal(m_HeadBarScreenPos);

            // 血条本地坐标修正
            this.m_HeadBar.GObject.x -= 100f;
            this.m_HeadBar.GObject.y -= 180f;
        }
    }
}