<template  @mouseover="pageOnfocus = true" @mouseleave="pageOnfocus = false">
    <div v-if="loading" class="loading">
        <!-- make random and add second game -->
        Loading... Please  <a href="https://mrdoob.com/projects/chromeexperiments/google-gravity/">play with me</a> <!--for more fun.-->
    </div>

    <div class="chatcontainer">
        <div id="chatcard" v-for="chat in chatcontent" :key="chat.uname">
            <p id="chatuname">{{chat.uname}}</p>
            <p id="chatimage">{{chat.image}}</p>
            <p id="chatdate">{{chat.date}}</p>
            <p id="chatreadstate">{{chat.readstate}}</p>
            <p id="chatmessage">{{chat.message}}</p>
        </div>
    </div>

    <div class="inputContainer">
        <!--<img id="chatMsgImg" src="../assets/sidebarArrow.png" alt=">" />-->
        <form class="form" id="msgForm" ref="msgFormRef" v-on:submit.prevent="onSubmit">
            <input type="text" class="chatmessageinput" v-model="sentmessageinput" />
            <input type="submit" class="chatmessageinput" @click="msgSend" style="display: none" />
            <a class="cursorchat">></a>
            <label class="cursor"></label>
        </form>
    </div>

    <div class="chatinfocontainer">
        <router-link class="chatinfocard" v-for="chat in chatlist" :key="chat.id" :to="{ name: 'chat', params: { name: chat.name }}">
            <img class="chatBackgroundImage" src="../assets/logo.svg" alt="??" />
        </router-link>

        <button class="chatinfocard" @click="chatNew"><!--<img class="chatBackgroundImage" src="../assets/logo.svg" alt="??" />-->+++</button>
        
        <div class="chatcreate" v-if="chatcreatebutton">
            <form v-on:submit.prevent="onSubmit">
                <input type="text" id="chatInputName" v-model="chatInputName" placeholder=" chat name" />
                <input type="text" id="chatInputKey" v-model="chatInputKey" placeholder=" secret key send friend" />
                <a id="chatError">{{chatError}}</a>
                <input type="submit" @click="chatNew" style="display: none" />
            </form>
        </div>
    </div>
</template>



<script lang="ts">
    import { defineComponent } from 'vue';

    export default defineComponent({ 
        components: {

        },
        data(): Data {
            return {
                loading: false,
                post: null,
                chatlist: [
                    { name: "name ..", image: "../assets/logo.svg" },
                    { name: "name ..", image: "../assets/logo.svg" },
                    { name: "name ..", image: "../assets/logo.svg" },
                    { name: "name ..", image: "../assets/logo.svg" },
                    { name: "name ..", image: "../assets/logo.svg" },
                    { name: "name ..", image: "../assets/logo.svg" },
                    { name: "name ..", image: "../assets/logo.svg" },
                    { name: "name ..", image: "../assets/logo.svg" },
                    { name: "name ..", image: "../assets/logo.svg" },
                    { name: "name ..", image: "../assets/logo.svg" },
                    ],
                chatcontent: [
                    { uname: "uname ..", image: "../assets/logo.svg", message: "message .." },
                    { uname: "uname ..", image: "../assets/logo.svg", message: "message .." },
                    { uname: "uname ..", image: "../assets/logo.svg", message: "message .." },
                    { uname: "uname ..", image: "../assets/logo.svg", message: "message .." },
                    { uname: "uname ..", image: "../assets/logo.svg", message: "message .." },
                    { uname: "uname ..", image: "../assets/logo.svg", message: "message .." },
                    { uname: "uname ..", image: "../assets/logo.svg", message: "message .." },
                    { uname: "uname ..", image: "../assets/logo.svg", message: "message .." },
                    { uname: "uname ..", image: "../assets/logo.svg", message: "message .." },
                    { uname: "uname ..", image: "../assets/logo.svg", message: "message .." },
                ],
                chatcreatebutton: false,
                sentmessageinput: null,
                pageOnfocus: false,
                lastid: null,

                currentchatname: null,
                oldchatname: null,
                chatnamechaged: null,

                chatInputName: null,
                chatInputKey: null,

                chatError: null,
            };
        },
        created() {
            // fetch the data when the view is created and the data is
            // already being observed
            this.getChatList();
            /*this.fetchData();*/
        },
        watch: {
            // call again the method if the route changes
            /*'$route': 'fetchData'*/
            '$route': 'changechat'
        },
        methods: {
            fetchData(): void {
                this.post = null;
                this.loading = true;              
            }, 
            changechat() {
                if (this.oldchatname != this.currentchatname) { this.chatnamechaged = true; this.GetChatContentNotSubscribe(); } 
            },
            chatNew() {
                // only hide field
                if (this.chatcreatebutton == false) {
                    this.chatcreatebutton = true
                } else {
                    this.chatcreatebutton = false
                }
                // await cred input
                // request by cred
            },
            getChatList() {
                if (this.$cookies.get("steadyforumsessionid")) { 
                    fetch('api/List/' + this.$cookies.get("steadyforumsessionid"))
                    .then(r => r.json())
                    .then(json => {
                        /* this.chatlist = json as Forecasts;*/
                        this.chatlist = json;
                        this.lastid = json.lenght;
                        /*alert(Object.keys(json.list).length + " unit of chat list");*/
                        this.loading = false;
                        return;
                    });
                }
            },
            async LoginChat(){


            },
            async GetChatContentNonWs() { /*Depricated*/
                if (this.chatnamechaged == true) { 
                    this.chatnamechaged == false
                    while (this.chatnamechaged == false) {
                        if (this.pageOnfocus) {

                            if (chatcontent == null) {
                                id = 0
                            } else {
                                id = this.lastid
                            }

                            fetch('api/Chat/' + this.$cookies.get("steadyforumsessionid") + '/' + this.chatname + '/' + id  )
                                .then(r => r.json())
                                .then(json => {
                                    /* this.chatlist = json as Forecasts;*/
                                    this.chatcontent = json;
                                    this.lastid = json.lenght - 1;
                                    alert(json.lenght +"unit of chat content");
                                    this.loading = false;
                                    return;
                                });
                            await new Promise(resolve => setTimeout(resolve, 1000)); 
                        }
                        document.getElementById("chatcontainer").scrollTop = 99999;
                    }
                }  
            },  
             async PostChatContentNonWs() { /*Depricated*/
                let dateNow = new Date();
                const requestOptionsPost = {
                        method: "POST",
                        headers: { "Content-Type": "application/json" },
                        body: JSON.stringify({
                            
                              /*"id": 0,*/
                              /*"idcontent": 0,*/
                              /*"readed": {
                                "year": 0,
                                "month": 0,
                                "day": 0,
                                "dayOfWeek": 0
                              },*/
                              "date": {
                                "year": dateNow.getFullYear(),
                                "month": dateNow.getMonth(),
                                "day": dateNow.getDate(),
                                "dayOfWeek": 0
                              },
                              "uname": "string",
                              "text": "string",
                              /*"mediapath": "string",*/
                              /*"geo": "string"*/                            
                        })
                    };

                fetch('api/Chat/', requestOpions  )
                    .then(r => r.json())
                    .then(json => {
                        alert(json);
                        return;
                    });    
             }, 
            subscribeChatContent(name) {
                let sessionid = this.$cookies.get("acidtrainsession");
        
                if (webSocket == null) {
                    let webSocket = new WebSocket('wss://localhost:5001/wschat');
                }

                webSocket.send(name+"\s"+sessionid)

                webSocket.onmessage = function(event) {
                  if (event.code == 100){ webSocket.close()}

                   chatcontent = event.data
                };

            },
            sentMessage() {
                // get session from cookie
                // sent session and content
            },
            login(){
                // test session expired
                // get new session id
                // set cookie
            }
        },
    });
</script>


<style>
    .inputContainer {
        height: 5%;
    }

    .cursorchat {
        color: lightseagreen;
    }

    #msgForm {
        position: fixed;
        left: 1em;
        /*top: 49.0em;*/
        width: 70%;
    }

    #chatMsgImg {
        background-color: #00000000;
        border: none;
        margin-top: 1em;
        margin-left: 0em;
        /*        position: fixed;*/
        width: 1em;
        height: 1em;
        top: 41em;
        left: 1.5em;
    }

    .chatmessageinput {
        background-color: #00000000;
        width: 100%;
        border: none;
        position: absolute;
        color: white;
        font-size: 1.2em;
        margin-left: 1em;
        left: -0.3em;
    }

    input:focus {
        outline: none;
    }

    .form label:before {
        content: '';
        display: none;
        position: absolute;
        width: 10px;
        height: 5px;
        background: #fff;
        opacity: 1;
        z-index: 3;
        color: white;
    }

    .form label.cursor:before {
        display: inline-block;
        animation: cursor 0.5s infinite step-end;
        color: white;
        margin-top: 1.3em;
    }

    @keyframes cursor {
        50% {
            background: transparent;
        }
    }

    #msgBtn {
        display: none;
    }

    .post {
        height: 100%;
    }

    .chatcontainer {
        height: 85%;
        overflow: scroll;
    }

    #chatcard {
        padding: 1em
    }

    ::-webkit-scrollbar {
        /* width: 1.4vw;
        height: 3.3vh;*/
        display: none;
    }

    ::-webkit-scrollbar-track {
        /*  display: none*/
        /*border-radius: 10px;*/
    }

    .chatinfocontainer {
        top: 60px;
        left: calc(50% - 300px);
        display: flex;
    }

    .chatinfocard {
        display: flex;
        height: 50px;
        width: 100px;
        background-color: #17141d;
        border-radius: 10px;
        box-shadow: -1rem 0 3rem #0000007a;
        /*   margin-left: -50px; */
        transition: 0.4s ease-out;
        position: relative;
        left: 0px;
        color: #2c3e50;
    }
    .chatname {
        background-color: #17141d;
    }

    .chatinfocard:not(:first-child) {
        margin-left: -50px;
    }

        .chatinfocard:hover {
            transform: translateY(-20px);
            transition: 0.4s ease-out;
        }

            .chatinfocard:hover ~ .chatinfocard {
                position: relative;
                left: 50px;
                transition: 0.4s ease-out;
            }



            .chatinfocard:hover .filledbar {
                width: 120px;
                transition: 0.4s ease-out;
            }


            .chatinfocard:hover .stroke {
                stroke-dashoffset: 100;
                transition: 0.6s ease-out;
            }
</style>