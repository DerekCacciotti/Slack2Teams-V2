using System.ComponentModel.DataAnnotations;

namespace Slack2Teams.Shared.Models.Responses.SlackResponses;

public class SlackMessageResponse
{
    public bool ok { get; set; }
    public List<SlackMessage> messages { get; set; }
    public bool has_more { get; set; }
    public int pin_count { get; set; }
    public object channel_actions_ts { get; set; }
    public int channel_actions_count { get; set; }
    public string channelid { get; set; }
    public ResponseMetadata response_metadata { get; set; }
}
public class SlackFile
    {
        public string id { get; set; }
        public string created { get; set; }
        public string timestamp { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string mimetype { get; set; }
        public string filetype { get; set; }
        public string pretty_type { get; set; }
        public string user { get; set; }
        public string user_team { get; set; }
        public bool editable { get; set; }
        public int size { get; set; }
        public string mode { get; set; }
        public bool is_external { get; set; }
        public string external_type { get; set; }
        public bool is_public { get; set; }
        public bool public_url_shared { get; set; }
        public bool display_as_bot { get; set; }
        public string username { get; set; }
        public string url_private { get; set; }
        public string url_private_download { get; set; }
        
    }

public class SlackMessage
{
    public string? subtype { get; set; }
    public string text { get; set; }
    //public string username { get; set; }
    public string type { get; set; }
    public string ts { get; set; }
    public string? bot_id { get; set; }
    //public List<Block>? blocks { get; set; }
    public List<SlackFile>? files { get; set; }
    public bool? upload { get; set; }
}
public class Block
{
    public string type { get; set; }
    public string block_id { get; set; }
    public List<Element> elements { get; set; }
}

public class Element
{
    public string type { get; set; }
    public List<Element> elements { get; set; }
    public string text { get; set; }
    public string user_id { get; set; }
    public string url { get; set; }
    public bool? @unsafe { get; set; }
}


