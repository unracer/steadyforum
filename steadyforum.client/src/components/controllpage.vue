<template >
    <!--<div class="siem-stat-container">

        <div class="circleWrapper" id="boxSort">
            <a id="circleLegend1"></a>
            <a id="circleLegend2"></a>
        </div>

        <div class="circleWrapper" id="boxCheck">
            <a id="circleLegend1"></a>
            <a id="circleLegend2"></a>
        </div>

        <div class="circleWrapper" id="imageRename">-->
            <!-- не все верно переименованы кста -->
            <!--<a id="circleLegend1"></a>
            <a id="circleLegend2"></a>
        </div>

        <div class="gistoWrapper">
            <div class="colWrapper"></div>
        </div>
    </div>

    <div class="siem-log-container">
        {{server_log}}
    </div>-->

    <div class="database-container">
        <details>
            <summary>Select table use mouse</summary>
            <p>user</p>
            <p>chat</p>
            <p>message</p>
            <p>shop</p>
            <p>forum-news</p>
            <p>forum-comment</p>
        </details>

        <input id="newRowDatabase" v-model="newRowDatabase_1" type="text" />
        <button id="saveNewRow" v-on:click="() => saveNewRow()">save</button>
        <button id="deleteNewRow" v-on:click="() => deleteNewRow()">delete</button>

        <div class="dataTable">
            <input id="editRowDatabase" v-model="editRowDatabase_1" type="text"
                    v-for="entity in row" :key="entity.name" />
            <button id="saveNewRow" v-on:click="() => saveNewRow()">save</button>
            <button id="deleteNewRow" v-on:click="() => deleteNewRow()">delete</button>
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
                    "request 234.142.234.2 /vue.icon " +
                    "request 234.142.234.2 ../../vue.icon (Possible path traversal) "   +               
                    "request 234.142.234.2 ../../vue.icon?index=alert() (Possible reflected xxs) "
                ,
                newRowDatabase_1: null,
                editRowDatabase_1: null,
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
            async drawStatImg(){
		        let obj = await this.asyncGetStatistic();
		        if (obj){
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
				        if (r % 3 == 0)  {
					        c++
					        r = 0				
				        }
			        }
		        } else {
			        console.log("init default stat")
			        var stat = [
				        ["sortBox",80147,102050], /*36700 100124; 60147 080324; 80000 290524*/
				        ["checkBox",36700,102050], /*36700 100124; 60147 080324;*/
				        ["renameImage",38218,560123], /*38218 100124; 64839 080324;*/ /*5601234 100124; 829000 080324; 1075000 290524*/
				        ]
		        }
		        var imgLen = stat.length

		        /*compute procent*/
		        for (var i = 0; i < 3; i++) {
			        var totalImage = stat[i][2]
			        var renameImage = Math.floor(stat[i][1] / totalImage * 100)
			        totalImage = 100 - renameImage
			
			        var totalFolder = stat[i][2]
			        var renameFolder = stat[i][1] / totalFolder * 100
			        totalFolder = 100 - renameFolder

			        // redraw
			       /* document.getElementsByClassName("circleWrapper")[i].innerHTML = ""

			        document.getElementsByClassName("circleWrapper")[i].innerHTML += '<div class="circle" style="background: radial-gradient(#525252 0, #525252 60px, transparent 0px, transparent 0%), conic-gradient(#00ffee94 0%, #00ffee '+renameImage+'%, #ff5e0099 '+renameImage+'%, #ff5e00 100%);"></div>'

			        document.getElementsByClassName("circleWrapper")[i].innerHTML += '<a id="circleLegend1">'+renameImage+'</a><a id="circleLegend2">'+totalImage+'</a>' 
		        */}
	        },

            async asyncGetStatistic(){
                return false;
            },
            drawStatStorage(){
                var storage = [
	                [38,50,89],
	                [39,61,79],
	                [40,65,97],
	                [41,56,96],
	                [42,43,67],
	                [43,50,83],
	                [44,45,72],
	                [45,39,54],
	                [46,44,99],
	                [47,56,92],
	                [48,65,89],
	                [49,66,90],
	                [61,55,93],
	                [62,41,87],
	                [63,52,69],
	                [64,47,92],
	                [65,45,85],
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
        height: 15em;
        width: 50%;
        background-color: red;
    }
    .siem-log-container {
        height: 15em;
        width: 50%;
        background-color: blue;
        overflow: scroll;
    }
    .database-container { 
        height: 20em;
        width: 100%;
        background-color: yellow;
    }
    
</style>