<!--we not garanted more 50% uptime

    + персональный цвет загрузки с краю от логина (open source антифишинг)

    but still freedom place

    уникалльная возможность блокировать вход после потери фокуса, если вход был с недоверенного устройства
    *отдельный модуль по странному поведению пользователя, по ключевым словам поиска


    -->

<template>
    <div class="loginWrapper" v-if="isloginshow">
        <p> we are next generation of society network.</p>
        <p> chat anon and fast with us.</p>
        <p>we not garanted more 50% uptime</p>
        <p>but still freedom place</p>

        <div class="loader" >
            <div class="inner one" style="border-bottom: 3px solid {{loadercolor}};"></div>
            <div class="inner two" style="border-right: 3px solid {{loadercolor}}; "></div>
            <div class="inner three" style="border-top: 3px solid {{loadercolor}};"></div>
        </div>

        <br>
        <input id="loginName" v-model="userLoginInput" type="password" /><br />
        <input id="loginPassword" v-model="userPasswordInput" type="password" /><br />
        <button id="loginBtn" v-on:click="() => userLogin()">LOGIN</button>
        <div v-if="loginMessage != null" class="error">{{loginMessage}}</div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';

    export default defineComponent({
        data(): Data {
            return {
                loading: false,
                post: null,
                isloginshow: true,
                userLoginInput: null,
                userPasswordInput: null,
                issessionexpired: true,
                loginMessage: null,
                loadercolor: "#ffff00",
            };
        },
        created() {
            // fetch the data when the view is created and the data is
            // already being observed
            await this.requestloadercolor()
            this.userLogin()
            this.userLoginInput = this.$cookies.set("steadyforumuname");
        },
        watch: {
            // call again the method if the route changes
        },
        methods: {
            async sha256(message) {
                const msgBuffer = new TextEncoder('utf-8').encode(message);
                const hashBuffer = await crypto.subtle.digest('SHA-256', msgBuffer);
                const hashArray = Array.from(new Uint8Array(hashBuffer));
                const hashHex = hashArray.map(b => ('00' + b.toString(16)).slice(-2)).join('');
                return hashHex;
            },
            async userLogin() {
                if (this.$cookies.get("steadyforumsessionid")) {
                    fetch('/api/Details/' + this.$cookies.get("steadyforumsessionid"))
                        .then(response => {
                            if (response.status == 400) {
                                this.isloginshow = true;
                            } else if (response.status == 200) {
                                this.isloginshow = false;
                            }
                            return
                        })
                }

                if (!this.userLoginInput) { return }

                var userCredBase64 = btoa(unescape(encodeURIComponent(this.userLoginInput + this.userPasswordInput)));
                var userCredSha256 = await this.sha256(userCredBase64)

                fetch('/api/Login/' + this.userLoginInput + "/" + userCredSha256)
                    .then(r => r.json())
                    .then(json => {
                        this.$cookies.remove('steadyforumuname');
                        this.$cookies.set("steadyforumuname", this.userLoginInput, "expiring time");
                        this.$cookies.remove('steadyforumsessionid');
                        this.$cookies.set("steadyforumsessionid", json.sessionid, "expiring time")
                        this.isloginshow = false;
                        return;
                    });
            },
            requestloadercolor() {
                fetch('/api/LoginColor/' this.sessionId)
                    .then(r => r.json())
                    .then(json => {
                        this.loadercolor = json.loadercolor;
                        return;
                    });
            }
        },
    });
</script>

<style scoped>
    .loginWrapper {
        background: #0d0d0dfc;
        /*padding-top: 20em;*/
        height: 100%;
        z-index: 20;
        position: absolute;
        margin: auto;
        left: 0;
        right: 0;
        text-align: center;
        justify-content: center;
        flex-direction: column;
        align-items: center;
        display: flex
    }

    .isclosed {
        display: none;
        /*transition*/
    }

    #loginName {
        background: none;
        height: 3em;
        color: white;
        outline: 2px solid #fff;
        border-radius: 5px;
    }

    #loginPassword {
        background: none;
        height: 3em;
        color: white;
        outline: 2px solid #fff;
        border-radius: 5px;
    }

    #loginBtn {
        margin-top: 0.2em;
        background: none;
        border: none;
        font-size: 1.2em;
        color: #00ffc4;
    }

    #loginBtn:active {
        background: gray;
        transition: 0.2s ease-in;
    }

    .loader {
        position: absolute;
        top: calc(10% - 32px);
        left: calc(10% - 32px);
        width: 64px;
        height: 64px;
        border-radius: 50%;
        perspective: 800px;
    }

    .inner {
        position: absolute;
        box-sizing: border-box;
        width: 100%;
        height: 100%;
        border-radius: 50%;
    }

        .inner.one {
            left: 0%;
            top: 0%;
            animation: rotate-one 1s linear infinite;
            border-bottom: 3px solid #EFEFFA;
        }

        .inner.two {
            right: 0%;
            top: 0%;
            animation: rotate-two 1s linear infinite;
            border-right: 3px solid #EFEFFA;
        }

        .inner.three {
            right: 0%;
            bottom: 0%;
            animation: rotate-three 1s linear infinite;
            border-top: 3px solid #EFEFFA;
        }

    @keyframes rotate-one {
        0% {
            transform: rotateX(35deg) rotateY(-45deg) rotateZ(0deg);
        }

        100% {
            transform: rotateX(35deg) rotateY(-45deg) rotateZ(360deg);
        }
    }

    @keyframes rotate-two {
        0% {
            transform: rotateX(50deg) rotateY(10deg) rotateZ(0deg);
        }

        100% {
            transform: rotateX(50deg) rotateY(10deg) rotateZ(360deg);
        }
    }

    @keyframes rotate-three {
        0% {
            transform: rotateX(35deg) rotateY(55deg) rotateZ(0deg);
        }

        100% {
            transform: rotateX(35deg) rotateY(55deg) rotateZ(360deg);
        }
    }
</style>