using HarmonyLib;

namespace NoDamageInShop;

internal static class ShopDamageGuard
{
    internal static bool IsShop()
    {
        return SemiFunc.RunIsShop() || (RunManager.instance != null && RunManager.instance.levelIsShop);
    }
}

[HarmonyPatch(typeof(PlayerHealth))]
internal static class PlayerHealthPatch
{
    [HarmonyPatch(nameof(PlayerHealth.Hurt))]
    [HarmonyPrefix]
    private static bool HurtPrefix()
    {
        return !ShopDamageGuard.IsShop();
    }

    [HarmonyPatch(nameof(PlayerHealth.HurtOther))]
    [HarmonyPrefix]
    private static bool HurtOtherPrefix()
    {
        return !ShopDamageGuard.IsShop();
    }
}

[HarmonyPatch(typeof(SemiFunc), nameof(SemiFunc.PlayerDamagingPlayer))]
internal static class SemiFuncPlayerDamagingPlayerPatch
{
    [HarmonyPrefix]
    private static bool Prefix()
    {
        return !ShopDamageGuard.IsShop();
    }
}

[HarmonyPatch(typeof(ShopKeeper), nameof(ShopKeeper.PlayerDamagedPlayer))]
internal static class ShopKeeperPlayerDamagedPlayerPatch
{
    [HarmonyPrefix]
    private static bool Prefix()
    {
        return !ShopDamageGuard.IsShop();
    }
}