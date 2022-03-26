using System;
using io.newgrounds;

namespace NewgroundsUnityAPIHelper.Helper.Scripts
{
    internal class LoaderController
    {
        private readonly core _core;

        public LoaderController(core core)
        {
            _core = core;
        }

        public void OpenAuthorUrl(bool openInNewTab, Action<string> onOpenedUrl)
        {
            var request = new io.newgrounds.components.Loader.loadAuthorUrl();
            request.openUrlWith(_core, openInNewTab, result =>
            {
                onOpenedUrl?.Invoke(result.url);
            });
        }

        public void OpenMoreGamesUrl(bool openInNewTab, Action<string> onOpenedUrl)
        {
            var request = new io.newgrounds.components.Loader.loadMoreGames();
            request.openUrlWith(_core, openInNewTab, result =>
            {
                onOpenedUrl?.Invoke(result.url);
            });
        }
        
        public void OpenNewgroundsUrl(bool openInNewTab, Action<string> onOpenedUrl)
        {
            var request = new io.newgrounds.components.Loader.loadNewgrounds();
            request.openUrlWith(_core, openInNewTab, result =>
            {
                onOpenedUrl?.Invoke(result.url);
            });
        }
        
        public void OpenOfficialUrl(bool openInNewTab, Action<string> onOpenedUrl)
        {
            var request = new io.newgrounds.components.Loader.loadOfficialUrl();
            request.openUrlWith(_core, openInNewTab, result =>
            {
                onOpenedUrl?.Invoke(result.url);
            });
        }
        
        public void OpenReferralUrl(bool openInNewTab, Action<string> onOpenedUrl)
        {
            var request = new io.newgrounds.components.Loader.loadReferral();
            request.openUrlWith(_core, openInNewTab, result =>
            {
                onOpenedUrl?.Invoke(result.url);
            });
        }
    }
}