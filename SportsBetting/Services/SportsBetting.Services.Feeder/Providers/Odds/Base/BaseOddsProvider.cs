﻿namespace SportsBetting.Services.Feeder.Providers.Odds.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using HtmlAgilityPack;

    using SportsBetting.Common.XPaths;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Services.Feeder.Contracts.Services;
    using SportsBetting.Services.Feeder.Factories;

    public abstract class BaseOddsProvider
    {
        private readonly IHtmlService htmlService;

        public BaseOddsProvider(IHtmlService htmlService)
        {
            this.htmlService = htmlService;
        }

        protected OddFeedModel BuildOdd(HtmlNode oddNode, string name, int rank, int marketKey, string header = null)
        {
            decimal value = GetValue(oddNode);
            bool isSuspended = IsSuspended(oddNode);
            OddResultFeedStatus resultStatus = htmlService.GetOddResultStatus(oddNode);

            return ObjectFactory.CreateOdd(name, value, isSuspended, resultStatus, rank, marketKey, header);
        }

        protected virtual decimal GetValue(HtmlNode oddNode)
        {
            decimal parsedValue = 0;

            try
            {
                HtmlNodeCollection nodeCollection = oddNode.SelectNodes(OddXPaths.VALUE);

                if (nodeCollection != null)
                {
                    string value = nodeCollection.Select(x => x.InnerText).First();
                    decimal.TryParse(value, out parsedValue);
                }
            }
            catch (Exception ex)
            {
            }

            return parsedValue;
        }

        protected abstract bool ShouldGet(HtmlNode marketNode, IList<string> oddNames);

        protected abstract bool IsSuspended(HtmlNode oddNode);
    }
}