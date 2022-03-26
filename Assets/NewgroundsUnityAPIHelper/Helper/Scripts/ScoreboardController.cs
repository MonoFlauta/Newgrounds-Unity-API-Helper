using System;
using System.Collections.Generic;
using System.Linq;
using io.newgrounds;
using io.newgrounds.objects;

namespace NewgroundsUnityAPIHelper.Helper.Scripts
{
    internal class ScoreboardController
    {
        private readonly core _core;

        public ScoreboardController(core core)
        {
            _core = core;
        }

        public void PostScore(int boardId, int score, Action onPosted)
        {
            var request = new io.newgrounds.components.ScoreBoard.postScore { id = boardId, value = score };
            request.callWith(_core, _ => onPosted?.Invoke());
        }


        public void GetScoreboard(int boardId, Action<scoreboard> onGetScoreboard)
        {
            GetScoreboards(boards =>
            {
                if (boards.Any(x => x.id == boardId))
                    onGetScoreboard(boards.First(x => x.id == boardId));
                else
                    throw new Exception($"Board with id {boardId} couldn't  been found");
            });
        }

        public void GetScoreboards(Action<List<scoreboard>> onGetScoreboards)
        {
            var request = new io.newgrounds.components.ScoreBoard.getBoards();
            request.callWith(_core, result =>
            {
                var scoreboards = result.scoreboards.Select(x => (scoreboard)x).ToList();
                onGetScoreboards(scoreboards);
            });
        }

        public void GetGlobalScores(
            int boardId, 
            Action<List<score>> onGetScores,
            ScoreboardPeriod period = ScoreboardPeriod.CurrentDay,
            int limit = 10,
            int skip = 10,
            string tag = null)
        {
            GetScoreboard(boardId, scoreboard =>
            {
                scoreboard.getGlobalScores(
                    ScoreboardToStringKey(period),
                    limit,
                    skip,
                    tag,
                    result =>
                    {
                        var scores = result.scores.Select(x => (score)x).ToList();
                        onGetScores(scores);
                    });
            });
        }
        
        public void GetPersonalScores(
            int boardId, 
            Action<List<score>> onGetScores,
            ScoreboardPeriod period = ScoreboardPeriod.CurrentDay,
            int limit = 10,
            int skip = 10,
            string tag = null)
        {
            GetScoreboard(boardId, scoreboard =>
            {
                scoreboard.getPersonalScores(
                    ScoreboardToStringKey(period),
                    limit,
                    skip,
                    tag,
                    result =>
                    {
                        var scores = result.scores.Select(x => (score)x).ToList();
                        onGetScores(scores);
                    });
            });
        }
        
        public void GetSocialScores(
            int boardId, 
            Action<List<score>> onGetScores,
            ScoreboardPeriod period = ScoreboardPeriod.CurrentDay,
            int limit = 10,
            int skip = 10,
            string tag = null)
        {
            GetScoreboard(boardId, scoreboard =>
            {
                scoreboard.getSocialScores(
                    ScoreboardToStringKey(period),
                    limit,
                    skip,
                    tag,
                    result =>
                    {
                        var scores = result.scores.Select(x => (score)x).ToList();
                        onGetScores(scores);
                    });
            });
        }

        private static string ScoreboardToStringKey(ScoreboardPeriod period)
        {
            switch (period)
            {
                case ScoreboardPeriod.AllTime:
                    return scoreboard.PERIOD_ALL_TIME;
                case ScoreboardPeriod.CurrentDay:
                    return scoreboard.PERIOD_CURRENT_DAY;
                case ScoreboardPeriod.CurrentWeek:
                    return scoreboard.PERIOD_CURRENT_WEEK;
                case ScoreboardPeriod.CurrentYear:
                    return scoreboard.PERIOD_CURRENT_YEAR;
                case ScoreboardPeriod.CurrentMonth:
                    return scoreboard.PERIOD_CURRENT_MONTH;
                default:
                    throw new ArgumentOutOfRangeException(nameof(period), period, null);
            }
        }
    }
}