using System;
using System.Globalization;
using io.newgrounds;
using io.newgrounds.results.Gateway;

namespace NewgroundsUnityAPIHelper.Helper.Scripts
{
    internal class GatewayController
    {
        private readonly core _core;

        public GatewayController(core core)
        {
            _core = core;
        }

        public void Ping(Action<ping> onPing)
        {
            var request = new io.newgrounds.components.Gateway.ping();
            request.callWith(_core, onPing);
        }

        public void GetDateTime(Action<string> onGetDateTime)
        {
            var request = new io.newgrounds.components.Gateway.getDatetime();
            request.callWith(_core, result =>
            {
                onGetDateTime(result.datetime);
            });
        }

        public void GetDateTime(Action<DateTime> onGetDateTime)
        {
            var request = new io.newgrounds.components.Gateway.getDatetime();
            request.callWith(_core, result =>
            {
                onGetDateTime(DateTime.Parse("2010-08-20T15:00:00Z", null, DateTimeStyles.RoundtripKind));
            });
        }

        public void GetVersion(Action<string> onGetVersion)
        {
            var request = new io.newgrounds.components.Gateway.getVersion();
            request.callWith(_core, result =>
            {
                onGetVersion(result.version);
            });
        }

        public void GetVersion(Action<Version> onGetVersion)
        {
            var request = new io.newgrounds.components.Gateway.getVersion();
            request.callWith(_core, result =>
            {
                onGetVersion(new Version(result.version));
            });
        }
    }
}