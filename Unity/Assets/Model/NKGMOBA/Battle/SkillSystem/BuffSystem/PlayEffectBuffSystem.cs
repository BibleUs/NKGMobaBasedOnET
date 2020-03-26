//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2020年1月24日 14:15:24
//------------------------------------------------------------

using UnityEngine;

namespace ETModel
{
    public class PlayEffectBuffSystem: BuffSystemBase
    {
        public override void OnInit(BuffDataBase BuffDataBase, Unit theUnitFrom, Unit theUnitBelongto)
        {
            //设置Buff来源Unit和归属Unit
            this.theUnitFrom = theUnitFrom;
            this.theUnitBelongto = theUnitBelongto;
            this.MSkillBuffDataBase = BuffDataBase;

            BuffTimerAndOverlayHelper.CalculateTimerAndOverlay(this, this.MSkillBuffDataBase);
        }

        public override void OnExecute()
        {
            PlayEffect();
            this.MBuffState = BuffState.Running;
        }

        public override void OnUpdate()
        {
            //只有不是永久Buff的情况下才会执行Update判断
            if (this.MSkillBuffDataBase.SustainTime + 1 > 0)
            {
                if (TimeHelper.Now() > this.MaxLimitTime)
                {
                    this.MBuffState = BuffState.Finished;
                }
            }
        }

        public override void OnFinished()
        {
            PlayEffectBuffData playEffectBuffData = MSkillBuffDataBase as PlayEffectBuffData;
            string targetEffectName = playEffectBuffData.EffectName;
            if (playEffectBuffData.CanChangeNameByCurrentOverlay)
            {
                targetEffectName = $"{playEffectBuffData.EffectName}{this.CurrentOverlay}";
            }

            theUnitBelongto.GetComponent<EffectComponent>()
                    .Remove(targetEffectName);
        }

        public override void OnRefresh()
        {
            PlayEffect();
            this.MBuffState = BuffState.Running;
        }

        void PlayEffect()
        {
            PlayEffectBuffData playEffectBuffData = MSkillBuffDataBase as PlayEffectBuffData;
            string targetEffectName = playEffectBuffData.EffectName;

            if (playEffectBuffData.CanChangeNameByCurrentOverlay)
            {
                targetEffectName = $"{playEffectBuffData.EffectName}{this.CurrentOverlay}";
                //Log.Info($"播放{targetEffectName}");
            }
            
            //如果想要播放的特效正在播放，就返回
            if (theUnitBelongto.GetComponent<EffectComponent>().CheckState(targetEffectName)) return;

            GameObjectPool<Unit> gameObjectPool = Game.Scene.GetComponent<GameObjectPool<Unit>>();

            if (!gameObjectPool.HasRegisteredPrefab(targetEffectName))
            {
                gameObjectPool.Add(targetEffectName,
                    this.theUnitFrom.GameObject.GetComponent<ReferenceCollector>()
                            .Get<GameObject>(targetEffectName));
            }

            Unit effectUnit = gameObjectPool.Fetch(targetEffectName);

            if (playEffectBuffData.FollowUnit)
            {
                if (playEffectBuffData.BuffTargetTypes == BuffTargetTypes.Self)
                {
                    effectUnit.GameObject.transform.SetParent(this.theUnitFrom.GetComponent<HeroTransformComponent>()
                            .GetTranform(playEffectBuffData.PosType));
                }
                else
                {
                    effectUnit.GameObject.transform.SetParent(this.theUnitBelongto.GetComponent<HeroTransformComponent>()
                            .GetTranform(playEffectBuffData.PosType));
                }

                effectUnit.GameObject.transform.localPosition = Vector3.zero;
            }

            theUnitBelongto.GetComponent<EffectComponent>()
                    .Play(targetEffectName, effectUnit);
        }
    }
}