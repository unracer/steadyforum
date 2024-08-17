<template @mouseover="pageOnfocus = true" @mouseleave="pageOnfocus = false">

    <!-- верхняя 50% карусель
        
        нижняя 50% предпочтения (дешевле/дороже, важность камеры, важность процессора, батареи и тд..)

        алгоритм поиска по магазам типа е каталог
        -->

        <div style="height: 45%; height: 400px; margin: 1em; overflow: hidden;">
            <div class="shopcarousel">
                <router-link :to="{ name: 'shop', params: { name: shopunit.id }}" @onclick="startFastPaymentSystem" v-for="shopunit in shopcarousellist" :key="shopunit.id"><img src="../assets/logo.svg" alt="unit carousel" /><p class="shopcarouselunit">{{shopunit.title}}</p></router-link>
            </div>
        </div>

        <div class="productsearch">
            <form class="form"  ref="msgFormRef" v-on:submit.prevent="onSubmit">
                <input type="text" class="unitsearchinput" v-model="sentmessageinput" value="oppo realme ram 8gb cam 120mpx " placeholder="searching .."/>
                <input type="submit" class="unitsearchinput" @click="msgSend" style="display: none" />
                <a class="cursorimage">></a>
                <label class="cursor"></label>
            </form>
        </div>

        <div class="productpredict">
            <div class="predictTile" v-for="hintunit in productpredictlist" :key="hintunit.title">{{hintunit.title}}</div>
        </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';

    export default defineComponent({
        data(): Data {
            return {
                loading: false,
                shopcarousellist: [
                    { id: 0, title: "steady Merch steack..", image: "../assets/logo.svg" },
                    { id: 0, title: "steady Merch hoodie ..", image: "../assets/logo.svg" },
                    { id: 0, title: "steady Merch quadrocopter pack ..", image: "../assets/logo.svg" },
                    { id: 0, title: "steady Merch hardware pack ..", image: "../assets/logo.svg" },
                    { id: 0, title: "steady Merch software pack ..", image: "../assets/logo.svg" },
                    { id: 0, title: "steady Merch phone ..", image: "../assets/logo.svg" },
                ],
                productpredictlist: [
                    { title: "Predict ..", link: "wait .." },
                    { title: "cat ..", link: "wait .." },
                    { title: "road ..", link: "wait .." },
                    { title: "xss ..", link: "wait .." },
                    { title: "amazing ..", link: "wait .." },
                    { title: "steady ..", link: "wait .." },
                    { title: "small ..", link: "wait .." },
                    { title: "red ..", link: "wait .." },
                    { title: "Predict ..", link: "wait .." },
                    { title: "cat ..", link: "wait .." },
                    { title: "road ..", link: "wait .." },
                    { title: "xss ..", link: "wait .." },
                    { title: "amazing ..", link: "wait .." },
                    { title: "steady ..", link: "wait .." },
                    { title: "small ..", link: "wait .." },
                    { title: "red ..", link: "wait .." },
                    { title: "Predict ..", link: "wait .." },
                    { title: "cat ..", link: "wait .." },
                    { title: "road ..", link: "wait .." },
                    { title: "xss ..", link: "wait .." },
                    { title: "amazing ..", link: "wait .." },
                    { title: "steady ..", link: "wait .." },
                    { title: "small ..", link: "wait .." },
                    { title: "red ..", link: "wait .." },
                ],
                chatcreatebutton: false,
                sentmessageinput: null,
            };
        },
        created() {
            // fetch the data when the view is created and the data is
            // already being observed
            /*this.fetchData();*/
        },
        watch: {
            // call again the method if the route changes
            /*'$route': 'fetchData'*/
        },
        methods: {
            async getProducts() {
                session = null /*from cookie*/
                chatname = null /*from param*/
                const requestOptions = {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify(
                        {
                            id: 0,
                            session: session,
                            chatname: chatname,
                        }
                    )
                };

                await fetch('/chats/' + requestOptions)
                    .then(r => r.json())
                    .then(json => {
                        /* this.chatlist = json as Forecasts;*/
                        this.chatlist = json;
                        this.loading = false;
                        return;
                    });
            },

            login() {
                // test session expired
                // get new session id
                // set cookie
            },
            predictWords() {

            },
            userinput() {


            },
            startFastPaymentSystem(id) {
                /*rus payment system https://www.google.com/url?sa=t&source=web&rct=j&opi=89978449&url=https://metib.online/docs/%25D0%259A%25D0%25B0%25D0%25BA%2520%25D1%2583%25D1%2581%25D1%2582%25D0%25B0%25D0%25BD%25D0%25BE%25D0%25B2%25D0%25B8%25D1%2582%25D1%258C%2520%25D0%25B2%25D0%25B8%25D0%25B4%25D0%25B6%25D0%25B5%25D1%2582%2520%25D0%25A1%25D0%2591%25D0%259F%2520%25D0%25BD%25D0%25B0%2520%25D1%2581%25D0%25B0%25D0%25B9%25D1%2582%2520%25D0%25A2%25D0%25A1%25D0%259F.pdf*/
                const baseUrl = " ht https://sbp1.metib.ru ";
                /*https://qr.nspk.ru/AS100001ORTF4GAF80KPJ53K186D9A3G?type=01&bank=100000000007&crc=0C8A*/
                /*eu payment system https://qna.habr.com/q/289047*/
                var elements = stripe.elements({
                    mode: 'payment',
                    currency: 'usd',
                    amount: 1099,
                });
                /*anon payment system https://habr.com/ru/articles/350430/*/
            }
        },
    });
</script>

<style >
    /*carousel*/
    * {
        -ms-overflow-style: none;
        scrollbar-width: none;
    }

    .shopcarouselunit {
        width: 100%;
        height: 100%;
        object-fit: cover;
    }

    .shopcarousel {
        width: 400px;
        height: 100vw;
        transform: rotate(-90deg) translateY(-400px);
        transform-origin: top right;
        overflow-x: hidden;
        overflow-y: auto;
    }

        .shopcarousel div {
            padding: 1em;
            position: relative;
            width: 100%;
            height: 400px;
            transform: rotate(90deg);
            margin: 10px 0;
        }

    .shopcarousel div:first-child {
        margin: 0;
    }

    .shopcarouselunit {
        padding: 10px;
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
    }



    /*input*/
    .unitsearchinput {
        background-color: #00000000;
        width: 100%;
        border: none;
        position: absolute;
        color: white;
        font-size: 1.2em;
        margin-left: 1em;
        left: 0.6em;
    }
    .productsearch {
        padding: 1em;
        width: 100%;
    }

/*predict*/
    .productpredict {
        padding: 1em;
        height: 45%;
        width: 100%;
    }
    .predictTile {
        background-color: #5e5e5ed6;
        border-radius: 15px;
        display: inline-block;
        margin: 0.5em;
        padding: 0.5em;
    }

</style>

