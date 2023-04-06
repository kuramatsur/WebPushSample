using Push.PushInfo;
using Push.ServerInfo;
using Push.Subscriber;
using System.Text.Json;
using WebPush;

namespace Push
{

    /// <summary>
    /// WebPush送信
    /// </summary>
    public class WebPusher
    {
        /// <summary>
        /// サーバー公開鍵情報
        /// </summary>
        ServerKey ServerKey;

        public WebPusher(ServerKey server_key)
        {
            ServerKey = server_key;
        }

        /// <summary>
        /// 送信する
        /// </summary>
        /// <param name="subsceribers">送信対象</param>
        public async Task Invoke(IEnumerable<ClientInfo> subsceribers)
        {
            var webPushClient = new WebPushClient();

            foreach (var pci in subsceribers)
            {
                var subscription = new PushSubscription(pci.PushEndpoint, pci.P256dh, pci.Auth);
                var vapidDetails = new VapidDetails(ServerKey.Subject, ServerKey.PublicKey, ServerKey.PrivateKey);

                //通知内容
                var opt = new Options() { body = "通知" };
                var payload = new Payload() { title = "test", options = opt };

                string jsonString = JsonSerializer.Serialize(payload);
                try
                {
                    Console.WriteLine(jsonString);
                    await webPushClient.SendNotificationAsync(subscription, jsonString, vapidDetails);
                    pci.Submitted = true;
                }
                catch (WebPushException exception)
                {
                    Console.Error.WriteLine("Error: " + exception.Message);
                    Console.Error.WriteLine("Http STATUS code: " + exception.StatusCode);
                }

            }
        }
    }

}