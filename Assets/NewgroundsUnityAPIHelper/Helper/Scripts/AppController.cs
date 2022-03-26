using System;
using io.newgrounds;
using io.newgrounds.objects;

namespace NewgroundsUnityAPIHelper.Helper.Scripts
{
    internal class AppController
    {
        private readonly core _core;

        public AppController(core core)
        {
            _core = core;
        }

        public void GetSession(Action<session> onGetSession)
        {
            var request = new io.newgrounds.components.App.checkSession();
            request.callWith(_core, result => onGetSession(result.__session));
        }

        public void EndSession(Action onEndSession)
        {
            var request = new io.newgrounds.components.App.endSession();
            request.callWith(_core, result =>
            {
                onEndSession?.Invoke();
            });
        }

        public void StartSession(Action<session> onStartSession)
        {
            var request = new io.newgrounds.components.App.startSession();
            request.callWith(_core, result =>
            {
                onStartSession?.Invoke(result.__session);
            });
        }

        public void LogView(Action onLogView)
        {
            var request = new io.newgrounds.components.App.logView();
            request.callWith(_core, result =>
            {
                onLogView?.Invoke();
            });
        }

        public void GetLatestVersion(Action<string> onGetLatestVersion)
        {
            var request = new io.newgrounds.components.App.getCurrentVersion();
            request.callWith(_core, result =>
            {
                onGetLatestVersion(result.current_version);
            });
        }

        public void GetLatestVersion(Action<Version> onGetLatestVersion)
        {
            var request = new io.newgrounds.components.App.getCurrentVersion();
            request.callWith(_core, result =>
            {
                onGetLatestVersion(new Version(result.current_version));
            });
        }

        public void IsDeprecatedVersion(Action<bool> onIsDeprecatedVersion)
        {
            var request = new io.newgrounds.components.App.getCurrentVersion();
            request.callWith(_core, result =>
            {
                onIsDeprecatedVersion(result.client_deprecated);
            });
        }

        public void GetHostLicense(Action<bool> onGetHostLicense)
        {
            var request = new io.newgrounds.components.App.getHostLicense();
            request.callWith(_core, result =>
            {
                onGetHostLicense(result.host_approved);
            });
        }
    }
}