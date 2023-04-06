if ("serviceWorker" in navigator) {
    var pubkey = "xxxxxxxxxxxxxxxxxxxxx";

    class subscription_manager {
        
        constructor(pubkey, registration) {
            this.pubkey = pubkey;
            this.registration = registration;
            this.subscription = null;
        }
      
        init(){
            this.registration = null;
            this.subscription = null;
        }

        async load() {
            this.subscription =  await this.registration?.pushManager?.getSubscription();

        }

        async remove(){
            await this.subscription?.unsubscribe();
            this.subscription = null;
        }

        async register(){
            this.subscription = await this.registration?.pushManager?.subscribe({
                userVisibleOnly: true,
                applicationServerKey: this.pubkey
            });
        }

        get_raw(){
            return this.subscription;
        }

        get_body(){
            return {
                "endpoint" : this.subscription?.endpoint,
                "auth" : btoa(String.fromCharCode.apply(null, new Uint8Array(this.subscription?.getKey('auth')))),
                "p256dh": btoa(String.fromCharCode.apply(null, new Uint8Array(this.subscription?.getKey('p256dh')))),
            };
        }

        is_valid(){
            return this.subscription && this.registration;
        }
    }

    


    var reg;
    var sbsc = new subscription_manager(pubkey);

   
    

    async function get_registoration(){
        var registration = await navigator.serviceWorker.getRegistration("./service-worker.js").catch(console.error);
        if ( !registration ){
            return null;
        }else{
            registration.update();
            console.log("service-worker.js updated.");
        }
        return registration;
    }

    async function create_registoration(){
        var registration = await navigator.serviceWorker
        .register("./service-worker.js")
        .catch(console.error);
        console.log("service-worker.js registered.");
        return;
    }

    async function remove_registration(registration){
        await registration?.unregister();
    }


    function show_subsc (push_subscription){
        window.document.querySelector('#go').textContent = push_subscription?.is_valid() ? "解除" : "登録";
        let subscription = push_subscription?.get_body();
        window.document.querySelector('#endpoint').textContent = subscription?.endpoint;
        window.document.querySelector('#auth').textContent = subscription?.auth
        window.document.querySelector('#p256dh').textContent = subscription?.p256dh;

        window.document.querySelector('#subscribe_json').textContent = JSON.stringify(push_subscription?.get_raw()?.toJSON());
    }

    async function subscribe_handler()  {
        if (sbsc.is_valid()) {
            //解除
            await sbsc.remove();
            sbsc.init();

            remove_registration(reg);
            reg = null;

            show_subsc(null);
        }else{
            //登録
            sbsc.registration = reg;
            sbsc.register();
            show_subsc(sbsc);
        }
    }


    window.addEventListener('load', async (e)=>{
        window.document.querySelector('#pubkey').value = pubkey;
        //regは常にある状態にする
        reg = await get_registoration();
        if (!reg){
            reg = await create_registoration();
        }
        sbsc.registration = reg;
        await sbsc.load();
        show_subsc(sbsc);

        window.document.querySelector('#go').addEventListener('click', subscribe_handler );
    });
    
}