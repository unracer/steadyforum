<script setup lang="ts">
    import forumpage from './forum-page.vue'
    import chatpage from './chat-page.vue'
    import shoppage from './shop-page.vue'
    import erppage from './erp-page.vue'
</script>

<template>
    <div class="slidercontainer">
        <input type="radio" name="slider" id="item-1" checked>
        <input type="radio" name="slider" id="item-2" >
        <input type="radio" name="slider" id="item-3" >
        <input type="radio" name="slider" id="item-4" >
        <div class="slidercards">
            <label class="slidercard" for="item-1" id="sliderforumcard"> <!--what for - check delete id for this-->
                <forumpage />
            </label>
            <label class="slidercard" for="item-2" id="sliderchatcard">
                <chatpage/>
            </label>
            <label class="slidercard" for="item-3" id="slidershopcard">
                <shoppage /> 
            </label>
            <label class="slidercard" for="item-4" id="slidererpcard">
                <erppage /> 
            </label>
        </div>
        <div class="player">
            <div class="upper-part">
                <div class="play-icon">
                    <svg width="20" height="20" fill="#2992dc" stroke="#2992dc" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" class="feather feather-play" viewBox="0 0 24 24">
                        <defs />
                        <path d="M5 3l14 9-14 9V3z" />
                    </svg>
                </div>
                <div class="info-area" id="test">
                    <label class="song-info" id="song-info-1">
                        <div class="title">Bunker</div>
                        <div class="sub-line">
                            <div class="subtitle">Balthazar</div>
                            <div class="time">4.05</div>
                        </div>
                    </label>
                    <label class="song-info" id="song-info-2">
                        <div class="title">Words Remain</div>
                        <div class="sub-line">
                            <div class="subtitle">chat</div>
                            <div class="time">4.05</div>
                        </div>
                    </label>
                    <label class="song-info" id="song-info-3">
                        <div class="title">Falling Out</div>
                        <div class="sub-line">
                            <div class="subtitle">Otzeki</div>
                            <div class="time">4.05</div>
                        </div>
                    </label>
                </div>
            </div>
            <div class="progress-bar">
                <span class="progress"></span>
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
                post: null
            };
        },
        created() {
            // fetch the data when the view is created and the data is
            // already being observed
            this.fetchData();
        },
        watch: {
            // call again the method if the route changes
            '$route': 'fetchData'
        },
        methods: {
            fetchData(): void {
                this.post = null;
                this.loading = true;

                /*fetch('weatherforecast')
                    .then(r => r.json())
                    .then(json => {
                        this.post = json as Forecasts;
                        this.loading = false;
                        return;
                    });*/
            },
            onclickchangeslidercolor(){

                $('input').on('change', function() {
                  $('body').toggleClass('blue');
                });
            }
        },
    });
</script>

<style scope>
    @import url("https://fonts.googleapis.com/css?family=DM+Sans:400,500,700&display=swap");

    * {
        box-sizing: border-box;
    }

    html, body {
        margin: 0;
        padding: 0;
        width: 100%;
        height: 100%;
    }

    body {
        display: flex;
        align-items: center;
        justify-content: center;
        /*padding: 10px 10px;*/
        font-family: 'DM Sans', sans-serif;
        transition: background .4s ease-in;
    }
    
    .blue

    {
        background-color: #428aa6;
    }

    /*carausel*/

    input[type=radio] {
        display: none;
    }

    .slidercard {
        position: absolute;
        width: 80%;
        height: 100%;
        left: 0;
        right: 0;
        margin: auto;
        transition: transform .4s ease;
        cursor: pointer;
        overflow: hidden;
    }

    .slidercontainer, #data-v-0807e5e9 {
        width: 100%;
        height: 100%;
        transform-style: preserve-3d;
        display: flex;
        justify-content: center;
        flex-direction: column;
        align-items: center;
        /*overflow: hidden;*/
    }

    .slidercards {
        position: relative;
        width: 100%;
        height: 100%;
        margin-bottom: 20px;
    }

    img {
        width: 100%;
        height: 100%;
        border-radius: 10px;
        object-fit: cover;
    }

/*доделать четверту.ю страницу*/
    #item-1:checked ~ .slidercards #slidershopcard, #item-2:checked ~ .slidercards #sliderforumcard, #item-3:checked ~ .slidercards #sliderchatcard {
        transform: translateX(-40%) scale(.8);
        opacity: .4;
        z-index: 0;
        filter: blur(5px);
    }

    #item-1:checked ~ .slidercards #sliderchatcard, #item-2:checked ~ .slidercards #slidershopcard, #item-3:checked ~ .slidercards #sliderforumcard {
        transform: translateX(40%) scale(.8);
        opacity: .4;
        z-index: 0;
        filter: blur(5px);
    }

    #item-1:checked ~ .slidercards #sliderforumcard, #item-2:checked ~ .slidercards #sliderchatcard, #item-3:checked ~ .slidercards #slidershopcard {
        transform: translateX(0) scale(1);
        opacity: 1;
        z-index: 1;
    } 
    #item-1:checked ~ .slidercards #sliderforumcard, #item-2:checked ~ .slidercards #sliderchatcard, #item-3:checked ~ .slidercards #slidershopcard {
        transform: translateX(0) scale(1);
        opacity: 1;
        z-index: 1;
    } 
     img

    {
        box-shadow: 0px 0px 5px 0px rgba(81, 81, 81, 0.47);
    }

    

    .player {
        background-color: #fff;
        border-radius: 8px;
        min-width: 320px;
        padding: 16px 10px;
    }

    .upper-part {
        position: relative;
        display: flex;
        align-items: center;
        margin-bottom: 12px;
        height: 36px;
        overflow: hidden;
    }

    .play-icon {
        margin-right: 10px;
    }

    .song-info {
        width: calc(100% - 32px);
        display: block;
    }

        .song-info .title {
            color: #403d40;
            font-size: 14px;
            line-height: 24px;
        }

    .sub-line {
        display: flex;
        justify-content: space-between;
        width: 100%;
    }

    .subtitle, .time {
        font-size: 12px;
        line-height: 16px;
        color: #c6c5c6;
    }

    .time {
        font-size: 12px;
        line-height: 16px;
        color: #a5a5a5;
        font-weight: 500;
        margin-left: auto;
    }

    .progress-bar {
        height: 3px;
        width: 100%;
        background-color: #e9efff;
        border-radius: 2px;
        overflow: hidden;
    }

    .progress {
        display: block;
        position: relative;
        width: 60%;
        height: 100%;
        background-color: #2992dc;
        border-radius: 6px;
    }

    .info-area {
        width: 100%;
        position: absolute;
        top: 0;
        left: 30px;
        transition: transform .4s ease-in;
    }

    #item-2:checked ~ .player #test {
        transform: translateY(0);
    }

    #item-2:checked ~ .player #test {
        transform: translateY(-40px);
    }

    #item-3:checked ~ .player #test {
        transform: translateY(-80px);
    }
    #item-4:checked ~ .player #test {
        transform: translateY(-120px);
    }

    #sliderforumcard {
        background-color: #140000d1;
        box-shadow: 0px 0px 100px 0px rgb(0 0 0);
        border-radius: 10px;
        object-fit: cover;
        color: #ff6c00;
    }

    #sliderchatcard {
        background-color: #0a0c12f2;
        box-shadow: 0px 0px 100px 0px rgb(0 0 0);
        border-radius: 10px;
        object-fit: cover;
    }

    #slidershopcard {
        background-color: #0a120ff2;
        box-shadow: 0px 0px 100px 0px rgb(0 0 0);
        border-radius: 10px;
        object-fit: cover;
    }
</style>