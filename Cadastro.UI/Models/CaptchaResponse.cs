using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cadastro.UI.Models
{
    public class CaptchaResponse
    {
        [JsonProperty("success")] public bool Success { get; set; }

        [JsonProperty("challenge_ts")] public DateTime ChallengeTimeStamp { get; set; }

        [JsonProperty("hostname")] public string Hostname { get; set; }

        [JsonProperty("error-codes")] public List<string> ErrorMessage { get; set; }
    }
}