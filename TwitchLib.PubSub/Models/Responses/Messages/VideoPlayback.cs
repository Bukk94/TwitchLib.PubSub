﻿using Newtonsoft.Json.Linq;
using TwitchLib.PubSub.Enums;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
    /// <summary>
    /// VideoPlayback model constructor.
    /// Implements the <see cref="TwitchLib.PubSub.Models.Responses.Messages.MessageData" />
    /// </summary>
    /// <seealso cref="TwitchLib.PubSub.Models.Responses.Messages.MessageData" />
    /// <inheritdoc />
    public class VideoPlayback : MessageData
    {
        /// <summary>
        /// Video playback type
        /// </summary>
        /// <value>The type.</value>
        public VideoPlaybackType Type { get; protected set; }
        /// <summary>
        /// Server time stamp
        /// </summary>
        /// <value>The server time.</value>
        public string ServerTime { get; protected set; }
        /// <summary>
        /// Current delay (if one exists)
        /// </summary>
        /// <value>The play delay.</value>
        public int PlayDelay { get; protected set; }
        /// <summary>
        /// Viewer count
        /// </summary>
        /// <value>The viewers.</value>
        public int Viewers { get; protected set; }

        /// <summary>
        /// VideoPlayback constructor.
        /// </summary>
        /// <param name="jsonStr">The json string.</param>
        public VideoPlayback(string jsonStr)
        {
            JToken json = JObject.Parse(jsonStr);
            switch (json.SelectToken("type").ToString())
            {
                case "stream-up":
                    Type = VideoPlaybackType.StreamUp;
                    break;
                case "stream-down":
                    Type = VideoPlaybackType.StreamDown;
                    break;
                case "viewcount":
                    Type = VideoPlaybackType.ViewCount;
                    break;
            }
            ServerTime = json.SelectToken("server_time")?.ToString();
            switch (Type)
            {
                case VideoPlaybackType.StreamUp:
                    PlayDelay = int.Parse(json.SelectToken("play_delay").ToString());
                    break;
                case VideoPlaybackType.ViewCount:
                    Viewers = int.Parse(json.SelectToken("viewers").ToString());
                    break;
                case VideoPlaybackType.StreamDown:
                    break;
            }
        }
    }
}
