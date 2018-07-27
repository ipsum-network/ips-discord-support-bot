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
        public decimal CrexLast { get; set; }

        [JsonProperty("LowPrice")]
        public decimal CrexLowPrice { get; set; }

        [JsonProperty("HighPrice")]
        public decimal CrexHighPrice { get; set; }

        [JsonProperty("PercentChange")]
        public decimal CrexPercentChange { get; set; }

        [JsonProperty("BaseVolume")]
        public decimal CrexBaseVolume { get; set; }

        [JsonProperty("QuoteVolume")]
        public decimal CrexQuoteVolume { get; set; }

        [JsonProperty("VolumeInBtc")]
        public decimal CrexVolumeInBtc { get; set; }

        [JsonProperty("VolumeInUsd")]
        public decimal CrexVolumeInUsd { get; set; }
    }
}