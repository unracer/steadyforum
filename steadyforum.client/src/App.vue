<script setup lang="ts">
    import thelogin from './components/thelogin.vue'
    import slideritem from './components/slideritem.vue'

</script>

<template>
    <div class="background"></div>
    <thelogin />
    <slideritem /> <!--view if have cookie-->
</template>

<style scope>
    #data-v-app, #app {
        margin: 0;
        padding: 0;
        width: 100%;
        height: 100%;
    }

    header {
        line-height: 1.5;
    }

    .logo {
        display: block;
        margin: 0 auto 2rem;
    }

    .background {
        background-image: url("https://localhost:5173/src/assets/destuny-background-2.jpg");
        background: linear-gradient(160deg, rgb(43 7 0 / 100%) 50%, rgb(43 7 0) 77%);
        background-image: url("https://localhost:5173/src/assets/destuny-background-1.jpg");
        position: absolute;
        height: 100%;
        width: 100%;
        background-size: cover;
        background-position: center;
        filter: blur(10px);
        z-index: -1;
        opacity: 2;
    }
</style>
