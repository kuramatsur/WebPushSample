//KeyGenで作って、サーバーで使用したキー
using Push.Persist;

var ski = new ServerKeyFactory(args[0]);
var server_key = ski.Create();

//配信先
var sif = new SubscriberFactory(args[1]);
var sbscrivers = sif.Create();

//配信
var push = new Push.WebPusher(server_key);
await push.Invoke(sbscrivers);
