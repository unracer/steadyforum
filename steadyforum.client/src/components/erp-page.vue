<!-- ADD README
    crm
    smart-contract event
    ERP system with git integration to download a new PLC release, to change the manufacture specification
-->

<template>
    <div class="tabs chatgroupcontainer">
        <router-link class="tab-entity" :to="{ name: 'slider'}">
            <img class="chatBackgroundImage" src="../assets/logo.svg" alt="??" />
            <a>home</a>
        </router-link>
        <router-link class="tab-entity" :to="{ name: 'controll', params: { sessionId: 'db' }}">
            <img class="chatBackgroundImage" src="../assets/logo.svg" alt="??" />
            <a>db</a>
        </router-link>
        <router-link class="tab-entity" :to="{ name: 'controll', params: { sessionId: 'doc' }}">
            <img class="chatBackgroundImage" src="../assets/logo.svg" alt="??" />
            <a>doc</a>
        </router-link>
        <router-link class="tab-entity" :to="{ name: 'controll', params: { sessionId: 'analytic' }}">
            <img class="chatBackgroundImage" src="../assets/logo.svg" alt="??" />
            <a>analytic</a>
        </router-link>
        <router-link class="tab-entity" :to="{ name: 'controll', params: { sessionId: 'workers' }}">
            <img class="chatBackgroundImage" src="../assets/logo.svg" alt="??" />
            <a>workers</a>
        </router-link>
        <router-link class="tab-entity" :to="{ name: 'controll', params: { sessionId: 'event' }}">
            <img class="chatBackgroundImage" src="../assets/logo.svg" alt="??" />
            <a>event</a>
        </router-link>
    </div>

    <div class="siem-stat-container">

        <div class="circleWrapper" id="circleshop">
            <a id="circleLegend1">{{allorder}}</a>
            <a id="circleLegend2">{{allmerch}}</a>
            <!--<a id="circleLegend3">shop</a>-->
        </div>

        <div class="circleWrapper" id="circlenews">
            <a id="circleLegend1">{{allpost}}</a>
            <a id="circleLegend2">{{allcomment}}</a>
            <!-- <a id="circleLegend3">news</a>-->
        </div>

        <div class="circleWrapper" id="circlechat">
            <a id="circleLegend1">{{allchat}}</a>
            <a id="circleLegend2">{{alluser}}</a>
            <!--<a id="circleLegend3">chat</a>-->
        </div>

        <div class="circleWrapper" id="circledatabse">
            <a id="circleLegend1">{{weightStorage}}</a>
            <a id="circleLegend2">{{loadStorage}}</a>
            <!--<a id="circleLegend3">chat</a>-->
        </div>

        <div class="gistoWrapper">
            <div class="colWrapper"></div>
        </div>
    </div>

    <div class="siem-log-container">
        server log - 16.12.24 18:46 stat: cpu storage ethernet
        <br />
        <br />
        {{server_log}}
    </div>

    <div class="database-container">
        <details>
            <summary>Select table using mouse</summary>
            <p>user_db</p>
            <p>chat_db</p>
            <p>message_db</p>
            <p>shop_db</p>
            <p>forum-news_db</p>
            <p>forum-comment_db</p>
        </details>

        <input v-for="entity in editRowDatabase_1[0]" :key="entity.id"
               id="newRowDatabase" type="text" :placeholder="entity" />
        <button id="saveNewRow" v-on:click="() => saveNewRow()" style="color:green">save new row</button>
        <button id="deleteNewRow" v-on:click="() => deleteNewRow()" style="color:red">clear field</button><br /><br />

        <div class="dataTable">
            <div v-for="row in editRowDatabase_1" :key="row.id">
                <input id="editRowDatabase" type="text"
                       v-for="entity in row" :key="entity.id" :placeholder="entity" :value="entity" />
                <button class="databaseaction" v-on:click="() => saveNewRow()" style="color:green">save</button>
                <button class="databaseaction" v-on:click="() => deleteNewRow()" style="color:red">delete</button>
            </div>
        </div>
    </div>

    <div class="scada-map-container">
        <!--image in map role-->
        <img class="scada-map-background-image" src="../assets/scada-template-map.png" alt="image: scada-template-map" />

        <!--params-->
        <div class="plc-container">
            <div class="plc-scope"
                 v-for="plc in plcData" :key="plc.id">
                <!-- id need for position-->
                <button id="plc-action-{{plc.id}}"
                        v-for="action in plc" :key="action.type" :value="arg.value" :click="execute-plc-action(plc.id, plc.action)" />
            </div>

            <!-- id need for position-->
            <div class="plc-data-{{plc.id}}"
                 v-for="plc in plcData" :key="plc.id">
                <input id="plc-arg" type="text"
                       v-for="arg in plc" :key="arg.name" :placeholder="arg" :value="arg.value" />
            </div>
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
                server_log:
                    "request 234.142.234.2 /vue.icon \n" +
                    "request 234.142.234.2 ../../vue.icon (Possible path traversal) \n" +
                    "request 234.142.234.2 ../../vue.icon (Possible path traversal) \n" +
                    "request 234.142.234.2 ../../vue.icon (Possible path traversal) \n" +
                    "request 234.142.234.2 ../../vue.icon (Possible path traversal) \n" +
                    "request 234.142.234.2 ../../vue.icon (Possible path traversal) \n" +
                    "request 234.142.234.2 ../../vue.icon (Possible path traversal) \n" +
                    "request 234.142.234.2 ../../vue.icon (Possible path traversal) \n" +
                    "request 234.142.234.2 ../../vue.icon (Possible path traversal) \n" +
                    "request 234.142.234.2 ../../vue.icon (Possible path traversal) \n" +
                    "request 234.142.234.2 ../../vue.icon (Possible path traversal) \n" +
                    "request 234.142.234.2 ../../vue.icon (Possible path traversal) \n" +
                    "request 234.142.234.2 ../../vue.icon?index=alert() (Possible reflected xxs) \n"
                ,
                newRowDatabase_1: null,
                editRowDatabase_1: null,
                stat: null,
                allorder: null,
                allmerch: null,
                allpost: null,
                allcomment: null,
                allchat: null,
                alluser: null,
                weightStorage: null,
                loadStorage: null,
                editRowDatabase_1: [
                    { id: 1, name: "name123", female: "femail123" },
                    { id: 2, name: "name" },
                    { id: 3, name: "name", female: "femail" },
                    { id: 3, name: "name", female: "femail" },
                    { id: 3, name: "name", female: "femail" },
                    { id: 3, name: "name", female: "femail" },
                    { id: 3, name: "name", female: "femail" },
                    { id: 3, name: "name", female: "femail" },
                    { id: 3, name: "name", female: "femail" },
                    { id: 3, name: "name", female: "femail" },
                    { id: 3, name: "name", female: "femail" },
                    { id: 3, name: "name", female: "femail" },
                    { id: 3, name: "name", female: "femail" },
                ],
            };
        },
        created() {
            // fetch the data when the view is created and the data is
            // already being observed
            this.fetchData();
            this.drawStatImg();
            this.drawStatStorage();
        },
        watch: {
            // call again the method if the route changes
            '$route': 'fetchData'
        },
        methods: {
            fetchData(): void {
                this.post = null;
                this.loading = true;
            },
            async drawStatImg() {
                let obj = await this.asyncGetStatistic();
                if (obj) {
                    var rawStat = obj.split(/[\s,\n,\r]+/).filter(Boolean);
                    var c = 0, r = 0, row = 0; stat = []
                    // init for data
                    for (var i = 0; i < (rawStat.length ** (0.5)); i++) {
                        stat[i] = []
                    }
                    // set data like pattern
                    for (; row < rawStat.length; row++) {
                        console.log(rawStat[row] + c + r)
                        stat[c][r] = rawStat[row]
                        r++
                        if (r % 3 == 0) {
                            c++
                            r = 0
                        }
                    }
                } else {
                    console.log("init default stat")
                    var stat = [
                        ["shop", 7, 700], /*merch order*/
                        ["news", 20, 200], /*post comment*/
                        ["chat", 150, 300], /*user group*/
                        ["database", 5, 2000], /*load storage*/
                    ]
                }
                var imgLen = stat.length

                /*compute procent*/
                for (var i = 0; i < imgLen; i++) {
                    var totalImage = stat[i][2]
                    var renameImage = Math.floor(stat[i][1] / totalImage * 100)
                    totalImage = 100 - renameImage

                    var totalFolder = stat[i][2]
                    var renameFolder = stat[i][1] / totalFolder * 100
                    totalFolder = 100 - renameFolder

                    // redraw
                    document.getElementsByClassName("circleWrapper")[i].innerHTML = " "

                    document.getElementsByClassName("circleWrapper")[i].innerHTML += '<div class="circle" style="background: radial-gradient(#52525200 0, #52525200 60px, transparent 0px, transparent 0%), conic-gradient(#00ffee2e 0%, #00ffee ' + renameImage + '%, #ff5e0024 ' + renameImage + '%, #ff5e00 100%);"></div>'

                    document.getElementsByClassName("circleWrapper")[i].innerHTML += '<a id="circleLegend1">' + renameImage + '</a><a id="circleLegend2">' + totalImage + '</a><a id="circleLegend3">' + stat[i][0] + '</a>'
                }
            },

            async asyncGetStatistic() {
                return false;
            },
            drawStatStorage() {
                var storage = [
                    [38, 50, 89],
                    [39, 61, 79],
                    [40, 65, 97],
                    [41, 56, 96],
                    [42, 43, 67],
                    [43, 50, 83],
                    [44, 45, 72],
                    [45, 39, 54],
                    [46, 44, 99],
                    [47, 56, 92],
                    [48, 65, 89],
                    [49, 66, 90],
                    [61, 55, 93],
                    [62, 41, 87],
                    [63, 52, 69],
                    [64, 47, 92],
                    [65, 45, 85],
                ]
                var storageLen = storage.length

                for (var i = 0; i < storageLen; i++) {
                    var storageNum = storage[i][0]
                    var storageContain = storage[i][1]
                    var storageRecord = storage[i][2]
                    /*alert(document.getElementsByClassName("colWrapper")[0].innerHTML)*/
/*	                document.getElementsByClassName("colWrapper")[0].innerHTML += '<div class="barContainer"><div id="'+storageNum+'" class="bar1" style="height: '+storageRecord+'%;">'+storageRecord+'</div><div id="'+storageNum+'" class="bar2" style="height: '+storageContain+'%;">'+storageContain+'</div><div id="'+storageNum+'" class="bar3" style="height: 8%;">'+storageNum+'</div></div>';
*/                }
            },
        }
    });
</script>


<style>
    .siem-stat-container {
        position: relative;
        margin: 1em;
        height: 16em;
        background-color: #00000085;
        overflow: hidden;
        display: flex;
    }
    .siem-log-container {
        padding: 1em;
        white-space: pre-line;
        margin: 1em;
        height: 15em;
        background-color: #00000085;
        overflow: scroll;
    }
    .database-container {
        height: 20em;
        width: 100%;
        background-color: #00000085;
        padding: 1em;
        white-space: pre-line;
        overflow-y: scroll;
        margin: 1em;
    }

    .circleWrapper {
        position: relative;
        padding: 10px;
    }
    #circlenews {
       /* background: radial-gradient(#52525200 0, #52525200 60px, transparent 0px, transparent 0%), conic-gradient(#00ffee2e 0%, #00ffee {{allpost}} %, #ff5e0024 {{allcomment}}%, #ff5e00 100%);*/
    }
    #circleshop {
        /*background: radial-gradient(#52525200 0, #52525200 60px, transparent 0px, transparent 0%), conic-gradient(#00ffee2e 0%, #00ffee {{allorder}} %, #ff5e0024 {{allmerch}}%, #ff5e00 100%);*/
    }
    #circlechat {
        /*background: radial-gradient(#52525200 0, #52525200 60px, transparent 0px, transparent 0%), conic-gradient(#00ffee2e 0%, #00ffee {{alluser}} %, #ff5e0024 {{allgroup}}%, #ff5e00 100%);*/
    }
    #circledatabse {
        /*background: radial-gradient(#52525200 0, #52525200 60px, transparent 0px, transparent 0%), conic-gradient(#00ffee2e 0%, #00ffee {{}} %, #ff5e0024 {{}}%, #ff5e00 100%);*/
    }


    #boxCheck {
        margin-left: 310px
    }

    #imageRename {
        margin-left: 620px
    }

    #circleLegend1 {
        position: absolute;
        margin-top: -110px;
        background-color: #00ffee;
        margin-left: 120px;
    }

    #circleLegend2 {
        position: absolute;
        margin-top: -110px;
        background-color: #ff5e00;
        margin-left: 60px;
    }

    .circle {
        width: 200px;
        height: 200px;
        border-radius: 200px;
    }

    .gistoWrapper {
        height: 200px;
        width: 820px;
        position: absolute;
        margin-top: 260px;
        padding: 10px;
        background-color: #525252;
        box-shadow: 0px 0px 5px 5px grey;
    }

    .colWrapper {
        height: 200px;
        width: 100%;
        position: absolute;
        /*border: 1px solid #ccc;*/
        display: flex;
        align-items: flex-end;
        font-size: 0.8em;
    }

    .barContainer {
        width: 20px;
    }

    .bar1 {
        width: 20px;
        background-color: #ff6500d1;
        position: absolute;
        bottom: 0px;
        z-index: 1;
    }

    .bar2 {
        width: 20px;
        background-color: #00f8ffd1;
        position: absolute;
        bottom: 0;
        z-index: 2;
    }

    .bar3 {
        width: 20px;
        height: 10%;
        background-color: #c700ff99;
        position: absolute;
        bottom: 0;
        z-index: 3;
    }

    .gistoWorkWrapper {
        height: 200px;
        width: 820px;
        position: absolute;
        margin-top: 500px;
        padding: 10px;
        background-color: #525252;
        box-shadow: 0px 0px 5px 5px grey;
        overflow-y: hidden;
        overflow-x: scroll;
    }

    .colWorkWrapper {
        height: 200px;
        position: absolute;
        /*border: 1px solid #ccc;*/
        display: flex;
        align-items: flex-end;
        font-size: 0.8em;
    }

    .barWorkContainer {
        width: 20px;
    }

    .notifyWrapper {
        height: 200px;
        width: 820px;
        position: absolute;
        margin-top: 740px;
        padding: 10px;
        background-color: #525252;
        box-shadow: 0px 0px 5px 5px grey;
        font-size: 0.9em;
        overflow-x: hidden;
    }

    .notifyMsg {
        height: 20px;
        width: 100%;
        overflow: hidden;
        margin: 0;
    }
    .databaseaction{
        text-align: right;
    }

    /*tabs*/

    .tab-entity:not(:first-child) {
        margin-left: -50px;
    }

    .tab-entity:hover {
        transform: translateY(-20px);
        transition: 0.4s ease-out;
    }

        .tab-entity:hover ~ .tab-entity {
            position: relative;
            left: 50px;
            transition: 0.4s ease-out;
        }



        .tab-entity:hover .filledbar {
            width: 120px;
            transition: 0.4s ease-out;
        }


        .tab-entity:hover .stroke {
            stroke-dashoffset: 100;
            transition: 0.6s ease-out;
        }

    .chatgroupcontainer {
        top: 60px;
        left: calc(50% - 300px);
        display: flex;
    }
    .tab-entity {
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

    .scada-map-background-image {
         width: 100em;
        padding: 1em;
        margin: 1em;
    }

</style>