﻿namespace SportsBetting.Feeder.Core.Providers.Markets
{
    using System.Collections.Generic;

    using HtmlAgilityPack;

    using SportsBetting.Common.XPaths;
    using SportsBetting.Feeder.Core.Contracts.Providers;
    using SportsBetting.Feeder.Core.Contracts.Services;
    using SportsBetting.Feeder.Core.Factories;
    using SportsBetting.Feeder.Models;

    public class MarketsProvider : IMarketsProvider
    {
        private readonly IHtmlService htmlService;
        private readonly IOddsProvider oddsProvider;

        public MarketsProvider(IHtmlService htmlService, IOddsProvider oddsProvider)
        {
            this.htmlService = htmlService;
            this.oddsProvider = oddsProvider;
        }

        public IEnumerable<MarketFeedModel> Get(HtmlNode matchContainer, MatchFeedModel match)
        {
            ICollection<MarketFeedModel> markets = new List<MarketFeedModel>();

            HtmlNodeCollection marketNodes = matchContainer.SelectNodes(MatchXPaths.MARKETS);

            foreach (var marketNode in marketNodes)
            {
                IList<string> oddNames = htmlService.GetOddNames(marketNode);

                MarketFeedModel market = ObjectFactory.CreateMarket(marketNode.FirstChild.FirstChild.InnerText, match.Key);
                market.Odds = oddsProvider.Get(marketNode, oddNames);

                markets.Add(market);
            }

            return markets;
        }
    }
}