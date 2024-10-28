<template @mouseover="pageOnfocus = true" @mouseleave="pageOnfocus = false">

    <!-- верхняя 50% карусель
        
        нижняя 50% предпочтения (дешевле/дороже, важность камеры, важность процессора, батареи и тд..)

        алгоритм поиска по магазам типа е каталог

        е-каталог фареве
        -->

        <div style="height: 45%; height: 400px; margin: 1em; overflow: hidden; background: linear-gradient(to left, #4d4d4d7a 0%, transparent 10%);">
            <div class="shopcarousel">
                <!--<div class="shopcarouselunit"><img src="../assets/logo.svg" alt="unit carousel" /><p></p></div>-->
                <div class="shopcarouselunit" v-for="shopunit in shopcarousellist" :key="shopunit.id">
                    <img src="../assets/logo.svg" alt="unit carousel" />
                    <router-link :to="{ name: 'shop', params: { title: shopunit.title }}" @click="runPaymentSystem(shopunit.id)">
                        <a style="top: -4em; position: relative; padding: 1em;  color: coral; box-shadow: 0px 0px 100px 0px rgb(0 0 0); background-color: rgb(165 165 165 / 38%); }">{{shopunit.title}}</a>
                    </router-link>
                </div>
            </div>
        </div>

        <div class="productsearch">
            <form class="form"  ref="msgFormRef" v-on:submit.prevent="onSubmit">
                <input type="text" class="unitsearchinput" v-model="sentmessageinput" placeholder="searching .."/>
                <input type="submit" class="unitsearchinput" @click="msgSend" style="display: none" />
                <a class="cursorshop">></a>
                <label class="cursor"></label>
            </form>
        </div>

        <div class="productpredict">
            <div class="predictTile" v-for="hintunit in productpredictlist" :key="hintunit.title" @click="predictPast(hintunit.title)">{{hintunit.title}}</div>
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
                    { id: 1, title: "steady Merch hoodie ..", image: "../assets/logo.svg" },
                    { id: 2, title: "steady Merch quadrocopter pack ..", image: "../assets/logo.svg" },
                    { id: 3, title: "steady Merch hardware pack ..", image: "../assets/logo.svg" },
                    { id: 4, title: "steady Merch software pack ..", image: "../assets/logo.svg" },
                    { id: 5, title: "steady Merch phone ..", image: "../assets/logo.svg" },
                ],
                productpredictlist: [
                    { title: "Predict:", link: "wait .." },
                    { title: "cat", link: "wait .." },
                    { title: "road", link: "wait .." },
                    { title: "xss", link: "wait .." },
                    { title: "amazing", link: "wait .." },
                    { title: "steady", link: "wait .." },
                    { title: "small", link: "wait .." },
                    { title: "red", link: "wait .." },
                    { title: "Predict", link: "wait .." },
                    { title: "cat", link: "wait .." },
                    { title: "road", link: "wait .." },
                    { title: "xss", link: "wait .." },
                    { title: "amazing", link: "wait .." },
                    { title: "steady", link: "wait .." },
                    { title: "small", link: "wait .." },
                    { title: "red", link: "wait .." },
                    { title: "Predict", link: "wait .." },
                    { title: "cat", link: "wait .." },
                    { title: "road", link: "wait .." },
                    { title: "xss", link: "wait .." },
                    { title: "amazing", link: "wait .." },
                    { title: "steady", link: "wait .." },
                    { title: "small", link: "wait .." },
                    { title: "red", link: "wait .." },
                    { title: "Predict", link: "wait .." },
                    { title: "cat", link: "wait .." },
                    { title: "road", link: "wait .." },
                    { title: "xss", link: "wait .." },
                    { title: "amazing", link: "wait .." },
                    { title: "steady", link: "wait .." },
                    { title: "small", link: "wait .." },
                    { title: "red", link: "wait .." },
                    { title: "Predict", link: "wait .." },
                ],
                chatcreatebutton: false,
                sentmessageinput: "oppo realme ram 8gb cam 120mpx ",
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
            runPaymentSystem(id) {
                /*Payment acceptance service on the website with crypto-access*/
                // fetch my api which return url
                // redirect to this url
                alert("strict recomend use 5post or alternate post system")
            },
            predictPast(title){
                this.sentmessageinput += " " + title;
            },
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
        width: 800px;
        height: 100vw;
        transform: rotate(-90deg) translateY(-800px);
        transform-origin: top right;
        overflow-x: hidden;
        overflow-y: auto;
    }

        .shopcarousel div {
            padding: 1em;
            position: relative;
            width: 50%;
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
        /* top: 50%; */
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
    .cursorshop{
        color: coral;
    }



    /*predict*/
    .productpredict {
        padding: 1em;
        width: 100%;
        height: 12em;
        overflow: hidden;
    }
    .predictTile {
        background-color: #5e5e5ed6;
        border-radius: 15px;
        display: inline-block;
        margin: 0.5em;
        padding: 0.5em;
        color: coral;
    }

</style>

