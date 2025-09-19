using System;
using System.Collections.Generic;
using System.Linq;
using io.newgrounds;
using io.newgrounds.objects;
using io.newgrounds.results.Medal;

namespace NewgroundsUnityAPIHelper.Helper.Scripts
{
    internal class MedalController
    {
        private readonly core _core;
        private readonly List<medal> _allMedals;
        private bool _hasLoadedMedals;

        public MedalController(core core)
        {
            _core = core;
            _allMedals = new List<medal>();
            _hasLoadedMedals = false;
        }

        public void Unlock(int id)
        {
            Unlock(id, x=>{});
        }
        
        public void Unlock(int id, Action<unlock> onUnlock)
        {
            if (IsMedalUnlocked(id)) return;
                
            var request = new io.newgrounds.components.Medal.unlock { id = id };
            request.callWith(_core, result =>
            {
                _allMedals.Find(medal => medal.id == id).unlocked = true;
                onUnlock(result);
            });
        }

        public bool IsMedalUnlocked(int id) =>
            GetMedal(id).unlocked;

        public void LoadMedals(Action<List<medal>> onLoadMedals)
        {
            var request = new io.newgrounds.components.Medal.getList();
            request.callWith(_core, result =>
            {
                _hasLoadedMedals = true;
                _allMedals.AddRange(result.medals.Select(x => (medal)x).ToList());
                onLoadMedals?.Invoke(_allMedals);
            });
        }

        public List<medal> GetAllMedals() =>
            _hasLoadedMedals
                ? _allMedals
                : throw new Exception("You need to load medals before calling any get all medals");

        public medal GetMedal(int id) =>
            _allMedals.Any(x => x.id == id)
                ? _allMedals.First(x => x.id == id)
                : throw new Exception("Medal with that id hasn't been loaded. Try loading the medals before asking for medals. If the problem persist, make sure that the medal exists");
    }
}