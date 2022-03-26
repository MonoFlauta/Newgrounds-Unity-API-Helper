# Newgrounds Unity API Helper
The Newgrounds Unity API Helper is a wrapper that tries to be more friendly for developers that use mostly Unity when trying to use the [Newgrounds](https://www.newgrounds.com/) API.
## About the API
The official [Newgrounds API](https://www.newgrounds.io/) is included in the project inside the Package folder under the name of * *NewgroundsAPI* * .
It was originally made by [Josh Tuttle (a.k.a PsychoGoldfish)](https://psychogoldfish.newgrounds.com/) and his contribution was crucial for all developers that wanted to use the Newgrounds API in their web games made with Unity.
## About the helper
Even the API is already very useful as it is and you can do everything with it, I personally preferred to always create a wrapper in order to work more comfortable. The main goal of the wrapper is to simplify the use of the API and even add a few performance tricks.
Feel free to use it or ask for help if you need it by using the [Issues section](https://github.com/MonoFlauta/Newgrounds-Unity-API-Helper/issues).

# How to use it
The following list contains all the things you need to fully use the helper.
## Getting started
- In order to start, you will need to have the Package folder inside your project. Once you have it, you will be able to use it's functionality
- Go to the Packages/Helper/Resources/ folder and enter the NewgroundsAPI prefab
- Set the App ID inside the core component
That's all! Now you will be able to call the methods you need.
## Medals
### UnlockMedal
In order to unlock a medal, you can call the following anywhere but replacing the 1234 for the medal id:

`NewgroundsAPIHelper.Instance.UnlockMedal(1234);`

You can also add an action and get the unlock result by adding a callback:

`NewgroundsAPIHelper.Instance.UnlockMedal(1234, unlock => {
  //Do something
});`

### LoadMedals
In order to fetch information of medals, you will need, in most of cases, to load medals first. To do so, you can do the following:

`NewgroundsAPIHelper.Instance.LoadMedals();`

This information will be stored by the NewgroundsAPIHelper in order to be able to respond to other messages. It will also avoid requesting information over and over again avoiding to have unnecesary async calls.
Apart from this, you will also be able to get all the medals information in the same call by adding a callback method:

`NewgroundsAPIHelper.Instance.LoadMedals(medals => {
  //Do something
});`

Note that this method is only optional.

### IsMedalUnlocked
You can ask if a medal is unlocked or not. Unless the medal has been unlocked during the current player session, you will need to call LoadMedals method first and make sure that it has finished by using the callback for example.
To know if a medal was unlocked, replace the 1234 for the medal id:

`var isMedalUnlocked = NewgroundsAPIHelper.Instance.IsMedalUnlocked(1234);`

### GetAllMedals - GetAllUnlockedMedals - GetAllLockedMedals
You can get the whole list of medals. To do so, you will need to call LoadMedals method first and make sure that it has finished by using the callback for example.
To get the whole list of medals, call the following method:

`var allMedals = NewgroundsAPIHelper.Instance.GetAllMedals();`

You can also get filtered lists of medals like all unlocked medals:

`var allUnlockedMedals = NewgroundsAPIHelper.Instance.GetAllUnlockedMedals();`

and all locked medals:

`var allLockedMedals = NewgroundsAPIHelper.Instance.GetAllLockedMedals();`

### GetMedal
You can get information from a specific medal. Unless the medal has been unlocked during the current player session, you will need to call LoadMedals method first and make sure that it has finished by using the callback for example.
To get the information of a medal, replace the 1234 with the medal id and do the following:

var medal = NewgroundsAPIHelper.Instance.GetMedal(1234);

## Scoreboards
### PostScore
You can post scores to a specific board by doing:

`var boardId = 1234;
var score = 100;
NewgroundsAPIHelper.Instance.PostScore(boardId, score);`

If you want, you can also have a callback that let's you know once the score has been posted:

`var boardId = 1234;
var score = 100;
NewgroundsAPIHelper.Instance.PostScore(boardId, score, () =>{ 
  //Do something
});`

### GetScoreboard
You can get a scoreboard in order to read it's information. In order to do that, you can do the following:

`var boardId = 1234;
NewgroundsAPIHelper.Instance.GetScoreboard(boardId, scoreboard => {
  //Do something
});`

### GetScoreboards
You can also get all the scoreboards if you want by doing:

`NewgroundsAPIHelper.Instance.GetScoreboards(scoreboards => {
  //Do something
});`

### GetGlobalScores - GetPersonalScores - GetSocialScores
You can get the scores from a scoreboard and filter them by global, personal or social scores.
- Global includes all players who posted to that scoreboard
- Personal includes scores by the player only
- Social includes scores by the player and his friends

In order to do it you need to do:

`var boardId = 1234;
NewgroundsAPIHelper.Instance.GetGlobalScores(boardId, scores => {
  //Do something
});`

And you can add a bunch of optional parameters:

`var boardId = 1234;
var period = ScoreboardPeriod.CurrentDay;
var limit = 10;
var skip = 0;
string filterTag = null;
NewgroundsAPIHelper.Instance.GetGlobalScores(boardId, scores => {
  //Do something
},
period, limit, skip, filterTag);`

### LogEvent
You can log events by calling the following method with the event name:

`NewgroundsAPIHelper.Instance.LogEvent("event name");`

And you can optionally add a callback event to know if you want when the event has been logged:

`NewgroundsAPIHelper.Instance.LogEvent("event name", () => {
  //Do something
});`

# Note
This documentation is in WIP. Once it is completed this section will be removed.
