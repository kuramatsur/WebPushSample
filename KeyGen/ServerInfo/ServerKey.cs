namespace Push.ServerInfo
{
    /// <summary>
    /// 鍵ペア情報
    /// </summary>
    public class ServerKey
    {
        public string? PublicKey { get; set; }
        public string? PrivateKey { get; set; }
        /// <summary>
        /// WebPushで使う情報
        /// 生成時は常にnullなので、後で任意の値を入れる
        /// </summary>
        public string? Subject { get; set; }

    }
}
