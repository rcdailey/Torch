using System.Reflection;
using NLog;
using Sandbox.Engine.Networking;
using Torch.Managers.PatchManager;
using Torch.Utils;

namespace Torch.Patches
{
    [PatchShim]
    internal static class ModsDownloadingPatch
    {
        private static readonly Logger _log = LogManager.GetCurrentClassLogger();
#pragma warning disable 649
        [ReflectedMethodInfo(typeof(MyWorkshop), nameof(MyWorkshop.DownloadWorldModsBlocking))]
        private readonly static MethodInfo _downloadWorldModsBlockingMethod;
#pragma warning restore 649

        public static void Patch(PatchContext ctx)
        {
            _log.Info("Patching mods downloading");

            ctx.GetPattern(_downloadWorldModsBlockingMethod).Suffixes
                .Add(typeof(ModsDownloadingPatch).GetMethod(nameof(Postfix)));
        }
        public static void Postfix(MyWorkshop.ResultData __result)
        {
            if (__result.Success) return;
            _log.Warn("Missing Mods:");
            __result.MismatchMods?.ForEach(b => _log.Info($"\t{b}"));
        }
    }
}