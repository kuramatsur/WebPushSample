using Push.ServerInfo;

namespace Push.Persist
{
    /// <summary>
    /// サーバーキーをファイルから読み込む
    /// </summary>
    public class ServerKeyFactory
    {
        /// <summary>
        /// JSON Line形式の鍵ペア情報
        /// </summary>
        public string Filename;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename">読み込むファイル名</param>
        public ServerKeyFactory(string filename)
        {
            Filename = filename;
        }


        /// <summary>
        /// 鍵ペアをファイルから取得する
        /// </summary>
        /// <returns></returns>
        public ServerKey Create()
        {
            var json = File.ReadAllText(Filename);

            var jsonNode = System.Text.Json.Nodes.JsonNode.Parse(json);
            var sk = new ServerKey();
            sk.PublicKey = (string?)jsonNode["public"];
            sk.PrivateKey = (string?)jsonNode["private"];
            sk.Subject = (string?)jsonNode["subject"];

            return sk;
        }



    }
}
