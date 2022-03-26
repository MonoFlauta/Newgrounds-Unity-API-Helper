using System;
using io.newgrounds;

namespace NewgroundsUnityAPIHelper.Helper.Scripts
{
    internal class PassportController
    {
        private readonly core _core;

        public PassportController(core core)
        {
            _core = core;
        }

        public void IsUserLoggedIn(Action<bool> onIsUserLoggedIn)
        {
            _core.checkLogin(onIsUserLoggedIn);
        }
    }
}