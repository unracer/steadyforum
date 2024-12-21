<!--
    + карусель товаров

    +/- кравлер по сайтам товаров

    + подсказки гугл для поиска

    + активные заказы

    + элемент заказа

    оплата

-->

<template @mouseover="pageOnfocus = true" @mouseleave="pageOnfocus = false">

    <div style="height: 45%; height: 400px; margin: 1em; overflow: hidden;">
        <div class="shopcarousel">
            <!--<div class="shopcarouselunit"><img src="../assets/logo.svg" alt="unit carousel" /><p></p></div>-->
            <div class="shopcarouselunit" v-for="shopunit in shopcarousellist" :key="shopunit.id">
                <img src="../assets/logo.svg" alt="unit carousel" />
                <router-link :to="{ name: 'shop', params: { title: shopunit.title } }" @click="runPaymentSystem(shopunit.id, null)">
                    <a style="top: -4em; position: relative; padding: 1em;  color: coral; box-shadow: 0px 0px 100px 0px rgb(0 0 0); background-color: rgb(165 165 165 / 38%); }">{{shopunit.title}}</a>
                </router-link>
            </div>
            <div class="attenuation"></div>
        </div>
    </div>

    <div class="productsearch">
        <form class="form" ref="msgFormRef" v-on:submit.prevent="onSubmit">
            <input type="text" class="searchTheme" v-model="sentmessageinput" placeholder="search unit" />
            <input type="submit" class="searchTheme" @click="msgSend" style="display: none" />
            <a class="cursorshop">></a>
            <label class="cursor"></label>
        </form>
    </div>

    <div class="productpredict">
        <div class="predictTile" v-for="hintunit in productpredictlist" :key="hintunit.title" @click="predictPast(hintunit.title)">{{hintunit.title}}</div>
    </div>

    <div class="postorder" v-if="showOrder">
        <input v-model="postTo" type="text" placeholder="post unit id" />
        <input v-model="postSize" type="radio" placeholder="post unit id" />
        order will expired for 2 minute without paid
        <img src="../assets/5post-size-manual.png" alt="post size" />
        <button @click="runPaymentSystem(null,null)">paid crypto wallet only</button>
    </div>
        
    <div class="postDelivery">
        <div class="ordersearch">
            <form class="form" ref="msgFormRef" v-on:submit.prevent="onSubmit">
                <input type="text" class="searchTheme" v-model="deliverysearchinput" placeholder="serach order" />
                <input type="submit" class="searchTheme" @click="deliverySearch" style="display: none" />
                <a class="cursorshop">></a>
                <label class="cursor"></label>
            </form>
        </div>

        <div class="postDeliveryRow" v-for="post in deliverylist" :key="post.id">
            {{post.id}}&#160
            {{post.date}}&#160
            {{post.expired}}&#160
            {{post.status}}&#160
            <button @click="runPaymentSystem(null, paid)">paid</button>
            <button @click="runPaymentSystem(null, abort)">abort</button>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';

    export default defineComponent({
        data(): Data {
            return {
                loading: false,
                shopcarousellist: [
                    { id: 0, title: "hoodie..", image: "../assets/merch-1.jpg" },
                    { id: 1, title: "backpack ..", image: "../assets/merch-2.jpg" },
                    { id: 2, title: "drone ..", image: "../assets/merch-3-1.jpg" },
                    { id: 2, title: "drone ..", image: "../assets/merch-3-2.jpg" },
                    { id: 2, title: "drone ..", image: "../assets/merch-3-3.jpg" },
                    { id: 3, title: "lock decoder ..", image: "../assets/merch-5.jpg" },
                    { id: 4, title: "drone software ..", image: "../assets/merch-3.jpg" },
                    { id: 4, title: "team party ..", image: "../assets/merch-4.jpg" },
                ],
                productpredictlist: [
                    { title: "Predict:", link: "wait .." },
                    { title: "ddr5", link: "wait .." },
                    { title: "ssd m2", link: "wait .." },
                    { title: "usb4.0", link: "wait .." },
                    { title: "xss", link: "wait .." },
                    { title: "dread", link: "wait .." },
                    { title: "raid", link: "wait .." },
                    { title: "redteam", link: "wait .." },
                    { title: "Predict:", link: "wait .." },
                    { title: "ddr5", link: "wait .." },
                    { title: "ssd m2", link: "wait .." },
                    { title: "usb4.0", link: "wait .." },
                    { title: "xss", link: "wait .." },
                    { title: "dread", link: "wait .." },
                    { title: "raid", link: "wait .." },
                    { title: "redteam", link: "wait .." },
                ],
                deliverylist: [
                    { id: 0, date: "01/01/2001", expired: "02/01/2001", status: "created/paid/aborted"},
                    { id: 1, date: "01/01/2001", expired: "02/01/2001", status: "created/paid/aborted"},
                    { id: 2, date: "01/01/2001", expired: "02/01/2001", status: "created/paid/aborted"},
                    { id: 3, date: "01/01/2001", expired: "02/01/2001", status: "created/paid/aborted"},
                    { id: 4, date: "01/01/2001", expired: "02/01/2001", status: "created/paid/aborted"},
                    { id: 0, date: "01/01/2001", expired: "02/01/2001", status: "created/paid/aborted"},
                    { id: 1, date: "01/01/2001", expired: "02/01/2001", status: "created/paid/aborted"},
                    { id: 2, date: "01/01/2001", expired: "02/01/2001", status: "created/paid/aborted"},
                    { id: 3, date: "01/01/2001", expired: "02/01/2001", status: "created/paid/aborted"},
                    { id: 4, date: "01/01/2001", expired: "02/01/2001", status: "created/paid/aborted"},
                ],
                chatcreatebutton: false,
                sentmessageinput: null,
                showOrder: false,
                readtopay: false,
                deliverysearchinput: null,
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
            async runPaymentSystem(id, action) {
                if (id == null && action == null) {
                    /*just sent to database and our personal can sent using himself account of 5post*/

                    const requestOptions = {
                        method: "POST",
                        headers: { "Content-Type": "application/json" },
                        body: JSON.stringify(
                            {
                                //postFrom = null, /*backend hardcode store addres*/
                                //postAmount = null, /*backend hardcode by unit id*/
                                //postTo = this.postTo,
                                //postSize = this.postSize,
                                //unitId = this.unitId,
                                //postId = this.postId,
                                //expired = "120",
                                //status = "wait" /*wait/paid/send/take*/,
                            }
                        )
                    };

                    let postStatus = await fetch('/chats/' + requestOptions)
                        .then(r => r.json())
                        .then(json => {
                            /* this.chatlist = json as Forecasts;*/
                            this.chatlist = json;
                            this.loading = false;
                            return;
                        });

                    if (postStatus == 200) {
                        this.showOrder = false
                        /* i cant trust for this shit .. omg i mainless*/
                        /*let status = router.push({ path: 'bitcoin.com', query: { postid: this.postid, wallet: '3894573' } })*/
                    }

                }

                if (id != null && !showOrder) {
                    alert("* It is strongly recommended to use 5post or an alternative postal system\n* Using paper money or an nfc\n* 5post have 5% commission")
                    this.showOrder = true
                    this.unitId = id

                    /*check db last order id*/

                    this.postId = /*recieved*/ null

                }

                console.Log("called runPaymentSystem")
            },
            predictPast(title) {
                /*https://github.com/eugeneware/suggestion?ysclid=m4trjypl4t287222676 */
                /*$ npm install suggestion*/

                /*var suggest = require('suggestion');
                suggest('dog training', function (err, suggestions) {
                    if (err) throw err;
                    console.log(suggestions);
                })*/

                this.sentmessageinput += " " + title;
            },
            crawleeUserSearch() {
                this.sentmessageinput

                fetch('/api/CrawlleUserSearch/' + this.sentmessageinput)
                    .then(r => {
                        if (r.status == 200) {
                            console.log(r.status)
                        } 
                        return r.json()
                    })
                    .then(json => {
                        /* this.chatlist = json as Forecasts;*/
                        this.shopcarousellist = json;
                        return;
                    });

            }
        },
    });
</script>

<style>
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

    .attenuation {
        background: linear-gradient(to left, rgb(10 17 14) 0%, #00000000 10%);
        /*background: linear-gradient(to left, #4d4d4d7a 0%, transparent 10%);*/
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
    .searchTheme {
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

    .cursorshop {
        color: coral;
    }



    /*predict*/
    .productpredict {
        padding-left: 1em;
        padding-right: 1em;
        width: 100%;
        height: 4em;
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

    /*delivery*/
    .postDelivery {
        position: relative;
        height: 12em;
        /* width: 100%; */
        /* background: #ff0000fc; */
        padding: 1em;
        overflow: scroll;
        /* margin: 1em; */
    }
    
    .postDeliveryRow {
        height: 1.5em;
        width: 100%;
        /* background: blue; */
        padding: 0.1em;
        margin: 0.2em;
    }
</style>

