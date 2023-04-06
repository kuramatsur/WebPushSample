//鍵ペアを生成して、JSON Line形式でコンソールに出力

// See https://aka.ms/new-console-template for more information
using Push.ServerInfo;
using System.Text.Json;
using WebPush;

VapidDetails vapidKeys = VapidHelper.GenerateVapidKeys();

var sk = new ServerKey();
sk.PublicKey = vapidKeys.PublicKey;
sk.PrivateKey = vapidKeys.PrivateKey;

string serialized = JsonSerializer.Serialize(sk);

Console.WriteLine(serialized);