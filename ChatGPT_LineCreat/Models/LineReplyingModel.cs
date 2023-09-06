namespace ChatGPT_LineCreat.Models
{

    public class WebhookRequestBodyDto
    {
        public string? Destination { get; set; }
        public List<WebhookEventDto> Events { get; set; }
    }

    public class WebhookEventDto
    {
        // -------- 以下 common property --------
        public string Type { get; set; } // 事件類型
        public string Mode { get; set; } // Channel state : active | standby
        public long Timestamp { get; set; } // 事件發生時間 : event occurred time in milliseconds
        public SourceDto Source { get; set; } // 事件來源 : user | group chat | multi-person chat
        public string WebhookEventId { get; set; } // webhook event id - ULID format
        public DeliverycontextDto DeliveryContext { get; set; } // 是否為重新傳送之事件 DeliveryContext.IsRedelivery : true | false
        
        public string? ReplyToken { get; set; } // 回覆此事件所使用的 token
        public MessageEventDto? Message { get; set; } // 收到訊息的事件，可收到 text、sticker、image、file、video、audio、location 訊息

        public UnsendEventDto? Unsend { get; set; } //使用者“收回”訊息事件
    }



    // -------- 以下 common property --------

    public class SourceDto
    {
        public string Type { get; set; }
        public string? UserId { get; set; }
        public string? GroupId { get; set; }
        public string? RoomId { get; set; }
    }

    public class DeliverycontextDto
    {
        public bool IsRedelivery { get; set; }

    }




    public class MessageEventDto
    {
        public string Id { get; set; }
        public string Type { get; set; }

        // Text Message Event
        public string? Text { get; set; }
        public List<TextMessageEventEmojiDto>? Emojis { get; set; }
        public TextMessageEventMentionDto? Mention { get; set; }

        ///Image (圖片)
        public ContentProviderDto? ContentProvider { get; set; }
        public ImageMessageEventImageSetDto? ImageSet { get; set; }

        /// 影片 or 音檔時長(單位：豪秒)
        public int? Duration { get; set; } 
    }

    public class TextMessageEventEmojiDto
    {
        public int Index { get; set; }
        public int Length { get; set; }
        public string ProductId { get; set; }
        public string EmojiId { get; set; }
    }

    public class TextMessageEventMentionDto
    {
        public List<TextMessageEventMentioneeDto> Mentionees { get; set; }
    }

    public class TextMessageEventMentioneeDto
    {
        public int Index { get; set; }
        public int Length { get; set; }
        public string UserId { get; set; }
    }


    ///Image (圖片)
    public class ContentProviderDto
    {
        public string Type { get; set; }
        public string? OriginalContentUrl { get; set; }
        public string? PreviewImageUrl { get; set; }
    }
    ///Image (圖片)
    public class ImageMessageEventImageSetDto
    {
        public string Id { get; set; }
        public string Index { get; set; }
        public string Total { get; set; }
    }

    public class UnsendEventObjectDto
    {
        public string messageId { get; set; }
    }

    // -------- 以下 unsend event --------
    public class UnsendEventDto
    {
        public string messageId { get; set; }
    }



    //public class LineReplyingModel
    //{
    //    public string platformid { get; set; }
    //    public string platformtype { get; set; }
    //    public string community_id { get; set; }
    //    public string message_type { get; set; }
    //    public string content { get; set; }
    //    public string attachmentid { get; set; }
    //    public object payload { get; set; }
    //    public string url { get; set; }
    //}

    //public class LineReplyingResultModel
    //{ 

    //}

}
