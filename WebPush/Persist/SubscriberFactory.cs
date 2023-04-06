using Push.Subscriber;

namespace Push.Persist
{
    /// <summary>
    /// サブスクライバ情報をJSON Linesファイルから読み込む
    /// </summary>
    public class SubscriberFactory
    {
        /// <summary>
        /// 読み込むファイル名
        /// </summary>
        string Filename;
        public SubscriberFactory(string filename)
        {
            Filename = filename;
        }


        /// <summary>
        /// サブスクライバ情報を取得する
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClientInfo> Create()
        {
            return File.ReadAllLines(Filename).Select(r => Create(r));
        }


        /// <summary>
        /// JSON Linesの一行からサブスクライバ情報を生成
        /// </summary>
        /// <param name="subscriber_json">JSON Line一行分</param>
        /// <returns></returns>
        public ClientInfo Create(string subscriber_json)
        {
            var jsonNode = System.Text.Json.Nodes.JsonNode.Parse(subscriber_json);
            var ci = new ClientInfo();

            ci.PushEndpoint = (string?)jsonNode["endpoint"];
            ci.Auth = (string?)jsonNode["keys"]["auth"];
            ci.P256dh = (string?)jsonNode["keys"]["p256dh"];

            return ci;
        }

    }
}
