using io.newgrounds;

namespace Package.Helper.Scripts
{
    internal class AppController
    {
        private readonly core _core;

        public AppController(core core)
        {
            _core = core;
        }

        public void Request()
        {
            var request = new io.newgrounds.components.App.checkSession();
            request.callWith(_core, session =>
            {
            });
        }
    }
}