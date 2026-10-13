using FTLGame.Combat;
using GameMod;
using Gameplay.Effects;

namespace StarterMod
{
    public sealed class ModBehaviour : GameMod.ModBehaviour
    {
        protected override void OnAfterSetup()
        {
            // Register custom rules before any die face that refers to them.
            var ruleId = RuleId("opening-strike");
            RegisterCombatRule(new OpeningStrikeRule(ruleId),
                new ModLocalizedText("Opening strike")
                    .Add("zh-CN", "先发制人"));

            var balancedStone = new ModItemDefinition("balanced-stone", 1,
                    "Balanced Stone")
                .Localize("zh-CN", "均衡石")
                .AddPassive(new ModPassiveDefinition("All die faces gain +1.")
                    .Localize("zh-CN", "所有骰面点数 +1。")
                    .AddSideValue(1));
            RegisterContent(balancedStone);

            var hero = new ModUnitDefinition("apprentice", 8, "Apprentice",
                    "Deals extra damage to an unwounded target.")
                .Localize("zh-CN", "学徒", "对未受伤的目标造成额外伤害。")
                .AsHero(HeroArchetype.Orange, 2)
                .AddFace(Damage(2).WithRules("opening-strike"))
                .AddFace(new ModEffectDefinition(EffectType.Shield, 2,
                    TargetingType.Self, true))
                .AddFace(Damage(2).WithKeywords(Keyword.Ranged))
                .AddFace(new ModEffectDefinition(EffectType.Heal, 2,
                    TargetingType.Single, true))
                .AddFace(Damage(1).WithKeywords(Keyword.PoisonDamage))
                .AddFace(Damage(1), new ModEffectDefinition(EffectType.Shield, 1,
                    TargetingType.Self, true));
            RegisterContent(hero);

            // A second unit demonstrates enemy registration, a back-row flag and a
            // portable trait that affects only one of its six die faces.
            var lookout = new ModUnitDefinition("lookout", 6, "Lookout",
                    "A cautious enemy that fights from the back row.")
                .Localize("zh-CN", "哨兵", "在后排作战的谨慎敌人。")
                .AsEnemy()
                .SetBackRow()
                .AddTrait(new ModPassiveDefinition("Face 2 gains +1 point.")
                    .Localize("zh-CN", "第 2 面点数 +1。")
                    .AddSideValue(1, ModDieFace.Two))
                .AddFace(Damage(1))
                .AddFace(Damage(2))
                .AddFace(new ModEffectDefinition(EffectType.Shield, 1,
                    TargetingType.Self, true))
                .AddFace(Damage(1).WithKeywords(Keyword.Ranged))
                .AddFace(Damage(2))
                .AddFace(Damage(1));
            RegisterContent(lookout);
        }

        private static ModEffectDefinition Damage(int value) =>
            new ModEffectDefinition(EffectType.Damage, value,
                TargetingType.Single, false);
    }

    internal sealed class OpeningStrikeRule : CombatRuleExtension
    {
        public OpeningStrikeRule(string id) : base(id)
        {
        }

        public override int ModifyValue(CombatRuleExtensionContext context, int value) =>
            context.Target != null && context.Target.Health == context.Target.MaximumHealth
                ? value * 2
                : value;

        public override void ApplyToTarget(CombatRuleExtensionContext context)
        {
            context.Signal();
        }
    }
}
