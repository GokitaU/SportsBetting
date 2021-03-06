﻿namespace SportsBetting.Feeder.Models
{
    using SportsBetting.Feeder.Models.Base;

    public class TournamentFeedModel : BaseFeedModel
    {
        public string Name { get; set; }

        protected override int GenerateKey()
        {
            return Name.GetHashCode();
        }
    }
}