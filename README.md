# WebPush

WebPush通知のサンプル

ブラウザで登録して、C#から通知する

### プロジェクト

#### KeyGen

公開鍵生成サンプル


#### WebPusher

通知実行サンプル


#### Registor

ブラウザ登録サンプルページ


### Usage

1. 鍵ペアを作成 

```
KeyGen > server_key.txt
```

2. 公開鍵をjavascriptに書き込む

1で出来たPublicKeyをRegistor/public/registor.jsのダミー値と置き換える

```
var pubkey = "xxxxxxxxxxxxxxxxxxxxx"; <-ダミー値 
```

3. ブラウザに登録

Registor/publicをhttpsでホスト(localhostのhttpでも可)

ホストされたindex.htmlをブラウザで表示

「登録」ボタンをクリックする

表示された 「json: 」以下の文字列を、ファイル subscribers.txt に保存

4. 通知実行

```
WebPusher server_key.txt subscribers.txt
```






