using System;
using System.Collections.Generic;
using System.Globalization;
using DiscordSupportBot.Models.BaseModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DiscordSupportBot.Models.Exchanges
{
    public partial class Crex
    {
        [JsonProperty("Error")]
        public object Error { get; set; }

        [JsonProperty("Tickers")]
        public Ticker[] Tickers { get; set; }

    }

    public class Ticker
    {
        [JsonProperty("PairId")]
        public int CrexPairId { get; set; }

        [JsonProperty("PairName")]
        public string CrexPairName { get; set; }

        [JsonProperty("Last")]
        public string CrexLast { get; set; }

        [JsonProperty("LowPrice")]
        public string CrexLowPrice { get; set; }

        [JsonProperty("HighPrice")]
        public string CrexHighPrice { get; set; }

        [JsonProperty("PercentChange")]
        public string CrexPercentChange { get; set; }

        [JsonProperty("BaseVolume")]
        public string CrexBaseVolume { get; set; }

        [JsonProperty("QuoteVolume")]
        public string CrexQuoteVolume { get; set; }

        [JsonProperty("VolumeInBtc")]
        public string CrexVolumeInBtc { get; set; }

        [JsonProperty("VolumeInUsd")]
        public string CrexVolumeInUsd { get; set; }
    }
}