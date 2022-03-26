using System;
using System.Collections.Generic;
using System.Linq;
using io.newgrounds;
using io.newgrounds.objects;
using io.newgrounds.results.Gateway;
using io.newgrounds.results.Medal;
using UnityEngine;

namespace NewgroundsUnityAPIHelper.Helper.Scripts
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
        private GatewayController _gatewayController;
        private LoaderController _loaderController;
        private AppController _appController;
        private PassportController _passportController;
        
        private void Init()
        {
            _medalController = new MedalController(core);
            _scoreboardController = new ScoreboardController(core);
            _eventController = new EventController(core);
            _gatewayController = new GatewayController(core);
            _loaderController = new LoaderController(core);
            _appController = new AppController(core);
            _passportController = new PassportController(core);
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

        /// <summary>
        /// Pings the Newgrounds server
        /// </summary>
        /// <param name="onPing">On ping callback</param>
        public void Ping(Action<ping> onPing)
        {
            _gatewayController.Ping(onPing);
        }

        /// <summary>
        /// Gets date time in string format
        /// </summary>
        /// <param name="onGetDateTime">On get date time callback</param>
        public void GetDateTime(Action<string> onGetDateTime)
        {
            _gatewayController.GetDateTime(onGetDateTime);
        }

        /// <summary>
        /// Gets date time in DateTime format
        /// </summary>
        /// <param name="onGetDateTime">On get date time callback</param>
        public void GetDateTime(Action<DateTime> onGetDateTime)
        {
            _gatewayController.GetDateTime(onGetDateTime);
        }

        /// <summary>
        /// Gets version in string format
        /// </summary>
        /// <param name="onGetVersion">On get version callback</param>
        public void GetVersion(Action<string> onGetVersion)
        {
            _gatewayController.GetVersion(onGetVersion);
        }

        /// <summary>
        /// Gets version in version format
        /// </summary>
        /// <param name="onGetVersion">On get version callback</param>
        public void GetVersion(Action<Version> onGetVersion)
        {
            _gatewayController.GetVersion(onGetVersion);
        }

        /// <summary>
        /// Loads the official URL of the app's author (as defined in your "Official URLs" settings), and logs a referral to your API stats. For apps with multiple author URLs, use Loader.loadReferral.
        /// </summary>
        /// <param name="openInNewTab">If should open in a new tab. Default is true</param>
        /// <param name="onOpenedUrl">Optional on opened url callback that returns the url</param>
        public void OpenAuthorUrl(bool openInNewTab = true, Action<string> onOpenedUrl = null)
        {
            _loaderController.OpenAuthorUrl(openInNewTab, onOpenedUrl);
        }
        
        /// <summary>
        /// Loads the Newgrounds game portal, and logs the referral to your API stats.
        /// </summary>
        /// <param name="openInNewTab">If should open in a new tab. Default is true</param>
        /// <param name="onOpenedUrl">Optional on opened url callback that returns the url</param>
        public void OpenMoreGamesUrl(bool openInNewTab = true, Action<string> onOpenedUrl = null)
        {
            _loaderController.OpenMoreGamesUrl(openInNewTab, onOpenedUrl);
        }
        
        /// <summary>
        /// Loads Newgrounds, and logs the referral to your API stats.
        /// </summary>
        /// <param name="openInNewTab">If should open in a new tab. Default is true</param>
        /// <param name="onOpenedUrl">Optional on opened url callback that returns the url</param>
        public void OpenNewgroundsUrl(bool openInNewTab = true, Action<string> onOpenedUrl = null)
        {
            _loaderController.OpenNewgroundsUrl(openInNewTab, onOpenedUrl);
        }
        
        /// <summary>
        /// Loads the official URL where the latest version of your app can be found (as defined in your "Official URLs" settings), and logs a referral to your API stats.
        /// </summary>
        /// <param name="openInNewTab">If should open in a new tab. Default is true</param>
        /// <param name="onOpenedUrl">Optional on opened url callback that returns the url</param>
        public void OpenOfficialUrl(bool openInNewTab = true, Action<string> onOpenedUrl = null)
        {
            _loaderController.OpenOfficialUrl(openInNewTab, onOpenedUrl);
        }
        
        /// <summary>
        /// Loads a custom referral URL (as defined in your "Referrals & Events" settings), and logs the referral to your API stats.
        /// </summary>
        /// <param name="openInNewTab">If should open in a new tab. Default is true</param>
        /// <param name="onOpenedUrl">Optional on opened url callback that returns the url</param>
        public void OpenReferralUrl(bool openInNewTab = true, Action<string> onOpenedUrl = null)
        {
            _loaderController.OpenReferralUrl(openInNewTab, onOpenedUrl);
        }

        /// <summary>
        /// Gets session
        /// </summary>
        /// <param name="onGetSession"></param>
        public void GetSession(Action<session> onGetSession)
        {
            _appController.GetSession(onGetSession);
        }

        /// <summary>
        /// Ends session
        /// </summary>
        /// <param name="onEndSession">Optional on end session callback</param>
        public void EndSession(Action onEndSession = null)
        {
            _appController.EndSession(onEndSession);
        }

        /// <summary>
        /// Starts a session
        /// </summary>
        /// <param name="onStartSession">Optional on start session callback that returns the session</param>
        public void StartSession(Action<session> onStartSession = null)
        {
            _appController.StartSession(onStartSession);
        }

        /// <summary>
        /// Increments "Total Views" statistic.
        /// </summary>
        /// /// <param name="onLogView">Optional on log view callback</param>
        public void LogView(Action onLogView = null)
        {
            _appController.LogView(onLogView);
        }

        
        /// <summary>
        /// Gets latest version in string format
        /// </summary>
        /// <param name="onGetLatestVersion">On get latest version callback</param>
        public void GetLatestVersion(Action<string> onGetLatestVersion)
        {
            _appController.GetLatestVersion(onGetLatestVersion);
        }

        /// <summary>
        /// Get latest version in version format
        /// </summary>
        /// <param name="onGetLatestVersion">On get latest version callback</param>
        public void GetLatestVersion(Action<Version> onGetLatestVersion)
        {
            _appController.GetLatestVersion(onGetLatestVersion);
        }

        /// <summary>
        /// Gets if it is a deprecated version
        /// </summary>
        /// <param name="onIsDeprecatedVersion">On is deprecated version callback</param>
        public void IsDeprecatedVersion(Action<bool> onIsDeprecatedVersion)
        {
            _appController.IsDeprecatedVersion(onIsDeprecatedVersion);
        }

        /// <summary>
        /// Checks a client-side host domain against domains defined in your "Game Protection" settings
        /// </summary>
        /// <param name="onGetHostLicense">On get host license callback that returns true if it is approved</param>
        public void GetHostLicense(Action<bool> onGetHostLicense)
        {
            _appController.GetHostLicense(onGetHostLicense);
        }

        /// <summary>
        /// Tells if the user is logged in
        /// </summary>
        /// <param name="onIsUserLoggedIn">On is user logged in callback that returns true if the user is logged in</param>
        public void IsUserLoggedIn(Action<bool> onIsUserLoggedIn)
        {
            _passportController.IsUserLoggedIn(onIsUserLoggedIn);
        }
    }
}
