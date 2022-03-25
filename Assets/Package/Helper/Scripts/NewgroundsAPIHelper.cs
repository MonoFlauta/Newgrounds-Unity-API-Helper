using System;
using System.Collections.Generic;
using System.Linq;
using io.newgrounds;
using io.newgrounds.objects;
using io.newgrounds.results.Medal;
using UnityEngine;

namespace Package.Helper.Scripts
{
    [RequireComponent(typeof(core))]
    public class NewgroundsAPIHelper : MonoBehaviour
    {
        public core core;
        
        private static NewgroundsAPIHelper _instance;

        /// <summary>
        /// Singleton reference to the Newgrounds API Helper.
        /// The prefab containing the information will be created automatically the first time being called.
        /// </summary>
        public static NewgroundsAPIHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Instantiate(Resources.Load<NewgroundsAPIHelper>("NewgroundsAPI"));
                    _instance.Init();
                    DontDestroyOnLoad(_instance.gameObject);
                }

                return _instance;
            }
        }

        private MedalController _medalController;
        private ScoreboardController _scoreboardController;
        private EventController _eventController;
        
        private void Init()
        {
            _medalController = new MedalController(core);
            _scoreboardController = new ScoreboardController(core);
            _eventController = new EventController(core);
        }

        /// <summary>
        /// Unlocks a medal with a specified ID
        /// </summary>
        /// <param name="id">ID of the medal</param>
        public void UnlockMedal(int id)
        {
            _medalController.Unlock(id);
        }

        /// <summary>
        /// Unlocks a medal with a specified ID
        /// </summary>
        /// <param name="id">ID of the medal</param>
        /// <param name="onUnlock">On unlock callback. This method is called only if the medal was actually unlocked or loaded for the first time.</param>
        public void UnlockMedal(int id, Action<unlock> onUnlock)
        {
            _medalController.Unlock(id, onUnlock);
        }
        
        /// <summary>
        /// Returns true if medal has been unlocked
        /// </summary>
        /// <param name="id">ID of the medal</param>
        /// <returns>True if medal has been unlocked</returns>
        public bool IsMedalUnlocked(int id) => 
            _medalController.IsMedalUnlocked(id);

        /// <summary>
        /// Loads all medals
        /// </summary>
        /// <param name="onLoadMedals">Optional on load medals callback</param>
        public void LoadMedals(Action<List<medal>>onLoadMedals = null)
        {
            _medalController.LoadMedals(onLoadMedals);
        }

        /// <summary>
        /// Gets all medals
        /// </summary>
        /// <returns>Complete list of medals</returns>
        public List<medal> GetAllMedals() => 
            _medalController.GetAllMedals();

        /// <summary>
        /// Gets all unlocked medals
        /// </summary>
        /// <returns>Complete list of unlocked medals</returns>
        public List<medal> GetAllUnlockedMedals() => 
            _medalController.GetAllMedals().Where(x => x.unlocked).ToList();

        /// <summary>
        /// Gets all locked medals
        /// </summary>
        /// <returns>Complete list of locked medals</returns>
        public List<medal> GetAllLockedMedals() =>
            _medalController.GetAllMedals().Where(x => !x.unlocked).ToList();

        /// <summary>
        /// Gets a medal
        /// </summary>
        /// <param name="id">ID of the medal</param>
        /// <returns>Medal with that id</returns>
        public medal GetMedal(int id) =>
            _medalController.GetMedal(id);

        /// <summary>
        /// Post a score to a board
        /// </summary>
        /// <param name="boardId">ID of the board</param>
        /// <param name="score">Score to post</param>
        /// <param name="onPosted">Optional on posted callback</param>
        public void PostScore(int boardId, int score, Action onPosted = null)
        {
            _scoreboardController.PostScore(boardId, score, onPosted);
        }

        /// <summary>
        /// Gets a scoreboard
        /// </summary>
        /// <param name="boardId">ID of the board</param>
        /// <param name="onGetScoreboard">On get scoreboard callback</param>
        public void GetScoreboard(int boardId, Action<scoreboard> onGetScoreboard)
        {
            _scoreboardController.GetScoreboard(boardId, onGetScoreboard);
        }
        
        /// <summary>
        /// Gets all the scoreboards
        /// </summary>
        /// <param name="onGetScoreboards">On get scoreboards callback</param>
        public void GetScoreboards(Action<List<scoreboard>> onGetScoreboards)
        {
            _scoreboardController.GetScoreboards(onGetScoreboards);
        }

        /// <summary>
        /// Gets global scores from a board
        /// </summary>
        /// <param name="boardId">ID of the board</param>
        /// <param name="onGetScores">On get scores callback</param>
        /// <param name="period">Period to use. Default is Current Day</param>
        /// <param name="limit">Limit of scores to bring. Default is 10</param>
        /// <param name="skip">Skip the first x amount of scores. Default is 0</param>
        /// <param name="filterTag">Optional tag to filter scores by</param>
        public void GetGlobalScores(
            int boardId, 
            Action<List<score>> onGetScores,
            ScoreboardPeriod period = ScoreboardPeriod.CurrentDay,
            int limit = 10,
            int skip = 0,
            string filterTag = null
            )
        {
            _scoreboardController.GetGlobalScores(boardId, onGetScores, period, limit, skip, filterTag);
        }
        
        /// <summary>
        /// Gets personal scores from a board
        /// </summary>
        /// <param name="boardId">ID of the board</param>
        /// <param name="onGetScores">On get scores callback</param>
        /// <param name="period">Period to use. Default is Current Day</param>
        /// <param name="limit">Limit of scores to bring. Default is 10</param>
        /// <param name="skip">Skip the first x amount of scores. Default is 0</param>
        /// <param name="filterTag">Optional tag to filter scores by</param>
        public void GetPersonalScores(
            int boardId, 
            Action<List<score>> onGetScores,
            ScoreboardPeriod period = ScoreboardPeriod.CurrentDay,
            int limit = 10,
            int skip = 0,
            string filterTag = null
        )
        {
            _scoreboardController.GetPersonalScores(boardId, onGetScores, period, limit, skip, filterTag);
        }
        
        /// <summary>
        /// Gets social scores from a board
        /// </summary>
        /// <param name="boardId">ID of the board</param>
        /// <param name="onGetScores">On get scores callback</param>
        /// <param name="period">Period to use. Default is Current Day</param>
        /// <param name="limit">Limit of scores to bring. Default is 10</param>
        /// <param name="skip">Skip the first x amount of scores. Default is 0</param>
        /// <param name="filterTag">Optional tag to filter scores by</param>
        public void GetSocialScores(
            int boardId, 
            Action<List<score>> onGetScores,
            ScoreboardPeriod period = ScoreboardPeriod.CurrentDay,
            int limit = 10,
            int skip = 0,
            string filterTag = null
        )
        {
            _scoreboardController.GetSocialScores(boardId, onGetScores, period, limit, skip, filterTag);
        }

        /// <summary>
        /// Logs an event
        /// </summary>
        /// <param name="eventName">Event name</param>
        /// <param name="onLogEvent">Optional on log event callback</param>
        public void LogEvent(string eventName, Action onLogEvent = null)
        {
            _eventController.LogEvent(eventName, onLogEvent);
        }
    }
}
