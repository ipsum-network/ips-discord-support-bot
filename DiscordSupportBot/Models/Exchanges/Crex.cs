using System;
using System.Collections.Generic;
using System.Globalization;
using DiscordSupportBot.Models.BaseModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


/*
{
"Error": null,
"Tickers": [
{
"PairId": 422,
"PairName": "BTC_IPS",
"Last": 0.000004000000000,
"LowPrice": 0.000002910000000,
"HighPrice": 0.000011500000000,
"PercentChange": -60.0400,
"BaseVolume": 0.3147625627072927079200000000,
"QuoteVolume": 36001.842157842158000,
"VolumeInBtc": 0.3147625627072927079200000000,
"VolumeInUsd": 2208.3720251439295531455135796
}
]
}
*/
namespace DiscordSupportBot.Models.Exchanges.Crex
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