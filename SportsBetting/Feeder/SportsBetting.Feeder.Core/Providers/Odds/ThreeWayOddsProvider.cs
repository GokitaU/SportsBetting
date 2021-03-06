﻿namespace SportsBetting.Feeder.Core.Providers.Odds
{
    using System.Collections.Generic;

    using HtmlAgilityPack;

    using SportsBetting.Common.XPaths;
    using SportsBetting.Feeder.Core.Contracts.Providers;
    using SportsBetting.Feeder.Core.Contracts.Services;
    using SportsBetting.Feeder.Core.Providers.Odds.Base;
    using SportsBetting.Feeder.Models;

    public class ThreeWayOddsProvider : BaseOddsProvider, IOddsProvider
    {
        private const int ODDS_COUNT = 3;

        private readonly IHtmlService htmlService;
        private readonly IOddsProvider oddsProvider;

        public ThreeWayOddsProvider(IHtmlService htmlService, IOddsProvider oddsProvider)
            : base(htmlService)
        {
            this.htmlService = htmlService;
            this.oddsProvider = oddsProvider;
        }

        public IEnumerable<OddFeedModel> Get(HtmlNode marketNode, IList<string> oddNames)
        {
            if (ShouldGet(marketNode, oddNames))
            {
                ICollection<OddFeedModel> odds = new List<OddFeedModel>();
                IList<HtmlNode> oddNodes = marketNode.SelectNodes(OddXPaths.NODE);

                for (int i = 0; i < oddNames.Count; i++)
                {
                    OddFeedModel odd = BuildOdd(oddNodes[i], oddNames[i], i);
                    odds.Add(odd);
                }

                return odds;
            }

            return oddsProvider.Get(marketNode, oddNames);
        }

        protected override bool ShouldGet(HtmlNode marketNode, IList<string> oddNames)
        {
            int oddsCount = htmlService.GetOddsCount(marketNode);
            bool isOddsCountValid = oddNames.Count == ODDS_COUNT && oddsCount == ODDS_COUNT;

            return isOddsCountValid && !htmlService.HasHeader(marketNode);
        }

        protected override bool IsSuspended(HtmlNode oddNode)
        {
            return htmlService.IsSuspended(oddNode?.FirstChild);
        }

        protected override decimal GetValue(HtmlNode oddNode)
        {
            decimal value = 0;
            decimal.TryParse(oddNode?.LastChild?.InnerText, out value);

            return value;
        }
    }
}