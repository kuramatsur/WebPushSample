namespace Push.Subscriber
{
    /// <summary>
    /// サブスクライバ情報
    /// </summary>
    /// <remarks></remarks>
    public class ClientInfo
    {
        public string? PushEndpoint { get; set; }
        public string? P256dh { get; set; }
        public string? Auth { get; set; }

        /// <summary>
        /// 送信済みフラグ
        /// </summary>
        /// <remarks>内部管理用</remarks>
        public bool Submitted { get; set; } = false;

    }
}
