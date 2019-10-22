﻿namespace SportsBetting.Feeder.Core.Providers.Matches
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using HtmlAgilityPack;

    using SportsBetting.Common.XPaths;
    using SportsBetting.Feeder.Core.Contracts.Providers;
    using SportsBetting.Feeder.Core.Factories;
    using SportsBetting.Feeder.Models;

    public class MatchesProvider : IMatchesProvider
    {
        private readonly ITeamsProvider teamsProvider;
        private readonly IMarketsProvider marketsProvider;
        private readonly ITournametsProvider tournametsProvider;

        public MatchesProvider(ITeamsProvider teamsProvider, IMarketsProvider marketsProvider, ITournametsProvider tournametsProvider)
        {
            this.teamsProvider = teamsProvider;
            this.marketsProvider = marketsProvider;
            this.tournametsProvider = tournametsProvider;
        }

        public MatchFeedModel Get(HtmlNode matchContainer)
        {
            HtmlNode matchInfo = matchContainer?.SelectSingleNode(MatchXPaths.HEADER_INFO_BOX);
            DateTime startTime = GetStartTime(matchInfo);

            IEnumerable<TeamFeedModel> teams = teamsProvider.Get(matchContainer);
            TournamentFeedModel tournament = tournametsProvider.Get(matchInfo);

            MatchFeedModel match = ObjectFactory.CreateMatch(startTime, teams.First(), teams.Last(), tournament);
            match.Markets = marketsProvider.Get(matchContainer, match);

            return match;
        }

        private DateTime GetStartTime(HtmlNode matchInfo)
        {
            string startTime = matchInfo.LastChild.InnerText;

            startTime = startTime.Replace(", ", $" { DateTime.Now.Year } ");
            startTime = startTime.Replace(" +", ":00 +");

            DateTime gameTime = DateTime.ParseExact(startTime, "dd MMM yyyy HH:mm:ss zzz", CultureInfo.InvariantCulture);

            return gameTime;
        }
    }
}