import './assets/main.css'

import * as Vue from 'vue'
/*npm install vue-router*/
import * as VueRouter from 'vue-router'
/*npm install vue-cookies*/
import VueCookies from 'vue-cookies';
import App from './App.vue'

import thelogin from './components/the-login.vue';
import controllpage from './components/erp-page.vue';
import slideritem from './components/slider-item.vue'


const routes = [
    {
        path: '/',
        name: 'root',
        component: thelogin,
    },
    {
        path: '/slider',
        name: 'slider',
        component: slideritem,
    },
    {
        path: '/Chat/:name',
        name: 'chat',
        component: slideritem,
    },
    /*title couse expired link leave amount unlike human-like data
        also by title can serach expired goods in other dealers*/
    {       
        path: '/Forum/:titleid',
        name: 'news',
        component: slideritem,
    },
    /*title couse expired link leave amount unlike human-like data
        also by title can serach expired goods in other dealers*/
    {        
        path: '/Shop/:title', 
        name: 'shop',
        component: slideritem,
    },
    {
        path: '/Controll/:sessionId',
        name: 'controll',
        component: controllpage,
    },
]

const router = VueRouter.createRouter({
    history: VueRouter.createWebHistory(),
    routes,
})
const app = Vue.createApp(App)

app.use(router)
app.use(VueCookies);
app.mount('#app')

