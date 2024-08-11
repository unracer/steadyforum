<template @mouseover="pageOnfocus = true" @mouseleave="pageOnfocus = false">

    <!-- верхняя 50% карусель
        
        нижняя 50% предпочтения (дешевле/дороже, важность камеры, важность процессора, батареи и тд..)

        алгоритм поиска по магазам типа е каталог
        -->

        <div style="height: 45%; height: 400px; margin: 1em; overflow: hidden;">
            <div class="shopcarousel">
                <div><img src="../assets/logo.svg" alt="unit carousel" /><p class="shopcarouselunit"></p></div>
                <div><img src="../assets/logo.svg" alt="unit carousel" /><p class="shopcarouselunit"></p></div>
                <div><img src="../assets/logo.svg" alt="unit carousel" /><p class="shopcarouselunit"></p></div>
                <div><img src="../assets/logo.svg" alt="unit carousel" /><p class="shopcarouselunit"></p></div>
                <div><img src="../assets/logo.svg" alt="unit carousel" /><p class="shopcarouselunit"></p></div>
                <div><img src="../assets/logo.svg" alt="unit carousel" /><p class="shopcarouselunit"></p></div>
                <div><img src="../assets/logo.svg" alt="unit carousel" /><p class="shopcarouselunit"></p></div>
                <div><img src="../assets/logo.svg" alt="unit carousel" /><p class="shopcarouselunit"></p></div>
                <div><img src="../assets/logo.svg" alt="unit carousel" /><p class="shopcarouselunit"></p></div>
                <div><img src="../assets/logo.svg" alt="unit carousel" /><p class="shopcarouselunit"></p></div>
                <div><img src="../assets/logo.svg" alt="unit carousel" /><p class="shopcarouselunit"></p></div>
                <div><img src="../assets/logo.svg" alt="unit carousel" /><p class="shopcarouselunit"></p></div>
                <div><img src="../assets/logo.svg" alt="unit carousel" /><p class="shopcarouselunit"></p></div>
                <div><img src="../assets/logo.svg" alt="unit carousel" /><p class="shopcarouselunit"></p></div>
                <div><img src="../assets/logo.svg" alt="unit carousel" /><p class="shopcarouselunit"></p></div>
                <div><img src="../assets/logo.svg" alt="unit carousel" /><p class="shopcarouselunit"></p></div>
                <div><img src="../assets/logo.svg" alt="unit carousel" /><p class="shopcarouselunit"></p></div>
                <div><img src="../assets/logo.svg" alt="unit carousel" /><p class="shopcarouselunit"></p></div>

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
            <div class="predictTile">Predict:</div>
            <div class="predictTile">cat</div>
            <div class="predictTile">road bike</div>
            <div class="predictTile">xss</div>
            <div class="predictTile">amazing</div>
            <div class="predictTile">small</div>
            <div class="predictTile">red</div>
            <div class="predictTile">road bike</div>
            <div class="predictTile">xss</div>
            <div class="predictTile">amazing</div>
            <div class="predictTile">small</div>
            <div class="predictTile">red</div>
            <div class="predictTile">road bike</div>
            <div class="predictTile">xss</div>
            <div class="predictTile">amazing</div>
            <div class="predictTile">small</div>
            <div class="predictTile">red</div>
            <div class="predictTile">road bike</div>
            <div class="predictTile">xss</div>
            <div class="predictTile">amazing</div>
            <div class="predictTile">small</div>
            <div class="predictTile">red</div>
            <div class="predictTile">road bike</div>
            <div class="predictTile">xss</div>
            <div class="predictTile">amazing</div>
            <div class="predictTile">small</div>
            <div class="predictTile">red</div>
            <div class="predictTile">road bike</div>
            <div class="predictTile">xss</div>
            <div class="predictTile">amazing</div>
            <div class="predictTile">small</div>
            <div class="predictTile">red</div>
            <div class="predictTile">..</div>
        </div>
</template>

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

<script lang="ts">
    import { defineComponent } from 'vue';

    export default defineComponent({ 
        data(): Data {
            return {
                loading: false,
                chatlist: [
                    { name: "loading ..", image: "../assets/logo.svg" },
                    { name: "loading ..", image: "../assets/logo.svg" },
                    { name: "loading ..", image: "../assets/logo.svg" },
                    ],
                chatcontent: [
                    { uname: "loading ..", image: "../assets/logo.svg", message: "wait .." },
                    { uname: "loading ..", image: "../assets/logo.svg", message: "wait .." },
                    { uname: "loading ..", image: "../assets/logo.svg", message: "wait .." },
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
            async getProducts(){
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

                await fetch('/chats/'+ requestOptions)
                     .then(r => r.json())
                     .then(json => {
                        /* this.chatlist = json as Forecasts;*/
                         this.chatlist = json;
                         this.loading = false;
                         return;
                     });
            },
         
            login(){
                // test session expired
                // get new session id
                // set cookie
            },
            predictWords(){
            
            },
            userinput(){


            },           
        },
    });
</script>