namespace Slack2Teams.Shared.Models.Responses.SlackResponses;

public class SlackMessageResponse
{
    public bool ok { get; set; }
    public List<Message> messages { get; set; }
    public bool has_more { get; set; }
    public int pin_count { get; set; }
    public object channel_actions_ts { get; set; }
    public int channel_actions_count { get; set; }
    public string channelid { get; set; }
    public ResponseMetadata response_metadata { get; set; }
}
public class File
    {
        public string id { get; set; }
        public int created { get; set; }
        public int timestamp { get; set; }
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
        public string media_display_type { get; set; }
        public string thumb_64 { get; set; }
        public string thumb_80 { get; set; }
        public string thumb_360 { get; set; }
        public int thumb_360_w { get; set; }
        public int thumb_360_h { get; set; }
        public string thumb_480 { get; set; }
        public int thumb_480_w { get; set; }
        public int thumb_480_h { get; set; }
        public string thumb_160 { get; set; }
        public string thumb_360_gif { get; set; }
        public string thumb_480_gif { get; set; }
        public int image_exif_rotation { get; set; }
        public int original_w { get; set; }
        public int original_h { get; set; }
        public string deanimate { get; set; }
        public string deanimate_gif { get; set; }
        public string permalink { get; set; }
        public string permalink_public { get; set; }
        public bool is_starred { get; set; }
        public bool has_rich_preview { get; set; }
        public string file_access { get; set; }
        public string thumb_pdf { get; set; }
        public int? thumb_pdf_w { get; set; }
        public int? thumb_pdf_h { get; set; }
        public string edit_link { get; set; }
        public string preview { get; set; }
        public string preview_highlight { get; set; }
        public int? lines { get; set; }
        public int? lines_more { get; set; }
        public bool? preview_is_truncated { get; set; }
    }

public class Message
{
    public string subtype { get; set; }
    public string text { get; set; }
    public string username { get; set; }
    public string type { get; set; }
    public string ts { get; set; }
    public string bot_id { get; set; }
    public List<Block> blocks { get; set; }
    public List<File> files { get; set; }
    public bool? upload { get; set; }
    public string user { get; set; }
    public bool? display_as_bot { get; set; }
    public string team { get; set; }
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


